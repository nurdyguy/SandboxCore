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

        private BitArray _primeBitPattern { get; set; }
        public object RunProblem467(int num)
        {
            var pN = new List<int>();
            var cN = new List<int>();
            BuildDigitSequences(pN, cN, num);
            var result = BuildSuperInt(pN, cN);
            return new { result };
        }

        private List<int> BuildSuperInt(List<int> seq1, List<int> seq2)
        {
            var length = Math.Max(seq1.Count(), seq2.Count());
            var super = new List<int>(seq1.Count() + seq2.Count());

            int i1 = 0;
            int i2 = 0;
            var done = false;

            while(!done)
            {
                if(i1 == -1)
                {
                    if(i2 > -1)
                        CopySeguenceSegment(seq2, super, i2);
                    break;
                }
                if(i2 == -1)
                {
                    CopySeguenceSegment(seq1, super, i1);
                    break;
                }
                
                var n1 = FindNext(seq1, i1, seq2[i2]);
                var n2 = FindNext(seq2, i2, seq1[i1]);

                if(n1 < n2)
                {
                    super.Add(seq1[i1++]);
                    //CopySeguenceSegment(seq2, super, i2, n1 - 1);
                    //i2 = n1 - 1;
                }
                else if (n2 < n1)
                {
                    super.Add(seq2[i2++]);
                    //CopySeguenceSegment(seq1, super, i1, n2 - 1);
                    //i1 = n2 - 1;
                }
                else
                {
                    if (n1 == int.MaxValue)
                        break;
                    super.Add(seq1[i1++]);
                    i2++;
                    //CopySeguenceSegment(seq1, super, i1, i1);
                    //i1++;
                    //i2++;
                }

                if (i1 >= seq1.Count())
                    i1 = -1;
                if (i2 >= seq2.Count())
                    i2 = -1;                
            }


            return super;
        }

        private bool IsSuperInt(List<int> seq, List<int> super)
        {
            var passed = true;


            return passed;
        }

        private void BuildDigitSequences(List<int> pDigs, List<int> cDigs, int size)
        {
            var primes = _calc.GetFirstNPrimes(size);
            var comps = new List<int>(size);
            var c = 4;
            while (comps.Count() < size)
            {
                if (!primes.Contains(c++))
                    comps.Add(c - 1);
            }

            for (var i = 0; i < primes.Count(); i++)
                pDigs.Add(SumDigits(primes[i]));
            for (var i = 0; i < comps.Count(); i++)
                cDigs.Add(SumDigits(comps[i]));

        }

        private int SumDigits(int num)
        {
            var result = 0;
            while(num > 0)
            {
                var t = num;
                num /= 10;
                result += t - 10 * num;
            }
            if (result > 9)
                result = SumDigits(result);
            return result;
        }

        private int FindNext(List<int> seq, int start, int value)
        {
            for (var i = start; i < seq.Count(); i++)
                if (seq[i] == value)
                    return i;
            return int.MaxValue;
        }

        private void CopySeguenceSegment(List<int> fromSeq, List<int> toSeq, int start, int end = int.MaxValue)
        {
            end = Math.Min(fromSeq.Count() + 1, end);
            for (var i = start; i <= end; i++)
                toSeq.Add(fromSeq[i]);
        }
    }
}
