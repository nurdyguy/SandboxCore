using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using SandboxCore.Models;
using SandboxCore.Filters;
using _calc = MathService.Calculators.Calculator;


namespace SandboxCore.Controllers
{
    public class StuffController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        [TestActionFilter]
        [HttpGet, Route("testing/MyForm/{code}")]
        public IActionResult MyForm(string code)
        {
            return View();
        }

        [HttpPost, Route("testing/MyForm/")]
        public IActionResult MyForm()
        {

            return View();
        }

        [HttpGet, Route("testing/test")]
        public IActionResult Test()
        {
            var vm = new TestViewModel()
            {
                ResultMessage = new ResultMessage()
                {
                    IsError = false,
                    Message = "Your Face",
                    ShowMessage = false
                },
                Strings = new List<string>()
                {
                    "text1", "text2", "text3"
                }
            };

            ViewData["stuff"] = vm;
            return View();
        }


        [HttpGet, Route("testing/dictionary")]
        public IActionResult DictionaryTest()
        {
            return View();
        }

        [HttpPost, Route("testing/dictionary")]
        public IActionResult DictionaryTest(DictionaryViewModel model)
        {

            return new JsonResult(model.Dictionary);
        }

        [HttpGet, Route("testing/forceWait/{span:int}")]
        public IActionResult ForceWait(int span)
        {
            if (span == 0)
                span = 10;
            span *= 1000;

            Thread.Sleep(span);

            return Ok();
        }

        [HttpGet]
        public IActionResult Parallax()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Promises()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Sounds()
        {
            return View();
        }

        [HttpGet, Route("stuff/printer/{num:int}")]
        public IActionResult printer(int num = 10)
        {
            var timer = new Stopwatch();
            timer.Start();
            var max = _calc.Factorial(num);
            for(var i = 0; i < max; i++)
            {
                var result = _calc.DecodePermutation(new BigInteger(i), num);                
            }
            timer.Stop();
            return Json(new { timer = timer.ElapsedMilliseconds });
        }

    }
}
