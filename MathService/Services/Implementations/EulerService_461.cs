using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Linq;
using MathService.Services.Contracts;
using MathService.Models.EulerModels;
using MathService.Models;
using System.Diagnostics;

namespace MathService.Services.Implementations
{
    public partial class EulerService : IEulerService    
    {
        //https://projecteuler.net/problem=461 difficulty: 30%
        //
        //Let f(n,k) = e^(k/n) - 1, for all non-negative integers k.
        //
        //Remarkably, f(200,6) + f(200,75) + f(200,89) + f(200,226) = 3.141592644529… ≈ π.
        //
        //In fact, it is the best approximation of π of the form f(n,a) + f(n,b) + f(n,c) + f(n,d) for n = 200.
        //
        //Let g(n) = a^2 + b^2 + c^2 + d^2 for a, b, c, d that minimize the error: | f(n,a) + f(n,b) + f(n,c) + f(n,d) - π|
        //(where |x| denotes the absolute value of x).
        //
        //You are given g(200) = 62 + 752 + 892 + 2262 = 64658.
        //
        //Find g(10000).


        //-----------------------------------------------------------------------------------
        //Notes:
        // 1.  Find d, the largest, so that e^(d/n) - 1 is closest to pi --- d ~ n*ln(pi + 1)
        // 2.  Repeat for c based on pi - e^(d/n) - 1
        // 
        // 

        public object RunProblem461(int num)
        {
            var total = (ulong)0;

            var dmax = FindNextLargestNum(num, 0, 0, 0);
            var closest = new Closest()
            {
                nums = new int[4] { dmax, 1, 1, 1 },
                error = Math.Abs(CalcTotal(num, dmax, 1, 1, 1) - Math.PI)
            };

            // i, j, k, l in descending order
            // dmax must be at least 1/4 of total
            var level1Min = GetLevelMin(num, 0, 0);
            for (var i = dmax; i > level1Min; i--)
            {
                var cmax = Math.Min(FindNextLargestNum(num, i, 0, 0), i);
                // cmax must be at least 1/3 of remaining
                for (var j = GetLevelMin(num, i, 0); j <= cmax; j++)
                {
                    var bmax = Math.Min(FindNextLargestNum(num, i, j, 0), j);
                    // bmax must be at least 1/2 of remaining
                    for (var k = GetLevelMin(num, i, j); k <= bmax; k++)
                    {
                        var amax = FindNextLargestNum(num, i, j, k);
                        
                        var test = Math.Abs(CalcTotal(num, i, j, k, amax) - Math.PI);
                        if (test < closest.error)
                        {
                            closest.nums[3] = i;
                            closest.nums[2] = j;
                            closest.nums[1] = k;
                            closest.nums[0] = amax;
                            closest.error = test;
                        }

                        // need to test amax + 1?
                        //var test = Math.Abs(CalcTotal(num, i, j, k, amax) - Math.PI);
                        //if (test < closest.error)
                        //{
                        //    closest.nums[3] = i;
                        //    closest.nums[2] = j;
                        //    closest.nums[1] = k;
                        //    closest.nums[0] = l;
                        //    closest.error = test;
                        //}

                    }
                }
            }

            return closest;
        }

        // result so that e^(x/num) - 1 ~ approx
        // ~ num*ln(approx + 1)
        private int FindNextLargestNum(int num, int x, int y, int z)
        {
            var currApprox = Math.Abs(CalcTotal(num, x, y, z, 0) - Math.PI);

            return (int)(num * Math.Log(currApprox + 1));
        }
        
        private double CalcTotal(int num, int a, int b, int c, int d)
        {
            return Math.Pow(Math.E, (double)a / num) 
                    + Math.Pow(Math.E, (double)b / num) 
                    + Math.Pow(Math.E, (double)c / num)
                    + Math.Pow(Math.E, (double)d / num) - 4;
        }

        private int GetLevelMin(int num, int x, int y)
        {
            var currApprox = Math.Abs(CalcTotal(num, x, y, 0, 0) - Math.PI);
            if(x == 0)
                return (int)(num * Math.Log(currApprox / 4 + 1));
            if (y == 0)
                return (int)(num * Math.Log(currApprox / 3 + 1));
            
            return (int)(num * Math.Log(currApprox / 2 + 1));
        }

        private class Closest
        {
            public int[] nums { get; set; }
            public double error { get; set; }
        }
    }    
}
