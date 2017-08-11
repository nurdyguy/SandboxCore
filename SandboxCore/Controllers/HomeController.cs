using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SandboxCore.Models;
using IdentityServer4.Models;

namespace SandboxCore.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
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

        [Route("DevError/{code:int}")]
        public IActionResult DevError(int code)
        {
            var vm = new ErrorVM()
            {
                Error = new ErrorMessage()
                {
                    Error = code.ToString()
                }
            };
            return View();
        }

        [Route("Error/{code:int}")]
        public IActionResult Error(int code)
        {
            return View();
        }

        [Route("Error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
