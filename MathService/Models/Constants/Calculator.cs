using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MathService.Models.Constants
{
    public static class Calculator
    {


        //public static BigInteger nCr

        public static ulong nCr(int n, int r)
        {
            return nCr_Constants.nCr(n, r);
        }

        public static BigInteger nCr_BigInteger(int n, int r)
        {
            var bigN = new BigInteger(n);
            return bigN.nCr(r);
        }

        public static ulong partFactorial(int x, int y = 1)
        {
            if (x == 0 || x == y)
                return 1;
            if (y > x)
                return 0;
            UInt64 result = 1;
            for (var i = y + 1; i <= x; i++)
                result *= (UInt64)i;
            return result;
        }

        public static ulong nCr_next(int n, int r, ulong prev)
        {
            return (prev * (ulong)(n - r + 1)) / (ulong)r; ;
        }
        
    }
}
