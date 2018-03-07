using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MathService.Calculators
{
    public static partial class Calculator
    {
        // nPr is really just a PartialFactorial so no need to save them
        //private static readonly string[][] _nPr_BigInt = {        };

        // nPr = n!/(n-r)!
        public static BigInteger nPr(BigInteger n, BigInteger r)
        {
            if (r > n || n < 0)
                return 0;
            if (n < 2)
                return 1;
            
            return PartialFactorial(n, n-r);
        }

        public static BigInteger nPr(ulong n, ulong r)
        {
            return nPr(new BigInteger(n), new BigInteger(r));
        }

        public static BigInteger nPr(long n, long r)
        {
            return nPr(new BigInteger(n), new BigInteger(r));
        }

        public static BigInteger nPr(int n, int r)
        {
            return nPr(new BigInteger(n), new BigInteger(r));
        }
    }
}
