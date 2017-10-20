using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;



using System.Diagnostics;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;


using _Bcalc = MathService.Models.Constants.BigIntegerCalculator;
using _calc = MathService.Models.Constants.Calculator;
using System.Threading;
using MathService.Models.Constants;
using SandboxCore.Filters;
using System.Numerics;
using SandboxCore.Models;

namespace SandboxCore.Controllers
{
    [AllowAnonymous]
    public class TestingController : BaseController
    {
        private IMemoryCache _memoryCache;        

        public TestingController(IMemoryCache memCache)
        {
            _memoryCache = memCache;
        }
      
        public IActionResult Index()
        {
            //Debug.WriteLine("------------------------------------------------------------");
            //for (var i = 0; i < 200; i++)
            //{
            //    var line = "";
            //    for (var j = 1; j <= 5; j++)
            //        line += "\"" + _calc.Factorial(5*i + j) + "\", ";

            //    Debug.WriteLine(line);
            //}
            return Json(new { timer = (new System.Numerics.BigInteger(800)).Factorial() });
        }

        [Route("Calculator/{n}/choose/{r}")]
        public IActionResult CalcTest(string n, string r)
        {
            var _n = BigInteger.Parse(n);
            var _r = BigInteger.Parse(r);
            var watch = new Stopwatch();
            watch.Start();
            BigInteger result;
            for(var i = 0; i < 1000; i++)
                result = _Bcalc.nCr(_n, _r);
            watch.Stop();
            return Json(new { time = watch.ElapsedMilliseconds });
        }
        
        [HttpGet]
        public async Task<IActionResult> TimerTesting()
        {
            var watch = new Stopwatch();
            var timers = new List<double>();
            var counters = new List<int>();
            watch.Start();
            
            List<List<int>> fullList;
            if (!_memoryCache.TryGetValue("fullList", out fullList))
            {
                fullList = GenFullList();
                _memoryCache.Set("fullList", fullList);
            }
            //List<List<int>> fullList = GenFullListFromCodes();

            var used = new List<int>(){ 1, 3, 6, 17, 20, 22, 25, 30, 31, 35, 41, 46, 48, 50};

            var all = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30,
                                        31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51};

            //var avail = new List<int>(all);

            var allSorted = new SortedList<int, int>() {
                {0, 0},{1, 1},{2, 2},{3, 3},{4, 4},{5, 5},{6, 6},{7, 7},{8, 8},{9, 9},
                {10, 10},{11, 11},{12, 12},{13, 13},{14, 14},{15, 15},{16, 16},{17, 17},{18, 18},{19, 19},
                {20, 20},{21, 21},{22, 22},{23, 23},{24, 24},{25, 25},{26, 26},{27, 27},{28, 28},{29, 29},
                {30, 30},{31, 31},{32, 32},{33, 33},{34, 34},{35, 35},{36, 36},{37, 37},{38, 38},{39, 39},
                {40, 40},{41, 41},{42, 42},{43, 43},{44, 44},{45, 45},{46, 46},{47, 47},{48, 48},{49, 49},{50, 50},{51, 51} };
            //var result = fullList.Where(l => !l.Intersect<int>(used).Any());    // 1.1 - 1.3 seconds
            //var result = fullList.Where(l => !HasMatch(l, used));
            //counter = result.Count();

            timers.Add(watch.ElapsedMilliseconds / 1000.0);
            watch.Restart();
            var bag = new ConcurrentBag<int>();
            Parallel.ForEach(fullList, h =>
            {
                if (!HasMatch(h, used))
                {
                    bag.Add(1);
                }
            });
            counters.Add(bag.Count());


            timers.Add(watch.ElapsedMilliseconds / 1000.0);
            watch.Restart();
            var bag2 = new ConcurrentBag<int>();
            Parallel.ForEach(fullList, h =>
            {
                if (!HasMatchBinarySearch(h, used))
                {
                    //bag2.Add(1);
                }
            });
            counters.Add(bag2.Count());



            // testing fastest way to remove elements from a list
            //for (var i = 0; i < 10000; i++)
            {
                /* 10000x = .001s  1000000x = .05s 
                var avail = all.Where(a => !used.Any(u => u == a));
                */

                /* 10000x = .16s
                var avail = all.Where(a => !used.Any(u => u == a)).ToList();
                */

                /*  10000x = .05s
                var avail = new List<int>(52);
                all.ForEach(a => { if (!used.Contains(a)) avail.Add(a); });
                */

                //var avail = new List<int>(all);
                //used.ForEach(u => { if()})

                //var res = GenFullListFromCodes(avail.Count());

                /*  10000x = .011s
                used.ForEach(u => allSorted.Remove(u));
                */

                /*  10000x = .03s  
                all.RemoveAll(x => used.Contains(x));
                */

                //all = RemoveElementsBinary(all, used);
            }
            

            //counter = result.Count();            
            timers.Add(watch.ElapsedMilliseconds / 1000.0);
            watch.Stop();
            return Json(new { timers, counters });
        }

        [HttpGet]
        public async Task<IActionResult> LoopTester()
        {
            var watch = new Stopwatch();
            var timers = new List<double>();
            var counters = new List<int>();
            watch.Start();

            

            var intList = new List<int>();
            
            for(var i = 0; i < 2500000; i++)
            {
                intList.Add(i);   
            }
            
            var bagResult = new ConcurrentBag<RandResult>()
            {
                new RandResult() { id = 0, val = 0 },
                new RandResult() { id = 1, val = 0 },
                new RandResult() { id = 2, val = 0 },
                new RandResult() { id = 3, val = 0 },
                new RandResult() { id = 4, val = 0 }
            };

            var conDictResult = new ConcurrentDictionary<int, RandResult>();            
            conDictResult.GetOrAdd(0, new RandResult() { id = 0, val = 0 });
            conDictResult.GetOrAdd(1, new RandResult() { id = 1, val = 0 });
            conDictResult.GetOrAdd(2, new RandResult() { id = 2, val = 0 });
            conDictResult.GetOrAdd(3, new RandResult() { id = 3, val = 0 });
            conDictResult.GetOrAdd(4, new RandResult() { id = 4, val = 0 });

            var intDict = new ConcurrentDictionary<int, int>();

            var listResult = new List<RandResult>()
            {
                new RandResult() { id = 0, val = 0 },
                new RandResult() { id = 1, val = 0 },
                new RandResult() { id = 2, val = 0 },
                new RandResult() { id = 3, val = 0 },
                new RandResult() { id = 4, val = 0 }
            };

            var arrayResult = new RandResult[5]
            {
                new RandResult() { id = 0, val = 0 },
                new RandResult() { id = 1, val = 0 },
                new RandResult() { id = 2, val = 0 },
                new RandResult() { id = 3, val = 0 },
                new RandResult() { id = 4, val = 0 }
            };

            //timers.Add(watch.ElapsedMilliseconds / 1000.0);
            watch.Restart();
            Parallel.ForEach(intList, i =>
            {
                //conDictResult[i % 5].val++;                
                //conDictResult.TryUpdate(i%5, )
                //conDictResult.AddOrUpdate(i%5, new RandResult() { id = i%5, val = 1 }, (key, value) => value );       //---- ~500ms
                intDict.AddOrUpdate(i % 5, 1, (key, value) => value++);         //---- ~400ms
            });
            timers.Add(watch.ElapsedMilliseconds / 1000.0);

            //watch.Restart();
            //var _lock = new object();
            //Parallel.ForEach(intList, i =>
            //{
            //    lock (_lock)
            //    {
            //        listResult[i % 5].val++;

            //    }
            //});
            //timers.Add(watch.ElapsedMilliseconds / 1000.0);

            watch.Restart();
            var dict = new Dictionary<int, int>()
            {
                { 0, 0 },{ 1, 1 },{ 2, 2 },{ 3, 3 },{ 4, 4 }
            };
            int[] indexes = new int[5] { 0, 1, 2, 3, 4 };
            int[] ints = new int[5] { 0, 0, 0, 0, 0 };
            Parallel.ForEach(intList, i =>
            {
                //Interlocked.Increment(ref ints[i % 5]);
                //var k = dict.First(kvp => kvp.Value == i % 5).Key;---350ms
                //var k = indexes[i % 5];                    //---150ms
                //dict.TryGetValue(i % 10, out int k);      //---200ms
                Interlocked.Increment(ref ints[indexes[i % 5]]);
            });
            timers.Add(watch.ElapsedMilliseconds / 1000.0);

            //watch.Restart();
            //intList.ForEach(i =>
            //{
            //    listResult.First(b => b.id == i % 5).val++;
            //});
            //timers.Add(watch.ElapsedMilliseconds / 1000.0);

            watch.Stop();
            return Json(new { timers, counters });
        }

        [HttpGet, Route("testing/getall/{max:int}")]
        public async Task<IActionResult> GetAll(int max = 52)
        {
            //for (int i = 1; i <= max; i++)
            //{
            //    Debug.WriteLine($"//--------{i}----------");
            //    ulong val = 1;
            //    //Debug.WriteLine($"{i}c0 = {val}");
            //    var str = "{ 1";
            //    for (int j = 1; j <= i; j++)
            //    {
            //        val = _calc.nCr_next(i, j, val);
            //        str += $", {val}";
            //        //Debug.WriteLine($"{i}c{j} = {val}");
            //    }
            //    for (int k = i + 1; k <= 52; k++)
            //        str += ", 0";
            //    str += " },";
            //    Debug.WriteLine(str);
            //}
            //return Json(0);
            var timer = new Stopwatch();
            var timers = new List<double>();
            timer.Start();
            double elapsed = 0;
            List<List<int>> fullList;
            fullList = GenFullListFromCodes(max);


            //if (!_memoryCache.TryGetValue("fullList", out fullList))
            //{
            //    fullList = GenFullList(max);
            //    _memoryCache.Set("fullList", fullList);
            //}
            //timers.Add(timer.ElapsedMilliseconds / 1000.0);
            

            //var encoded = EncodeCombs(fullList, max - 1);

            //timers.Add(timer.ElapsedMilliseconds / 1000.0);

            //var decoded = DecodeCombs(encoded, 5, max - 1);
            
            timer.Stop();
            timers.Add(timer.ElapsedMilliseconds / 1000.0);
            int counter = fullList.Count;
            return Json(new { timers, counter });
        }

        [TestActionFilter]
        [HttpGet, Route("testing/MyForm/{code}")]
        public IActionResult MyForm(string code)
        {
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

        #region privateMethods


        // vs 52---14.2s
        // vs 47---7.9s
        // vs 42---4.3s
        // vs 37---2.1s
        // vs 35---1.5s
        // vs 34---1.3s
        // vs 33---1.1s
        // vs 32---0.95s
        private List<List<int>> GenFullList(int max = 52)
        {
            var fullList = new List<List<int>>();
            var allInts = new SortedList<int, int>();
            for (int i = 0; i < max; i++)
                allInts.Add(i, i);

            List<int> next = new List<int>(5);

            for (var i = 0; i < 5; i++)
            {
                next.Add(allInts[allInts.Keys[0]]);
                allInts.RemoveAt(0);
            }
            List<int> curr = new List<int>(next);

            while (next != null)
            {
                fullList.Add(new List<int>(next));
                for (var i = 0; i < 5; i++)
                {
                    curr[i] = next[i];
                    allInts.Remove(next[i]);
                };

                next = GetNext(next, allInts);
                curr.ForEach(c => allInts.Add(c, c));
            }
            return fullList;
        }

        // vs 52---4.7s
        // vs 42---1.2s
        // vs 32---0.2s
        private List<List<int>> GenFullListFromCodes(int max = 52, int combinationLength = 5)
        {
            var totalCombs = (int)_calc.nCr(max, combinationLength);
            var codes = new List<int>(totalCombs - 1);
            for (int i = 0; i < totalCombs; i++)
                codes.Add(i);

            var decoded = DecodeCombs(codes, combinationLength, max - 1);
            return decoded;
        }

        private List<int> GetNext(List<int> current, SortedList<int, int> available)
        {
            
            SortedList<int, int> allAvail = new SortedList<int, int>(available);
            current.ForEach(c => allAvail.Add(c, c));
            
            List<int> cIndexes = new List<int>();
            current.ForEach(c => cIndexes.Add(0));
            var length = current.Count;

            for (int i = length - 1; i >= 0; i--)
            {
                cIndexes[i] = allAvail.IndexOfKey(current[i]);

                if (cIndexes[i] == allAvail.Count - 2 - (length - i - 1))
                {
                    current[i] = allAvail[allAvail.Keys[cIndexes[i] + 1]];
                    return current;
                }
                if (cIndexes[i] < allAvail.Count - 2 - (length - i - 1))
                {
                    current[i] = allAvail[allAvail.Keys[cIndexes[i] + 1]];
                    // loop forward and assign tail
                    for (int j = i + 1; j < length; j++)
                    {
                        current[j] = allAvail[allAvail.Keys[cIndexes[i] + 1 + (j - i)]];
                    }
                    return current;
                }

            }

            return null;
        }

        // vs 4----0.71s
        // vs 7----0.92s
        // vs 10---1.18s ----- break even
        // vs 14---1.35s
        // vs 20---still 1.3ish!!!
        private bool HasMatch(List<int> l1, List<int> l2)
        {
            for(int i = 0; i < l1.Count; i++)
                for (int j = 0; j < l2.Count; j++)
                    if (l1[i] == l2[j])
                        return true;
            return false;
        }

        private bool HasMatchBinarySearch(List<int> l1, List<int> l2)
        {
            for (int i = 0; i < l1.Count; i++)
            {
                if (l2.BinarySearch(l1[i]) >= 0)
                    return true;
            }
            return false;
        }

        private List<int> EncodeCombs(List<List<int>> combs, int max = 51)
        {
            if (!combs.Any())
                return null;

            var l = new List<int>();
            combs.ForEach(c =>
            {
                l.Add(GetCombID(c, max));
            });

            return l;
        }

        // using parallel---max=52---5s
        private List<List<int>> DecodeCombs(List<int> codes, int combLength = 2, int max = 51)
        {
            if (!codes.Any())
                return null;
            var combs = new ConcurrentBag<List<int>>();
            Parallel.ForEach(codes, c =>
            {
                combs.Add(GetCombFromID(c, combLength, max));
            });
            return combs.ToList();
        }

        private int GetCombID(List<int> comb, int max = 51)
        {
            UInt64 id = _calc.nCr(max + 1, comb.Count);
            for (int i = 0; i < comb.Count; i++)
                id -= _calc.nCr(max - comb[i], comb.Count - i);
            return (int)id;
        }

        private List<int> GetCombFromID(int id, int combLength = 2, int max = 51)
        {
            List<int> comb = new List<int>(combLength);
            var tId = _calc.nCr(max + 1, combLength) - (UInt64)id;
            for (int i = combLength; i > 0; i--)
            {
                UInt64 tVal = 0;
                bool done = false;
                int pos = 0;
                while (!done)
                {
                    var t = _calc.nCr(max - pos, i);
                    if (t <= tId)
                    {
                        tVal = t;
                        done = true;
                    }
                    pos++;
                }
                tId -= tVal;
                comb.Add(pos - 1);
            }

            return comb;
        }

        private List<int> RemoveElementsBinary(List<int> list, List<int> toRemove)
        {
            for (var i = 0; i < toRemove.Count; i++)
            {
                bool done = false;
                if(list[0] == toRemove[i])
                {
                    //list.Remove()
                }
                while (!done)
                {
                    var first = list[0];
                    var last = list[list.Count - 1];
                    var mid = list[list.Count / 2];


                }
            }
            return list;
        }

        public class RandResult
        {
            public int id { get; set; }
            public int val { get; set; }
            
        }

        #endregion
    }
}
