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
      
        
        [HttpGet]
        public async Task<IActionResult> TimerTesting()
        {
            var timer = new Stopwatch();
            int counter = 0;
            timer.Start();

            List<List<int>> fullList;
            if (!_memoryCache.TryGetValue("fullList", out fullList))                           
                return Json("try again");

            var used = new List<int>(){ 1, 3, 6, 9, 12, 15, 17, 20, 22, 25, 28, 29, 30, 31, 35, 40, 41, 46, 48, 50};
            //var result = fullList.Where(l => !l.Intersect<int>(used).Any());    // 1.1 - 1.3 seconds
            var result = fullList.Where(l => !HasMatch(l, used));


            counter = result.Count();
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
        private List<List<int>> GenFullListFromCodes(int max = 52)
        {
            var totalCombs = (int)_calc.nCr(max, 5);
            var codes = new List<int>(totalCombs - 1);
            for (int i = 0; i < totalCombs; i++)
                codes.Add(i);

            var decoded = DecodeCombs(codes, 5, max - 1);
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
