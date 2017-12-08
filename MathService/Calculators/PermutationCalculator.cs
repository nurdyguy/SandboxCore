using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Diagnostics;


namespace MathService.Calculators
{
    public static partial class Calculator
    {


        // converts Permutation into integer
        public static BigInteger EncodePermutation(int[] perm)
        {



            return 0;
        }

        // gets Permutation from Integer --- https://stackoverflow.com/questions/1506078/fast-permutation-number-permutation-mapping-algorithms
        public static int[] DecodePermutation(BigInteger code, int size)
        {
            int index = 0;            
            BigInteger m = code;
            int[] permuted = new int[size];
            int[] elems = new int[size];

            for (int i = 0; i < size; i++)
                elems[i] = i;

            for (int i = 0; i < size; i++)
            {
                index = (int)(m % (size - i));
                m = m / (size - i);
                permuted[i] = elems[index];
                elems[index] = elems[size - i - 1];
            }
            //PrintArray(permuted);
            return permuted;

        }

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
        public static void GenAllPermutations(int n)
        {
            var curr = new int[n];
            for (var a = 0; a < n; a++)
                curr[a] = a + 1;
            
            //PrintArray(curr);
            var temp = new int[n];
            BigInteger counter = 1;
            int i = 0;
            while (i < n)
            {
                if (temp[i] < i)
                {
                    if (i % 2 == 0)
                    {
                        int t = curr[i];
                        curr[i] = curr[0];
                        curr[0] = t;
                    }
                    else
                    {
                        int t = curr[i];
                        curr[i] = curr[temp[i]];
                        curr[temp[i]] = t;
                    }
                    //PrintArray(curr);
                    counter++;
                    temp[i]++;
                    i = 0;
                }
                else
                {
                    temp[i] = 0;
                    i++;
                }
            }
        }
    }

    public class Permutation
    {
        public List<int> Perm { get; set; }
        public int order { get; set; }
    }
}
