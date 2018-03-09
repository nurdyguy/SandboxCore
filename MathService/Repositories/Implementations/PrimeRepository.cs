using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MathService.Repositories.Contracts;
using MathService.Repositories.Constants;

namespace MathService.Repositories.Implementations
{
    public class PrimeRepository : Primes_, IPrimeRepository
    {
        
        //public static void InitPrimes()
        //{
        //    var primes = new SortedList<ulong, ulong>();
        //    for (var i = 2; (ulong)i <= _maxPrime; i++)
        //        if (IsPrime(i))
        //            primes.Add((ulong)i, (ulong)i);
        //    _primes = primes;
        //}

        public bool IsPrime(ulong num)
        {
            var x = _primes[0][10];
            var y = _primes_1[10];

            //// if in list
            //if (num < _maxPrime)
            //    return _primes.Any((int)num);

            //// divisible by a known prime
            //foreach(var i in _primes.Keys)
            //    if (num % i == 0)
            //        return false;

            //// long check starting after end of primes list
            //ulong sqrt = (ulong)System.Math.Sqrt((double)num);
            //for (ulong i = _maxPrime + 1; i <= sqrt; i++)
            //    if (num % i == 0)
            //        return false;

            return true;
        }

        public bool IsPrime(long num)
        {
            return IsPrime((ulong)num);
        }

        public bool IsPrime(int num)
        {
            return IsPrime((ulong)num);
        }

        public int GetPrime(int index)
        {
            //if (index < _primes.Length)
            try
            {
                return 20;// _primes[0][0];
            }
            catch(Exception ex)
            {
                return -1;
            }
           
        }

        public List<int> GetAllPrimes(int max)
        {
            //var primes = new List<int>(_primes.Where(p => p <= max));
            return new List<int>();// _primes[0];
        }

        public List<int> GetFirstNPrimes(int n)
        {
            // var primes = new List<int>(_primes.Take(n));
            return new List<int>();// _primes[0];
        }
    }
}
