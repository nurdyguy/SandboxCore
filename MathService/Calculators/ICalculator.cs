using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MathService.Calculators
{
    public interface ICalculator
    {
        void InitCalculator();

        #region PrimeCalculations
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
        BitArray GetPrimeBitArray(int length);
        List<bool> SieveOfErat(int max);
        #endregion

        #region CombinationCalculations
        ulong nCr_ulong(int n, int r);
        BigInteger nCr(BigInteger n, BigInteger r);
        BigInteger nCr(ulong n, ulong r);
        BigInteger nCr(long n, long r);
        BigInteger nCr(int n, int r);
        BigInteger EncodeCombination(List<int> comb, int max);
        
        // gets Combination from integer 
        List<int> DecodeCombination(BigInteger id, int max, int combLength);        
        List<List<int>> GenAllCombinations(int max);
        #endregion


        #region PermutationCalculations
        BigInteger nPr(BigInteger n, BigInteger r);
        BigInteger nPr(ulong n, ulong r);
        BigInteger nPr(long n, long r);
        BigInteger nPr(int n, int r);
        
        BigInteger EncodePermutation(int[] perm);
        
        // gets Permutation from Integer --- https://stackoverflow.com/questions/1506078/fast-permutation-number-permutation-mapping-algorithms
        int[] DecodePermutation(BigInteger code, int size);
        
        // Heaps algorithm non-recursive https://en.wikipedia.org/wiki/Heap%27s_algorithm
        //procedure generate(n : integer, A : array of any):
        //    c : array of int

        //    for i := 0; i<n; i += 1 do
        //        c[i] := 0
        //    end for
        //
        //    output(A)
        //
        //    i := 0;
        //    while i<n do
        //        if  c[i] < i then
        //            if i is even then
        //                swap(A[0], A[i])
        //            else
        //                swap(A[c[i]], A[i])
        //            end if
        //            output(A)
        //            c[i] += 1
        //            i := 0
        //        else
        //            c[i] := 0
        //            i += 1
        //        end if
        //    end while
        void GenAllPermutations(int n);
        #endregion

        #region FactorialCalculations
        BigInteger Factorial(BigInteger n);
        BigInteger Factorial(ulong n);
        BigInteger Factorial(long n);
        BigInteger Factorial(int n);
        BigInteger Factorial(short n);
        BigInteger PartialFactorial(BigInteger n, BigInteger x);
        BigInteger PartialFactorial(ulong n, ulong x);
        
        #endregion

        #region FactorizationCalculations
        BigInteger lcm(List<long> nums);
        BigInteger lcm(List<BigInteger> nums);
        BigInteger lcm(BigInteger x, BigInteger y);
        BigInteger gcf(BigInteger x, BigInteger y);
        int GetMaxPowerDivisor(BigInteger num, BigInteger factor);
        
        #endregion
    }
}
