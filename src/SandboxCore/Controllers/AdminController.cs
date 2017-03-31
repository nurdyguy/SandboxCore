using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using SandboxCore.Identity.Dapper.Entities;
using SandboxCore.Authorization;
using SandboxCore.Identity.Models;
using SandboxCore.Models.AccountViewModels;
using SandboxCore.Services;
using SandboxCore.Identity.Managers;

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Microsoft.Extensions.Caching.Memory;

namespace SandboxCore.Controllers
{
    [Authorize("Admin")]
    public class AdminController : Controller
    {
        private readonly UserDataService _userDataService;
        private IMemoryCache _memoryCache;

        public AdminController(UserDataService userDataService, IMemoryCache memCache)
        {
            _userDataService = userDataService;
            _memoryCache = memCache;
        }
      
        [HttpGet, Route("Admin/UserManagement")]
        public async Task<IActionResult> UserManagement()
        {
            var owners = await _userDataService.GetUsersInRoleAsync("Owner");
            var admins = await _userDataService.GetUsersInRoleAsync("Admin");
            var users = (await _userDataService.GetUsersInRoleAsync("User")).ToList();

            var norole = await _userDataService.GetAllUsersWithoutRoles();
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
            
            var allRoles = new List<string>() { "Owner", "Admin", "User" };
            try
            {
                var tasks = new List<Task>();
                tasks.AddRange(request.NewOwners.Select(o => ProcessRoleChange(o, "Owner", allRoles)));
                tasks.AddRange(request.NewAdmins.Select(a => ProcessRoleChange(a, "Admin", allRoles)));
                tasks.AddRange(request.NewUsers.Select(u => ProcessRoleChange(u, "User", allRoles)));

                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                success = false;
                message = ex.Message;
            }

            return Json(new { Success = success, Message = message });
        }
        

        private async Task ProcessRoleChange(int userId, string newRole, List<string> allRoles)
        {
            var user = await _userDataService.FindByIdAsync(userId.ToString());

            if (user.Id == 0)
                return;

            var currRoles = await _userDataService.GetRolesAsync(user);
            var toRemove = currRoles.Where(cr => allRoles.Contains(cr));

            await Task.WhenAll(toRemove.Select(r => _userDataService.RemoveFromRoleAsync(user, r)));

            await _userDataService.AddToRoleAsync(user, newRole);
        }
        
    }
}
