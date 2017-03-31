using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using models = SandboxCore.Identity.Models;
using SandboxCore.Identity.Managers;

namespace SandboxCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserDataService _userDataService;
        private readonly SignInManager<models.User> _signInManager;

        public HomeController(
            UserDataService userDataService,
            SignInManager<models.User> signInManager
            )
        {
            _userDataService = userDataService;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = new models.User();
            if (User.Identity.IsAuthenticated)
                user = await _userDataService.FindByNameAsync(User.Identity.Name);

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }


        
    }
}
