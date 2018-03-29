using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MathService.Services.Contracts;

namespace SandboxCore.Controllers
{
    public class EulerController : Controller
    {
        private readonly IEulerService _eulerService;

        public EulerController(IEulerService eulerService)
        {
            _eulerService = eulerService;
        }

        public async Task<IActionResult> Index()
        {
           

            return View();
        }

        [HttpGet, 
        Route("Euler/{problemNumber:int}/{x:int}"),
        Route("Euler/{problemNumber:int}/{x:int}/{y:int}"),
        Route("Euler/{problemNumber:int}/{x:int}/{y:int}/{z:int}")]
        public async Task<IActionResult> Problem(int problemNumber, int x = 1, int y = 1, int z = 1)
        {
            var watch = new Stopwatch();
            var timers = new List<double>();
            watch.Start();

            object result;
            switch(problemNumber)
            {
                case 461:
                    result = _eulerService.RunProblem461(x);
                    break;
                case 482:
                    result = _eulerService.RunProblem482(x);
                    break;
                case 483:
                    result = _eulerService.RunProblem483(x);
                    break;
                case 500:
                    result = _eulerService.RunProblem500(x);
                    break;
                case 501:
                    result = _eulerService.RunProblem501(x);
                    break;
                case 504:
                    result = _eulerService.RunProblem504(x);
                    break;
                case 566:
                    result = _eulerService.RunProblem566(x, y, z);
                    break;
                case 590:
                    result = _eulerService.RunProblem590(x);
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }

            timers.Add(watch.ElapsedMilliseconds / 1000.0);
            watch.Stop();
            return Json(new { timers, Result = result });
        }
        
    }
}
