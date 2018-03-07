using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MathService.Calculators
{
    public static partial class Calculator
    {
        public static BigInteger lcm(List<long> nums)
        {
            BigInteger l = 1;
            for (int i = nums.Count - 1; i >= 0; i--)
                if (l % nums[i] != 0)
                    l = lcm(l, nums[i]);
            return l;
        }

        public static BigInteger lcm(List<BigInteger> nums)
        {
            BigInteger l = 1;
            for (int i = nums.Count - 1; i >= 0; i--)
                if (l % nums[i] != 0)
                    l = lcm(l, nums[i]);
            return l;
        }

        public static BigInteger lcm(BigInteger x, BigInteger y)
        {
            return x * y / BigInteger.GreatestCommonDivisor(x, y);
        }

        public static BigInteger gcf(BigInteger x, BigInteger y)
        {

            BigInteger min = new BigInteger(1);
            BigInteger max = new BigInteger(1);
            if (x < y)
            {
                min = x;
                max = y;
            }
            else
            {
                min = y;
                max = x;
            }
            for (var i = min; i > 1; i--)
                if (x % i == 0 && y % i == 0)
                    return i;
            return 1;
        }

        public static int GetMaxPowerDivisor(BigInteger num, BigInteger factor)
        {
            if (factor < 2)
                return -1;
            int pow = 1;
            while (num % BigInteger.Pow(factor, pow) == 0)
                pow++;
            return pow - 1;
        }
    }
}
