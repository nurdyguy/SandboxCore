using System;
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
        private readonly string _primesFile_1 = "../MathService/Repositories/Constants/_primes_1.txt";
        private readonly int[] _primes;
        private readonly int _maxPrime;

        private bool[] _boolPrimes;

        public PrimeRepository()
        {
            HashSet<int> primeHashSet = new HashSet<int>(
            File.ReadLines(_primesFile_1)
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
            if (index < _primes.Length)
                return _primes[index];
            else
                return -1;
           
        }

        public List<int> GetAllPrimes(int max)
        {
            return _primes.Where(p => p <= max).ToList();
        }

        public List<int> GetAllPrimes(long max)
        {
            return _primes.Where(p => p <= max).ToList();
        }

        public List<int> GetAllPrimes(ulong max)
        {
            return _primes.Where(p => (ulong)p <= max).ToList();
        }

        public List<int> GetFirstNPrimes(int n)
        {
            var primes = new List<int>(_primes.Take(n));
            return primes;
        }

        public bool[] SieveOfErat(int max)
        {
            var nums = new bool[max];
            nums[0] = false; nums[1] = false;
            for (var i = 2; i < nums.Count(); i++)
                nums[i] = true;

            for (var j = 2; j * 2 < nums.Count(); j++)
                nums[j * 2] = false;
            for (var j = 2; j * 3 < nums.Count(); j++)
                nums[j * 3] = false;
            
            var maxCheck = (int)Math.Sqrt(nums.Count());
            for (var i = 5; i < maxCheck; i++)            
                if (nums[i])
                    for (var j = 2; j * i < nums.Count(); j++)
                        nums[j * i] = false;                
            
            
            //for (var i = 6; i < maxCheck; i+=6)
            //{
            //    if (nums[i - 1])
            //    {
            //        var check = i - 1;
            //        for (var j = 2; j * check < nums.Count(); j++)
            //            nums[j * check] = false;
            //    }

            //    if (nums[i + 1])
            //    {
            //        var check = i + 1;
            //        for (var j = 2; j * check < nums.Count(); j++)
            //            nums[j * check] = false;
            //    }
            //}
            return nums;
        }

        private void CompressBoolArray()
        {
            //var compressed = new bool[1000000];
        }
    }
}
