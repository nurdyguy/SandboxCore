using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Numerics;
using System.Diagnostics;
using System.Collections.Concurrent;


using MathService.Services.Contracts;
using MathService.Models.EulerModels;

using _calc = MathService.Services.Implementations.Calculator;


namespace MathService.Services.Implementations
{
    public class EulerService : IEulerService
    {
        public double RunProblem566(int x, int y, int z)
        {
            var sim = new Problem566(x, y, z);
            do
            {
                sim.MakeNextCut();

            } while (sim.Slices.Any(s => !s.IsFrostingUp));


            return sim.LastEdge.Angle;
        }

        public BigInteger RunProblem590(int num)
        {
            var lcm50000 = BigInteger.Parse(_calc._e590_lcm50000);
            var lcm50000_primeFactorization = _calc._e590_lcm50000_primeFactorization;


            //var powers = GetPrimeFactorization(lcm50000);
            // PrintArray(powers.ToList());


            //return NumSubsetsWithLCMnum_IncludesNum(num);
            //var h = H_brute(num);
            //PrintArray(divs);
            //return divs.Count;
            H_hasMax_brute(num);
            return H_noMax(num);
        }

        // number of sets who have lcm(set) = num ---- cardnality of subset of {1, 2, ..., n}
        // subset of set of divisors of n------
        //private BigInteger 
        private BigInteger H_brute(int num)
        {
            BigInteger counter = 0;
            var divs = FindAllDivisors(num).OrderBy(n => n).ToList();
            for(int size = 1; size <= divs.Count; size++)
            {
                BigInteger maxId = _calc.nCr(divs.Count, size);
                for (int id = 1; id <= maxId; id++)
                {
                    var comb = GetCombFromID(id, size, divs.Count);
                    var set = new List<BigInteger>();
                    for (int i = 0; i < size; i++)
                        set.Add(divs[(int)comb[i]-1]);

                    var l = _calc.lcm(set);
                    if (l == num)
                    {
                        counter++;
                        PrintArray(set);
                    }
                }
            }

            return counter;
        }

        private BigInteger H_noMax(int num)
        {
            BigInteger counter = 0;
            var divs = FindAllDivisors(num).Where(d => d != 1 && d != num).OrderBy(d => d).ToList();
            for (int size = 1; size <= divs.Count; size++)
            {
                BigInteger maxId = _calc.nCr(divs.Count, size);
                for (int id = 1; id <= maxId; id++)
                {
                    var comb = GetCombFromID(id, size, divs.Count);
                    var set = new List<BigInteger>();
                    for (int i = 0; i < size; i++)
                        set.Add(divs[(int)comb[i] - 1]);

                    var l = _calc.lcm(set);
                    if (l == num)
                    {
                        counter++;
                        PrintArray(set);
                    }
                }
            }

            return counter;
        }

        private BigInteger H_hasMax_brute(int num)
        {
            BigInteger counter = 0;
            var divs = FindAllDivisors(num).Where(d => d != 1).OrderBy(d => d).ToList();
            for (int size = 1; size <= divs.Count; size++)
            {
                BigInteger maxId = _calc.nCr(divs.Count, size);
                for (int id = 1; id <= maxId; id++)
                {
                    var comb = GetCombFromID(id, size, divs.Count);
                    var set = new List<BigInteger>();
                    for (int i = 0; i < size; i++)
                        set.Add(divs[(int)comb[i] - 1]);

                    var l = _calc.lcm(set);
                    if (l == num && set.Contains(num))
                    {
                        counter++;
                        PrintArray(set);
                    }
                }
            }

            return counter;
        }

        // num sets with max = 2*Sum(nCi) where i = 1 to max and n = num proper divisors of max
        private BigInteger H_hasMax(BigInteger num)
        {
            BigInteger counter = 0;
            var divs = FindAllDivisors(num).Where(d => d != 1 && d != num);
            var numDivs = new BigInteger(divs.Count());
            for(BigInteger i = 0; i <= numDivs; i++)
            {
                counter += _calc.nCr(numDivs, i);
            }

            return 2*counter;
        }

        // returns lcm of {1, 2, 3, ..., max}
        private BigInteger L(int max)
        {
            var list = new List<long>(max);
            for (long i = 1; i <= max; i++)
                list.Add(i);

            return _calc.lcm(list);
        }
        
        private List<BigInteger> FindAllDivisors(BigInteger num)
        {
            BigInteger counter = 0;
            var divisors = new List<BigInteger>() { 1 };            
            var powers = GetPrimeFactorization(num);

            for(int i = 0; i < powers.Length; i++)
            {
                var newDivs = new ConcurrentBag<BigInteger>();
                for (int j = 1; j <= powers[i]; j++)
                {
                    divisors.ForEach(d => 
                    //Parallel.ForEach(divisors, d =>
                    {
                        newDivs.Add(d * BigInteger.Pow(_calc.GetPrime(i), j));
                        Debug.WriteLine(d * BigInteger.Pow(_calc.GetPrime(i), j));
                        counter++;
                    });                    
                }
                divisors.AddRange(newDivs);
            }
            Debug.WriteLine($"----------------{counter}----------------");
            return divisors;
        }

        private BigInteger NumDivisors(BigInteger num)
        {
            return NumDivisors(GetPrimeFactorization(num));
        }

        private BigInteger NumDivisors(int[] powers)
        {
            BigInteger result = 1;
            for (int i = 0; i < powers.Length; i++)
                result *= powers[i] + 1;
            return result;
        }

        private int[] GetPrimeFactorization(BigInteger num)
        {
            var primes = _calc.GetAllPrimes(50000).ToArray();
            var powers = new int[primes.Length];
            for (var i = 0; i < primes.Length; i++)
                powers[i] = _calc.GetMaxPowerDivisor(num, primes[i]);
            return powers;
        }

        private BigInteger NumSubsetsWithLCMnum_IncludesNum(BigInteger num)
        {
            var powers = GetPrimeFactorization(num);
            var divs = NumDivisors(powers);
            // proper divs
            divs -= 2;

            BigInteger sum = 0;
            for (BigInteger i = 0; i <= divs; i++)
                sum += _calc.nCr(divs, i);

            return sum * 2;
        }

        private static List<List<BigInteger>> GetAllSubsets(int n)
        {
            var subsets = new List<List<BigInteger>>();
            for (int r = 1; r <= n; r++)
                for (var id = 1; id <= _calc.nCr(n, r); id++)
                    subsets.Add(GetCombFromID(id, r, n));
            return subsets;
        }

        public static int GetCombID(List<int> comb, int max)
        {
            BigInteger id = _calc.nCr(max + 1, comb.Count);
            for (int i = 0; i < comb.Count; i++)
                id -= _calc.nCr(max - comb[i], comb.Count - i);
            return (int)id;
        }

        public static List<BigInteger> GetCombFromID(int id, int combLength, int max)
        {
            List<BigInteger> comb = new List<BigInteger>(combLength);
            var tId = _calc.nCr(max, combLength) - (UInt64)id;
            BigInteger bMax = max;
            for (BigInteger i = combLength; i > 0; i--)
            {
                BigInteger tVal = 0;
                bool done = false;
                BigInteger pos = 0;
                while (!done)
                {
                    var t = _calc.nCr((int)(bMax - pos), (int)i);
                    if (t <= tId)
                    {
                        tVal = t;
                        done = true;
                    }
                    pos++;
                }
                tId -= tVal;
                comb.Add(pos - 1);
            }

            return comb;
        }

        private static void PrintSomething()
        {
            //for (int i = 0; i < 2000; i++)
            //    if (BigInteger.Compare(_calc.Factorial(i), _calc.ShortFactorial(i, 1)) != 0)
            //        Debug.WriteLine("-------------------------" + i + "-------------------------" + _calc.Factorial(i) + "-------------------------");

            //for (int n = 201; n <= 500; n++)
            //{
            //    string str = "new string[] {";
            //    for (int r = 0; r <= n; r++)
            //    {
            //        str += "\"" + _calc.nCr(n, r) + "\"";
            //        if (r < n)
            //            str += ", ";
            //    }
            //    str += "},";
            //    Debug.WriteLine(str);
            //}

            //int start = 2001;
            //int end = 3000;
            //int perLine = 5;
            //string str = "";
            //for (var i = start; i <= end; i++)
            //{
            //    str += "\"" + _calc.ShortFactorial(i, 1) + "\", ";
            //    if (i % perLine == 0)
            //    {
            //        Debug.WriteLine(str);
            //        str = "";
            //    }
            //}
        }

        private static void PrintArray(List<BigInteger> arr)
        {
            var str = "";
            for(int i = 0; i < arr.Count() - 1; i++)
                str += arr[i] + ", ";
            str += arr[arr.Count() - 1];
            Debug.WriteLine(str);
        }

        private static void PrintArray(List<int> arr)
        {
            var str = "";
            for (int i = 0; i < arr.Count() - 1; i++)
                str += arr[i] + ", ";
            str += arr[arr.Count() - 1];
            Debug.WriteLine(str);
        }
    }
}
