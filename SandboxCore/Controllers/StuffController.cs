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

        [HttpGet, Route("testing/MyForm/")]
        public IActionResult MyForm()
        {
            var vm = new List<TestViewModel>()
            {
                new TestViewModel()
                {
                    Id = 1,
                    Name = "A"
                },
                new TestViewModel()
                {
                    Id = 2,
                    Name = "B"
                },
                new TestViewModel()
                {
                    Id = 3,
                    Name = "C"
                },
                new TestViewModel()
                {
                    Id = 4,
                    Name = "D"
                },
                new TestViewModel()
                {
                    Id = 5,
                    Name = "E"
                }
            };
            return View(vm);
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
            for (var i = 0; i < max; i++)
            {
                var result = _calc.DecodePermutation(new BigInteger(i), num);
            }
            timer.Stop();
            return Json(new { timer = timer.ElapsedMilliseconds });
        }

        [HttpPost, Route("stuff/test/{id:int=0}")]
        public IActionResult Submit(int id)
        {

            return RedirectToAction("Myform");
        }

        [HttpGet, Route("stuff/carsncolors")]
        public IActionResult CarsnColors()
        {
            var FooCollection = new List<FooViewModel>
            {
                new FooViewModel()
                {
                    CarName = "red accord",
                    Color = "red"
                },
                new FooViewModel()
                {
                    CarName = "red ford",
                    Color = "red"
                },
                new FooViewModel()
                {
                    CarName = "red dodge",
                    Color = "red"
                },
                new FooViewModel()
                {
                    CarName = "blue mustang",
                    Color = "blue"
                },
                new FooViewModel()
                {
                    CarName = "blue ranchero",
                    Color = "blue"
                },
                new FooViewModel()
                {
                    CarName = "blue escort",
                    Color = "blue"
                },
                new FooViewModel()
                {
                    CarName = "blue chevy",
                    Color = "blue"
                },
                new FooViewModel()
                {
                    CarName = "green pinto",
                    Color = "green"
                },
                new FooViewModel()
                {
                    CarName = "green acura",
                    Color = "green"
                },
                new FooViewModel()
                {
                    CarName = "yellow Mitch",
                    Color = "yellow"
                },
                new FooViewModel()
                {
                    CarName = "yellow Beauford",
                    Color = "yellow"
                },
                new FooViewModel()
                {
                    CarName = "yellow Jethro",
                    Color = "yellow"
                }
            };


            return View(FooCollection);
        }

        [HttpPost, Route("stuff/cars/{color}")]
        public IActionResult Cars(string color, List<FooViewModel> cars)
        {
            // do stuff...

            return View();
        }

        [HttpPost, Route("stuff/cars/")]
        public IActionResult Cars2([FromBody] List<FooViewModel> cars)
        {
            // do stuff...

            return View();
        }

        [HttpGet, Route("stuff/radio")]
        public IActionResult Radio()
        {
            return View();
        }

        [HttpGet, Route("stuff/iframe")]
        public IActionResult iframe()
        {
            return View();
        }
    }
}
