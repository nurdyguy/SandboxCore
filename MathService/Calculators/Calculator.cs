using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Diagnostics;
using System.Linq;


namespace MathService.Calculators
{
    public static partial class Calculator
    {


        //public static void Printer(int max = 1000)
        //{
        //    var line = "{";
        //    for (var i = 2; i <= max; i++)
        //    {
        //        if (IsPrime(i))
        //            line += $" {{{i},{i}}}, ";                                
        //    }

        //    Debug.WriteLine(line);
        //}

        private static void PrintArray(int[] arr)
        {
            var str = "( ";
            for (int i = 0; i < arr.Count() - 1; i++)
                str += arr[i] + ", ";
            str += arr[arr.Count() - 1] + " )";
            Debug.WriteLine(str);
        }
    }
}
