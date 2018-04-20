using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Linq;
using MathService.Services.Contracts;
using MathService.Models.EulerModels;
using System.Collections;
using System.Collections.Concurrent;

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
        
        public object RunProblem467(int num)
        {
            
            var mod = 1000000007;
            var pN = new List<int>();
            var cN = new List<int>();
            BuildDigitSequences(pN, cN, num);

            //Where(i => i != 3 && i != 6 && i != 9).ToList()
            var common = FindLongestCommonSubInteger(pN, cN);
            //return new { Count = common.Count(), Common = PrintableList(common) };


            var resultList = BuildSuperInt(pN, cN);
            //var result = BuildResultNum(resultList);

            var p = IsSuperInt(pN, resultList);
            var c = IsSuperInt(cN, resultList);

            var bigResult = BuildResultBigInt(resultList, mod);

            return new { Result = PrintableList(resultList), pN = PrintableList(pN), p, cN = PrintableList(cN), c, answer = bigResult };
        }

        private List<int> BuildSuperInt(List<int> seq1, List<int> seq2)
        {
            var length = Math.Max(seq1.Count(), seq2.Count());
            var super = new List<int>(seq1.Count() + seq2.Count());

            int i1 = 0, i2 = 0, n1 = 0, n2 = 0;

            
            while(i1 < seq1.Count() || i2 < seq2.Count())
            {
                var skip = 0;
                
                if (i2 < seq2.Count())                
                    if (seq2[i2] == 3 || seq2[i2] == 6 || seq2[i2] == 9)
                        skip++;

                if(i2 + skip < seq2.Count())
                    n1 = seq1.IndexOf(seq2[i2 + skip], i1) - i1;           
                else
                    n1 = -1;
                
                if (i1 < seq1.Count())
                    n2 = seq2.IndexOf(seq1[i1], i2) - i2;
                else
                    n2 = -1;
                
                if (n1 < 0 && n2 >= 0)
                {// next num in seq1 matches somewhere in seq2 but next num in seq2 is not in seq1
                    //--- compensate for skip ---
                    //--problem...

                    // take front of seq2 up to match
                    CopySeguenceSegment(seq2, super, i2, i2 + n2);

                    // move forward
                    i2 += n2 + 1;                    
                    i1++;
                    // continue
                    
                }
                else if(n2 < 0 && n1 >= 0)
                {// next num in seq2 matches somewhere in seq1 but next num in seq1 is not in seq2
                    //--- compensate for skip ---
                    if (skip > 0)
                    {
                        var skipVal = seq2[i2];
                        var skipAdded = false;

                        for (var i = 0; i < n1; i++)
                        {
                            if (skipVal < seq1[i1 + i] && !skipAdded)
                            {
                                super.Add(skipVal);
                                skipAdded = true;
                            }
                            else
                                super.Add(seq1[i1 + i]);
                        }
                        if (!skipAdded)
                            super.Add(skipVal);
                        super.Add(seq1[i1 + n1]);
                    }
                    else // take front of seq1 up to match 
                        CopySeguenceSegment(seq1, super, i1, i1 + n1);

                    // move forward
                    i1 += n1 + 1;
                    i2 += 1 + skip;
                    // continue

                }
                else if(n1 < 0 && n2 < 0)
                {// both seq and mutually exclusive
                    // iterate through rest and always take smaller number until both are done                    
                    var merged = SmallestMergeTwoLists(new List<int>(seq1.Skip(i1)), new List<int>(seq2.Skip(i2)));
                    super.AddRange(merged);
                    break;
                }
                else if(n1 < n2)
                {// next num in seq2 matching in seq1 is closer to front than next num in seq1 is in seq2                    
                    //--- compensate for skip ---
                    if(skip > 0)
                    {
                        var skipVal = seq2[i2];
                        var skipAdded = false;

                        for (var i = 0; i < n1; i++)
                        {
                            if (skipVal < seq1[i1 + i] && !skipAdded)
                            {
                                super.Add(skipVal);
                                skipAdded = true;
                            }
                            else
                                super.Add(seq1[i1 + i]);
                        }
                        if(!skipAdded)
                            super.Add(skipVal);
                        super.Add(seq1[i1 + n1]);
                    }
                    else // take front of seq1 up to match                     
                        CopySeguenceSegment(seq1, super, i1, i1 + n1);

                    // move forward
                    i1 += n1 + 1;                    
                    i2+= 1 + skip;
                    // continue
                }
                else if(n2 < n1)
                {// next num in seq1 matching in seq2 is closer to front than next num in seq2 is in seq1
                 // take front of seq2 up to match
                    CopySeguenceSegment(seq2, super, i2, i2 + n2);

                    // move forward
                    i2 += n2 + 1;                    
                    i1++;
                    // continue
                }
                else // n1 == n2
                {// both have a match at same spot 
                    //--- compensate for skip ---
                    if (skip > 0)
                    {
                        var skipVal = seq2[i2];
                        if (skipVal < seq1[n1])
                        {
                            super.Add(skipVal);
                            if (seq1[i1] < seq2[i2])
                                super.Add(seq1[i1++]);
                            else
                                super.Add(seq2[i2 + skip]);
                            i2 += 1 + skip;
                        }
                        else
                        {
                            super.Add(seq1[i1++]);
                            super.Add(skipVal);
                            super.Add(seq2[i2 + skip]);
                            i2 += 1 + skip;
                        }
                    }
                    else
                        // add next
                        if (seq1[i1] < seq2[i2])
                            super.Add(seq1[i1++]);
                        else
                            super.Add(seq2[i2++]);


                }              
            }


            return super;
        }

        private bool IsSuperInt(List<int> seq, List<int> super)
        {
            int next = 0;
            for(var i = 0; i < seq.Count(); i++)
            {
                next = super.IndexOf(seq[i], next);
                if (next == -1)
                    return false;
            }
            return true;
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
            return -1;
        }

        private void CopySeguenceSegment(List<int> fromSeq, List<int> toSeq, int start, int end = int.MaxValue)
        {
            end = Math.Min(fromSeq.Count() - 1, end);
            for (var i = start; i <= end; i++)
                toSeq.Add(fromSeq[i]);
        }

        private long BuildResultNum(List<int> seq)
        {
            long result = 0;
            for (var i = 0; i < seq.Count(); i++)
                result += seq[seq.Count() - i - 1] * (long)Math.Pow(10, i);
            return result;
        }

        private BigInteger BuildResultBigInt(List<int> seq, int mod)
        {
            var result = BigInteger.Zero;
            for (var i = 0; i < seq.Count(); i++)
                result += seq[seq.Count() - i - 1] * BigInteger.ModPow(10, i, mod);
            return BigInteger.Remainder(result, mod);
        }

        private string PrintableList(List<int> seq)
        {
            var str = $"{seq[0]}";
            for (var i = 1; i < seq.Count(); i++)
                str += $", {seq[i]}";
            return str;
        }

        private List<int> FindLongestCommonSubInteger(List<int> seq1, List<int> seq2)
        {
            if (IsSuperInt(seq1, seq2))
                return seq1;

            var length = seq1.Count() - 1;
            var matches = new ConcurrentBag<List<int>>();

            var common = new List<int>(seq1.Count());
            var i2 = 0;
            for(var i = 0; i < seq1.Count(); i++)
            {
                var n = seq2.IndexOf(seq1[i], i2);
                if (n >= 0)
                {
                    common.Add(seq1[i]);
                    i2 = n;
                }
                
                
            }


            return common;
        }

        private List<int> SmallestMergeTwoLists(List<int> seq1, List<int> seq2)
        {
            var merge = new List<int>(seq1.Count() + seq2.Count());

            int i = 0, j = 0;
            while(i < seq1.Count() || j < seq2.Count())
            {
                if (i >= seq1.Count())
                {
                    CopySeguenceSegment(seq2, merge, j);
                    return merge;
                }

                if(j >= seq2.Count())
                {
                    CopySeguenceSegment(seq1, merge, i);
                    return merge;
                }

                if (seq1[i] < seq2[j])
                    merge.Add(seq1[i++]);
                else if (seq2[j] < seq1[i])
                    merge.Add(seq1[j++]);
                else
                {
                    merge.Add(seq1[i++]);
                    j++;
                }                
            }

            return merge;
        }
    }
}
