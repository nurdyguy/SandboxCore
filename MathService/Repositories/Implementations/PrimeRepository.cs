﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

using MathService.Repositories.Contracts;


//using MathService.Repositories.Constants;

namespace MathService.Repositories.Implementations
{
    public class PrimeRepository : IPrimeRepository
    {
        private readonly string _primesFile_1 = "../MathService/Repositories/Constants/primes_1.txt";
        private readonly int[] _primes;
        private readonly int _maxPrime;

        public PrimeRepository()
        {
            HashSet<int> primeHashSet = new HashSet<int>(
            File.ReadLines(_primesFile_1)
                //.AsParallel() //maybe?
                .SelectMany(line => Regex.Matches(line, @"\d+").Cast<Match>())
                .Select(m => m.Value)
                .Select(int.Parse));

            //Predicate<int> isPrime = primeHashSet.Contains;

            _primes = primeHashSet.ToArray();
            _maxPrime = _primes[_primes.Count() - 1];            
        }
        
        public bool IsPrime(ulong num)
        {
            

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
            SieveOfErat(1000000000);
            if (index < _primes.Length)
                return _primes[index];
            else
                return -1;
           
        }

        public List<int> GetAllPrimes(int max)
        {
            return _primes.Where(p => p <= max).ToList();;
        }

        public List<int> GetFirstNPrimes(int n)
        {
            var primes = new List<int>(_primes.Take(n));
            return primes;
        }

        private void SieveOfErat(int max)
        {
            var nums = new bool[max];
            nums[0] = false; nums[1] = false;
            for (var i = 2; i < nums.Count(); i++)
                nums[i] = true;

            var maxCheck = (int)Math.Sqrt(nums.Count());
            for (var i = 2; i < maxCheck; i++)
                if (nums[i])
                    for (var j = 2; j*i < nums.Count(); j++)
                        nums[j*i] = false;
        }
    }
}
