using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MathService.Repositories.Contracts
{
    public interface IPrimeRepository
    {
        //void InitPrimes();

        bool IsPrime(ulong num);
        bool IsPrime(int num);

        int GetPrime(int index);
        List<int> GetAllPrimes(int max);
        List<int> GetAllPrimes(long max);
        List<int> GetAllPrimes(ulong max);
        List<int> GetFirstNPrimes(int n);
        ulong GetPrimeCount(int max);
        ulong GetPrimeCount(long max);
        ulong GetPrimeCount(ulong max);
        BitArray SieveOfErat(long max);        
        BitArray GetPrimeBitArray(int length);
        List<bool> SieveOfErat(int max);
    }
}
