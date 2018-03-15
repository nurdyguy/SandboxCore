using System;
using System.Collections.Generic;
using System.Text;

using MathService.Repositories.Contracts;

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
        public List<int> GetFirstNPrimes(int n)
        {
            return _primeRepo.GetFirstNPrimes(n);
        }

        public bool[] SieveOfErat(int max)
        {
            return _primeRepo.SieveOfErat(max);
        }
    }
}
