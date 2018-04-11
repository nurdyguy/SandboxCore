using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Linq;
using MathService.Services.Contracts;
using MathService.Models.EulerModels;
using MathService.Models;


namespace MathService.Services.Implementations
{
    public partial class EulerService : IEulerService    
    {
        //https://projecteuler.net/problem=501

        //The eight divisors of 24 are 1, 2, 3, 4, 6, 8, 12 and 24. 
        //The ten numbers not exceeding 100 having exactly eight divisors are 24, 30, 40, 42, 54, 56, 66, 70, 78 and 88. 
        //Let f(n) be the count of numbers not exceeding n with exactly eight divisors.
        //You are given f(100) = 10, f(1000) = 180 and f(10^6) = 224427.

        //Find f(10^12).

        //-----------------------------------------------------------------------------------
        //Notes:
        // Can have 8 divisors if p^7 or p^3*q^1 or p^1*q^1*r^1 where p, q, r all prime
        // 1.  Get total with p^7 so all primes where p^7 < 10^exp      => p < 7th root of 10^exp ---- very easy
        // 2.  Get total with p^3*q                                     => p < cube root of 10^exp times q < 10^exp/p^3 excluding p itself ---- still pretty easy
        // 3.  Get total with p*q*r                                     =>   ---- more complicated

        public object RunProblem501(int exp)
        {
            var total = (ulong)0;

            var singles =  GetSinglePrimeFactorCount(exp);
            var doubles =  GetTwoPrimeFactorsCount(exp);
            var triples =  GetThreePrimeFactorsCount(exp);

            return new { singles, doubles, triples };
        }
        
        private ulong GetSinglePrimeFactorCount(int exp)
        {
            var maxPrime = Math.Pow(Math.Pow(10, exp), 1.0/7.0);
            return _calc.GetPrimeCount((long)maxPrime);
        }

        //p^3 * q
        private ulong GetTwoPrimeFactorsCount(int exp)
        {
            var count = (ulong)0;
            var max = Math.Pow(10, exp);
            var pMax = Math.Pow(max, 1.0 / 3.0);
            var pPrimes = _calc.GetAllPrimes((int)pMax);

            for (var i = 0; i < pPrimes.Count; i++)
            {
                var qMax = (ulong)(max / Math.Pow(pPrimes[i], 3));
                count += _calc.GetPrimeCount(qMax);
                if (pPrimes[i] <= _calc.GetPrime(i))
                    count--;
            }
            

            return count;
        }

        private ulong GetThreePrimeFactorsCount(int exp)
        {
            var count = (ulong)0;
            
            return count;
        }

    }
}
