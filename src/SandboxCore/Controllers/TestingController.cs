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
using System.Collections.Concurrent;


using _calc = SandboxCore.Math.CombinationsCalculator;

namespace SandboxCore.Controllers
{
    [AllowAnonymous]
    public class TestingController : Controller
    {
        private IMemoryCache _memoryCache;

        public TestingController(IMemoryCache memCache)
        {
            _memoryCache = memCache;
        }
      
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Index2()
        {
            if (Request.Headers.Keys.Contains("Referer"))
                ViewBag.Referer = HttpContext.Request.Headers["Referer"];
            else
                ViewBag.Referer = "";
            var x = string.IsNullOrEmpty(ViewBag.Referer);
            
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> TimerTesting()
        {
            var timer = new Stopwatch();
            int counter = 0;
            timer.Start();

            List<List<int>> fullList;
            //if (!_memoryCache.TryGetValue("fullList", out fullList))                           
                //return Json("try again");

            var used = new List<int>(){ 1, 3, 6, 17, 20, 22, 25, 30, 31, 35, 41, 46, 48, 50};

            var all = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30,
                                        31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51};

            var allSorted = new SortedList<int, int>() {
                {0, 0},{1, 1},{2, 2},{3, 3},{4, 4},{5, 5},{6, 6},{7, 7},{8, 8},{9, 9},
                {10, 10},{11, 11},{12, 12},{13, 13},{14, 14},{15, 15},{16, 16},{17, 17},{18, 18},{19, 19},
                {20, 20},{21, 21},{22, 22},{23, 23},{24, 24},{25, 25},{26, 26},{27, 27},{28, 28},{29, 29},
                {30, 30},{31, 31},{32, 32},{33, 33},{34, 34},{35, 35},{36, 36},{37, 37},{38, 38},{39, 39},
                {40, 40},{41, 41},{42, 42},{43, 43},{44, 44},{45, 45},{46, 46},{47, 47},{48, 48},{49, 49},{50, 50},{51, 51} };
            //var result = fullList.Where(l => !l.Intersect<int>(used).Any());    // 1.1 - 1.3 seconds
            //var result = fullList.Where(l => !HasMatch(l, used));


            for (var i = 0; i < 10000; i++)
            {
                // 10000x = .001s
                //var avail = all.Where(a => !used.Any(u => u == a));

                // 10000x = .16s
                //var avail = all.Where(a => !used.Any(u => u == a)).ToList();

                /*  10000x = .05s
                var avail = new List<int>(52);
                all.ForEach(a => { if (!used.Contains(a)) avail.Add(a); });
                */
                //var res = GenFullListFromCodes(avail.Count());
            }


            //counter = result.Count();
            timer.Stop();
            double elapsed = timer.ElapsedMilliseconds / 1000.0;
            return Json(new { elapsed, counter });
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
            var match = false;
            for(int i = 0; i < l1.Count; i++)
                for (int j = 0; j < l2.Count; j++)
                    if (l1[i] == l2[j])
                        return true;
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


        
    }
}
