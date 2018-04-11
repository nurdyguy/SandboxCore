using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Linq;
using MathService.Services.Contracts;
using MathService.Models.EulerModels;
using System.Collections;

namespace MathService.Services.Implementations
{
    public partial class EulerService : IEulerService
    {
        //Problem 574 --- https://projecteuler.net/problem=467
        //Published on Sunday, 13th April 2014, 10:00 am; Solved by 323; Difficulty rating: 50%
        //
        //An integer s is called a superinteger of another integer n if the digits 
        //of n form a subsequence of the digits of s.
        //For example, 2718281828 is a superinteger of 18828, 
        //while 314159 is not a superinteger of 151.
        //
        //Let p(n) be the nth prime number, and let c(n) be the nth composite number. 
        //For example, p(1) = 2, p(10) = 29, c(1) = 4 and c(10) = 18.
        //
        //{p(i) : i ≥ 1} = {2, 3, 5, 7, 11, 13, 17, 19, 23, 29, ...}
        //{c(i) : i ≥ 1} = {4, 6, 8, 9, 10, 12, 14, 15, 16, 18, ...}
        //
        //Let PD the sequence of the digital roots of {p(i)} (CD is defined similarly for {c(i)}):
        //PD = {2, 3, 5, 7, 2, 4, 8, 1, 5, 2, ...}
        //CD = {4, 6, 8, 9, 1, 3, 5, 6, 7, 9, ...}
        //
        //Let P_n be the integer formed by concatenating the first n elements of PD 
        //(C_n is defined similarly for CD).
        //
        //P_10 = 2357248152
        //C_10 = 4689135679
        //
        //Let f(n) be the smallest positive integer that is a common superinteger of P_n and C_n. 
        //
        //For example, f(10) = 2357246891352679, and f(100) mod 1,000,000,007 = 771661825.
        //
        //Find f(10,000) mod 1,000,000,007.
        //
        //
        // Notes:
        // 1.  Get P_n and C_n as arrays
        // 2.  Start with the one with more digits and build out the superInt

        private BitArray _primeBitPatter { get; set; }
        public object RunProblem467(int num)
        {
            var p = _calc.GetPrimeBitArray(num);
            var result = BigInteger.Zero;


            return result;
        }

        private int[] BuildSuperInt(int[] seq1, int[] seq2)
        {
            var length = Math.Max(seq1.Length, seq2.Length);
            var super = new int[length*2];



            return super;
        }

        private bool IsSuperInt(int[] seq, int[] super)
        {
            var passed = true;


            return passed;
        }

        private int[] BuildDigitSeq(int numDigits, bool usePrimes)
        {
            var seq = new int[numDigits];

            return seq;
        }
    }
}
