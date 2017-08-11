using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using AutoMapper;

using AccountService.Services.Contracts;
using AccountService.Services.Implementations;
using AccountService.Models;

using SandboxCore.Authentication;
using SandboxCore.Models.AccountViewModels;


namespace SandboxCore.Controllers
{
    [Authorize("Admin")]
    public class AdminController : Controller
    {
        private readonly IUserDataService _userDataService;
        private IMemoryCache _memoryCache;

        public AdminController(IUserDataService userDataService, IMemoryCache memCache)
        {
            _userDataService = userDataService;
            _memoryCache = memCache;
        }

        [HttpGet, Route("Admin/UserManagement")]
        public async Task<IActionResult> UserManagement()
        {
            var owners = await _userDataService.GetUsersByRole(Role.Owner);
            var admins = await _userDataService.GetUsersByRole(Role.Admin);
            var users = (await _userDataService.GetUsersByRole(Role.User)).ToList();

            var norole = await _userDataService.GetUsersWithoutRole();
            users.AddRange(norole);

            var vm = new UserManagementViewModel()
            {
                Owners = Mapper.Map<List<UserViewModel>>(owners),
                Admins = Mapper.Map<List<UserViewModel>>(admins),
                Users = Mapper.Map<List<UserViewModel>>(users),
            };


            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesViewModel request)
        {
            var success = true;
            var message = "";

            if (!User.IsOwner())
            {
                request.NewOwners = new List<int>();
                message = "Admins cannot create Owners!";
            }

            var allRoles = new List<Role>() { Role.Owner, Role.Admin, Role.User };
            try
            {
                var tasks = new List<Task>();
                //allRoles.ForEach(r => tasks.AddRange(request.NewOwners.Select(o => ProcessRoleChange(o, r, allRoles))));
                tasks.AddRange(request.NewOwners.Select(o => ProcessRoleChange(o, Role.Owner, allRoles)));
                tasks.AddRange(request.NewAdmins.Select(a => ProcessRoleChange(a, Role.Admin, allRoles)));
                tasks.AddRange(request.NewUsers.Select(u => ProcessRoleChange(u, Role.User, allRoles)));

                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                success = false;
                message = ex.Message;
            }

            return Json(new { Success = success, Message = message });
        }

        private async Task ProcessRoleChange(int userId, Role newRole, List<Role> allRoles)
        {
            var user = await _userDataService.GetUser(userId);

            if (user.UserId == 0)
                return;

            var currRoles = user.Roles;
            var toRemove = currRoles.Where(cr => allRoles.Contains(cr));

            await Task.WhenAll(toRemove.Select(r => _userDataService.RemoveUserFromRole(user.UserId, r.ID)));

            await _userDataService.AddUserToRole(user.UserId, newRole.ID); 
    }
    }
}
