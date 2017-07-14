using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Authorization;

using IdentityModel;
using IdentityServer4.Services;
using IdentityServer4.Models;

using AccountService.Services.Contracts;
using AccountService.Models;

using SandboxCore.Authentication;
using SandboxCore.Models.AccountViewModels;
using System.Text.RegularExpressions;
using AccountService.Models.RequestModels;

namespace SandboxCore.Controllers
{
    [Authorize]
    [SecurityHeaders]
    public class AccountController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IUserDataService _userDataService;
        //private readonly IdentityOptions _options;

        public AccountController(
                    IIdentityServerInteractionService interaction,
                    IUserDataService userDataService
                    //IdentityOptions options
                    )
        {
            _interaction = interaction;
            _userDataService = userDataService;
            //_options = options;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var vm = new LoginVM()
            {
                AllowRememberLogin = true,
                EnableLocalLogin = true,
                ExternalProviders = new List<ExternalProvider>()
            };

            return View(vm);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputVM model)
        {
            if (ModelState.IsValid)
            {
                // check password
                bool correctPassword = await _userDataService.CheckUserPassword(model.Username, model.Password);

                if (!correctPassword)
                {
                    ModelState.AddModelError("", AuthenticationOptions.InvalidCredentialsErrorMessage);
                    return await Login(Request.Query["returnUrl"]);
                }

                // get user object
                var user = await _userDataService.GetUserByUsername(model.Username);

                await LogUserIn(user, model.RememberLogin);

                Microsoft.Extensions.Primitives.StringValues returnUrl = "";
                if (Request.Query.TryGetValue("returnUrl", out returnUrl))
                    return Redirect(Request.Query["returnUrl"]);

                return RedirectToAction("index", "home");
            }
            ModelState.AddModelError("", AuthenticationOptions.InvalidCredentialsErrorMessage);
            return await Login(Request.Query["returnUrl"]);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // delete local authentication cookie
            await HttpContext.Authentication.SignOutAsync();           
            return RedirectToAction("LoggedOut");
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoggedOut()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Logout");

            var vm = new LoggedOutVM()
            {
                AutomaticRedirectAfterSignOut = AuthenticationOptions.AutomaticRedirectAfterSignOut,
                PostLogoutRedirectUri = AuthenticationOptions.SignOutRedirectUrl
            };
            return View(vm);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {            
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    Email = model.Email
                };
                try
                {
                    var newUser = await _userDataService.Create(user, model.Password);
                    if (newUser != null)
                    {
                        await LogUserIn(newUser, false);

                        Microsoft.Extensions.Primitives.StringValues returnUrl = "";
                        if (Request.Query.TryGetValue("returnUrl", out returnUrl))
                            return Redirect(Request.Query["returnUrl"]);

                        return RedirectToAction("MyProfile", "Account");
                    }
                    
                }
                catch(Exception ex)
                {
                    // add errors...
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
            var user = await _userDataService.GetUser(User.UserId());            
            if(user != null)
            {
                var vm = new UpdateProfileVM()
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber
                };
                return View(vm);
            }
            return View(new UpdateProfileVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MyProfile(UpdateProfileVM model)
        {
            if (string.IsNullOrWhiteSpace(model.FirstName) || string.IsNullOrWhiteSpace(model.LastName) || string.IsNullOrWhiteSpace(model.Email))
                return View(model);

            var user = await _userDataService.GetUser(User.UserId());

            var request = new UpdateUserRequestModel()
            {
                UserId = User.UserId(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            user = await _userDataService.UpdateUser(request);
            
            if (model.NewPassword != null)
            {
                var updatePasswordRequest = new UpdateUserPasswordRequestModel()
                {
                    UserId = user.Id,
                    NewPassword = model.NewPassword
                };
                var success = await _userDataService.UpdateUserPassword(updatePasswordRequest);
            }

            await HttpContext.Authentication.SignOutAsync();
            await LogUserIn(user, false);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CheckEmailAvailable([FromBody] string email)
        {
            if (!IsValidEmailFormat(email))
                return Json(new { Available = false });

            var user = await _userDataService.GetUserByUsername(email);
            if (user == null || user.Id == User.UserId())
                return Json(new { Available = true });
            else
                return Json(new { Available = false });
        }


        #region PrivateFun
        private IEnumerable<Claim> GetUserClaims(User user)
        {
            var displayName = $"{user.FirstName} {user.LastName}";
            if (string.IsNullOrWhiteSpace(displayName))
                displayName = user.Email;

            var claims = new List<Claim>();

            claims.Add(new Claim(JwtClaimTypes.Email, user.Email));
            claims.Add(new Claim(JwtClaimTypes.GivenName, user.FirstName ?? ""));
            claims.Add(new Claim(JwtClaimTypes.FamilyName, user.LastName ?? ""));
            claims.Add(new Claim(JwtClaimTypes.Name, displayName));
            claims.Add(new Claim(AuthenticationClaims.UserIdClaim, user.Id.ToString()));
            claims.Add(new Claim(JwtClaimTypes.Role, user.Roles?.OrderBy(r => r.ID).FirstOrDefault()?.Name ?? ""));
            
            return claims;
        }

        private async Task LogUserIn(User user, bool rememberLogin)
        {
            // get claims
            var appClaims = GetUserClaims(user);
            var displayName = $"{user.FirstName} {user.LastName}";
            if (string.IsNullOrWhiteSpace(displayName))
                displayName = user.Email;

            var subject = (AuthenticationOptions.UserSubjectPrefix + user.Id.ToString()).Sha256();
            var props = (AuthenticationProperties)null;
            if (rememberLogin)
            {
                props = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.Add(AuthenticationOptions.RememberMeLoginDuration)
                };
            };

            await HttpContext.Authentication.SignInAsync(subject, displayName, props, appClaims.ToArray());            
        }

        private bool IsValidEmailFormat(string email)
        {
            if (email.Length > 100)
                return false;

            // pattern from: http://www.rhyous.com/2010/06/15/regular-expressions-in-cincluding-a-new-comprehensive-email-pattern/
            string pattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,15})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
            Regex rex = new Regex(pattern);
            if (email != rex.Match(email).Value)
                return false;
            
            return true;
        }
        #endregion  

    }
}
