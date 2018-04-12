using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using MathService.Repositories.Contracts;
using System.Collections;

namespace MathService.Calculators
{
    public partial class Calculator 
    {
        public bool IsPrime(ulong num)
        {
            return _primeRepo.IsPrime((int)num);
        }

        public bool IsPrime(int num)
        {
            return _primeRepo.IsPrime((int)num);
        }

        public int GetPrime(int index)
        {
            return _primeRepo.GetPrime(index);
        }
        public List<int> GetAllPrimes(int max)
        {
            return _primeRepo.GetAllPrimes(max);
        }
        public List<int> GetAllPrimes(long max)
        {
            return _primeRepo.GetAllPrimes(max);
        }
        public List<int> GetAllPrimes(ulong max)
        {
            return _primeRepo.GetAllPrimes(max);
        }
        public List<int> GetFirstNPrimes(int n)
        {
            return _primeRepo.GetFirstNPrimes(n);
        }

        public BitArray GetPrimeBitArray(int length)
        {
            return _primeRepo.GetPrimeBitArray(length);
        }
        public List<bool> SieveOfErat(int max)
        {
            return _primeRepo.SieveOfErat(max);
        }

        public ulong GetPrimeCount(int max)
        {
            return _primeRepo.GetPrimeCount(max);
        }
        public ulong GetPrimeCount(long max)
        {
            return _primeRepo.GetPrimeCount(max);
        }

        public ulong GetPrimeCount(ulong max)
        {
            return _primeRepo.GetPrimeCount(max);
        }
    }
}
