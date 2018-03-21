﻿using System;
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
        List<int> GetFirstNPrimes(int n);

        List<bool> SieveOfErat(int max);
    }
}
