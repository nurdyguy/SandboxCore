using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SandboxCore.Models;
using IdentityServer4.Models;

namespace SandboxCore.Controllers
{
    [AllowAnonymous]
    public class ReactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        
    }
}
