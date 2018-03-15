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
        //
        //Problem 501 ---- Eight Divisors
        //The eight divisors of 24 are 1, 2, 3, 4, 6, 8, 12 and 24. 
        //The ten numbers not exceeding 100 having exactly eight divisors 
        //are 24, 30, 40, 42, 54, 56, 66, 70, 78 and 88. Let f(n) be the count of numbers not exceeding n with exactly eight divisors.
        //You are given f(100) = 10, f(1000) = 180 and f(10^6) = 224427.
        //Find f(10^12).
		//
		//
		//Notes:  
		//8 divisors means p7 or p3*p1 or p1*p1*p1
		//all p7 				all primes p < seventh root of 10^x -- super easy
		//all p3*p1				all first all p < third root of 10^x, times all p < 10^x / p3 excluding p3 itself (counted in p7) -- still pretty easy
		//all p1*p1*p1

        public object RunProblem501(int exp)
        {
            _calc.GetPrime(5);
            return BigInteger.One;
        }


        
    }
}
