using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Linq;
using MathService.Services.Contracts;
using MathService.Models.EulerModels;
using MathService.Models;
using System.Diagnostics;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace MathService.Services.Implementations
{
    public partial class EulerService : IEulerService    
    {
        //https://projecteuler.net/problem=569
        //Published on Sunday, 11th September 2016, 06:00 am; Solved by 282;
        //Difficulty rating: 45%
        //
        //A mountain range consists of a line of mountains with slopes of exactly 45°, and heights governed by the prime numbers, p_n.
        //The up-slope of the k-th mountain is of height p_2k−1, and the downslope is p2k.
        //The first few foot-hills of this range are illustrated below.
        //
        //Tenzing sets out to climb each one in turn, starting from the lowest. 
        //At the top of each peak, he looks back and counts how many of the previous peaks he can see. 
        //In the example above, the eye-line from the third mountain is drawn in red,
        //showing that he can only see the peak of the second mountain from this viewpoint.
        //Similarly, from the 9th mountain, he can see three peaks, those of the 5th, 7th and 8th mountain.
        //
        //Let P(k) be the number of peaks that are visible looking back from the k-th mountain. 
        //Hence P(3)=1 and P(9)=3.
        //
        //Also ∑k=1-100 of P(k)=227.
        //
        //Find ∑k=1-2500000 of P(k).
        //
        //-----Notes-----
        // 1. 
        // 2. 
        // 3. 
        //    
        // 4.                                   
        
        public object RunProblem569(int num)
        {
            // make primes 1-based
            if ((_primes?.Count() ?? 0) < num)
            {
                _primes = new List<int>(num + 3){ 0 };
                _primes.AddRange(_calc.GetFirstNPrimes(5000000));
            }

            // make peaks 1-based;
            var peaks = new List<Peak>(num + 1) { new Peak() { x = 0, y = 0 }, new Peak() { x = 2, y = 2 } };
            for (var i = 2; i <= num; i++)
            {
                var nextPeak = new Peak();
                nextPeak.x = peaks.Last().x + _primes[2 * i - 1] + _primes[2 * i - 2];
                nextPeak.y = peaks.Last().y + _primes[2 * i - 1] - _primes[2 * i - 2];
                nextPeak.IsTwin = _primes[2 * i - 1] - _primes[2 * i - 2] == 2;
                peaks.Add(nextPeak);
            }

            long count = 0;
            //for (var i = 2; i <= num; i++)
            //{
            //    count += CountVisiblePeaks(peaks, i);
            //}

            var counts = new ConcurrentBag<long>();
            Parallel.For(2, num + 1, i => counts.Add(CountVisiblePeaks(peaks, i)));
            count = counts.Sum();
            
            return count;
        }

        private int FindLeftMostVisiblePeak(List<Peak> peaks, int rightPeak)
        {
            var leftPeak = 1;
            var minSlope = CalcSlope(peaks[rightPeak], peaks[leftPeak]);
            for(var i = 2; i < rightPeak; i++)
            {
                var nextSlope = CalcSlope(peaks[rightPeak], peaks[i]);
                if(nextSlope < minSlope)
                {
                    minSlope = nextSlope;
                    leftPeak = i;
                }
            }
            
            return leftPeak;
        }

        private long CountVisiblePeaks(List<Peak> peaks, int rightPeak)
        {
            var count = (long)1;
            if (peaks[rightPeak].IsTwin)
                return count;
            var leftPeak = rightPeak - 1;
            var minSlope = CalcSlope(peaks[rightPeak], peaks[leftPeak]);
            var end = rightPeak / 2;
            for (var i = rightPeak - 2; i >= end; i--)
            {
                var nextSlope = CalcSlope(peaks[rightPeak], peaks[i]);
                if (nextSlope < minSlope)
                {
                    minSlope = nextSlope;
                    count++;              
                    if(peaks[i].IsTwin)
                    {
                        minSlope = CalcSlope(peaks[rightPeak], peaks[i - 1]);
                        count++;
                        i--;
                    }
                }
            }

            return count;
        }

        private double CalcSlope(Peak p1, Peak p2)
        {
            return (double)(p1.y - p2.y) / (p1.x - p2.x);
        }


        private class Peak
        {
            public long x { get; set; }
            public long y { get; set; }
            public bool IsTwin { get; set; }
        }
        
    }
}
