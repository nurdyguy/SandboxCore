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
    }
}
