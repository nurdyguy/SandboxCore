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
        //https://projecteuler.net/problem=207 difficulty: 40%
        //
        //For some positive integers k, there exists an integer partition of the form   4^t = 2^t + k,
        //where 4t, 2t, and k are all positive integers and t is a real number.

        //The first two such partitions are 4^1 = 2^1 + 2 and 4^1.5849625... = 2^1.5849625... + 6.

        //Partitions where t is also an integer are called perfect.
        //For any m ≥ 1 let P(m) be the proportion of such partitions that are perfect with k ≤ m.
        //Thus P(6) = 1/2.

        //In the following table are listed some values of P(m)

        //   P(5) = 1/1
        //   P(10) = 1/2
        //   P(15) = 2/3
        //   P(20) = 1/2
        //   P(25) = 1/2
        //   P(30) = 2/5
        //   ...
        //   P(180) = 1/4
        //   P(185) = 3/13

        // Find the smallest m for which P(m) < 1/12345                                                                                                                                                                                                                            

        //-----------------------------------------------------------------------------------
        //Notes:
        // 1.  if 4^t - 2^t = k --- list all possibilities 2^t = 2, 3, 4, ....  
        // 2.  when 2^t is a power of 2 then we are "perfect"
        // 3.  k = (2^t)^2 - 2^t
        // 4.  close to 50000000000

        public object RunProblem207(long goalDenom)
        {
            var stop = 1 / (double)goalDenom;
            long min = 10;
            long max = 50000000000;
            var result = new PartitionResult();
            while(max - min > 1)
            {
                var mid = (max + min) / 2;
                result = CalcP(mid);
                if ((double)result.PerfectCounter / result.TotalCounter >= stop)
                    min = mid;
                else
                    max = mid;

            }
            return new { result, max };
        }
        
        private PartitionResult CalcP(long max)
        {
            var perfCounter = 0;
            long k = 2;

            for(k = 2; k* k - k <= max; k++)
            {
                if (_twoPowers.Contains((ulong) k))
                    perfCounter++;
                
            }
            return new PartitionResult{ PerfectCounter = perfCounter, TotalCounter = k - 2 };
        }
        
        private class PartitionResult
        {
            public long PerfectCounter { get; set; }
            public long TotalCounter { get; set; }
        }
    }    
}
