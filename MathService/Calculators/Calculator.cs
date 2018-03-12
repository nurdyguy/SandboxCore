using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Diagnostics;
using System.Linq;
using MathService.Models;
using MathService.Repositories.Contracts;

namespace MathService.Calculators
{
    public partial class Calculator : ICalculator
    {
        private readonly IPrimeRepository _primeRepo;

        public Calculator(IPrimeRepository primeRepo)
        {
            _primeRepo = primeRepo;
            InitCalculator();
        }

        public void InitCalculator()
        {
            //_primeRepo.InitPrimes();
            Debug.WriteLine("-------InitCalculator-------");
        }

        public double Distance(Point<double> p1, Point<double> p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2) + Math.Pow(p1.Z - p2.Z, 2));
        }

        public long Distance(Point<long> p1, Point<long> p2)
        {
            var result = Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2) + Math.Pow(p1.Z - p2.Z, 2));
            if (Math.Ceiling(result) == Math.Floor(result))
                return Convert.ToInt64(result);
            return -1;
        }

        public int Distance(Point<int> p1, Point<int> p2)
        {
            var result = Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2) + Math.Pow(p1.Z - p2.Z, 2));
            if (Math.Ceiling(result) == Math.Floor(result))
                return Convert.ToInt32(result);
            return -1;
        }

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

        private void PrintArray(int[] arr)
        {
            var str = "( ";
            for (int i = 0; i < arr.Count() - 1; i++)
                str += arr[i] + ", ";
            str += arr[arr.Count() - 1] + " )";
            Debug.WriteLine(str);
        }
    }
}
