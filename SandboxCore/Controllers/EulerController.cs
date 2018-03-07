﻿using System;
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

        [HttpGet, Route("Euler/482/{max:int}")]
        public async Task<IActionResult> Problem482(int max = 5)
        {
            var watch = new Stopwatch();
            var timers = new List<double>();
            watch.Start();

            var result = _eulerService.RunProblem482(max);


            timers.Add(watch.ElapsedMilliseconds / 1000.0);
            watch.Stop();
            return Json(new { timers, Result = result });
        }

        [HttpGet, Route("Euler/483/{max:int}")]
        public async Task<IActionResult> Problem483(int max = 5)
        {
            var watch = new Stopwatch();
            var timers = new List<double>();
            watch.Start();

            var result = _eulerService.RunProblem483(max);


            timers.Add(watch.ElapsedMilliseconds / 1000.0);
            watch.Stop();
            return Json(new { timers, Result = result });
        }

        [HttpGet, Route("Euler/500/{max:int}")]
        public async Task<IActionResult> Problem500(int max = 5)
        {
            var watch = new Stopwatch();
            var timers = new List<double>();
            watch.Start();reader();

            var result = _eulerService.RunProblem500(max);


            timers.Add(watch.ElapsedMilliseconds / 1000.0);
            watch.Stop();
            return Json(new { timers, Result = result });
        }

        [HttpGet, Route("Euler/566/{x:int}/{y:int}/{z:int}")]
        public async Task<IActionResult> Problem566(int x, int y, int z)
        {
            var watch = new Stopwatch();
            var timers = new List<double>();

            var result = _eulerService.RunProblem566(x, y, z);


            timers.Add(watch.ElapsedMilliseconds / 1000.0);
            watch.Stop();
            return Json(new { timers, result });
        }

        [HttpGet, Route("Euler/590/{max:int}")]
        public async Task<IActionResult> Problem590(int max)
        {
            var watch = new Stopwatch();
            var timers = new List<double>();
            watch.Start();

            var result = _eulerService.RunProblem590(max);


            timers.Add(watch.ElapsedMilliseconds / 1000.0);
            watch.Stop();
            return Json(new { timers, Result = result.ToString() });
        }


    }
}
