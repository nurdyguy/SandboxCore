using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Linq;
using MathService.Services.Contracts;
using MathService.Models.EulerModels;
using MathService.Models;
using System.Diagnostics;

namespace MathService.Services.Implementations
{
    public partial class EulerService : IEulerService    
    {
        //https://projecteuler.net/problem=504
        //
        //Problem 504 Published on Saturday, 21st February 2015, 10:00 pm; Solved by 2023;
        //Difficulty rating: 15%
        //Let ABCD be a quadrilateral whose vertices are lattice points 
        //lying on the coordinate axes as follows:
        //
        //A(a, 0), B(0, b), C(−c, 0), D(0, −d), 
        //    where 1 ≤ a, b, c, d ≤ m and a, b, c, d, m are integers.
        //
        //It can be shown that for m = 4 there are exactly 256 valid ways to construct ABCD.
        //Of these 256 quadrilaterals, 42 of them strictly contain a square number of lattice points.
        //
        //How many quadrilaterals ABCD strictly contain a square number of lattice points for m = 100 ?
        //
        //-----Notes-----
        // 1. Specific quadrant is irrelevant, use Q1 wolog
        // 2. x vs y coordinate is irrelevant, 
        // 3. If you chop the rectangle [0,x]x[0,y] along the diagonal (in half) that is the 
        //    quadrant triangle so basically, (x-1)(y-1)/2 then exclude points ON diagonal
        // 4. Count up points in quadrants + points on axis
        public object RunProblem504(int max)
        {
            long count = 0;
            var pts = new List<int>(4);
            for (var a = 1; a <= max; a++)
                for (var b = 1; b <= max; b++)
                    for (var c = 1; c <= max; c++)
                        for (var d = 1; d <= max; d++)
                        {
                            var lp = GetLatticePoints(a, b, c, d);
                            var sqrt = Math.Sqrt(lp);
                            if (Math.Floor(sqrt) == Math.Ceiling(sqrt))
                                count++;
                                //Debug.WriteLine($"----{a}, {b}, {c}, {d}: {lp}----");
                        }
           // var latticePoints = GetLatticePoints(pts);

            return count;
        }

        private long GetLatticePoints(List<int> pts)
        {
            return PointsInTriangle(pts[0], pts[1])
                    + PointsInTriangle(pts[1], pts[2])
                    + PointsInTriangle(pts[2], pts[3])
                    + PointsInTriangle(pts[0], pts[3])
                    + PointsOnAxis(pts[0], pts[1], pts[2], pts[3]);
        }
        private long GetLatticePoints(int a, int b, int c, int d)
        {
            return PointsInTriangle(a, b)
                    + PointsInTriangle(b, c)
                    + PointsInTriangle(c, d)
                    + PointsInTriangle(a, d)
                    + PointsOnAxis(a, b, c, d);
        }
        private long PointsInTriangle(int x, int y)
        {
            long points = ((x - 1)*(y - 1) - gcf(x, y) + 1)/2;
            
            return points;
        }
        private long PointsOnAxis(int a, int b, int c, int d)
        {
            return a + b + c + d - 3;
        }

        private long gcf(int x, int y)
        {
            while (x != 0 && y != 0)
            {
                if (x > y)
                    x %= y;
                else
                    y %= x;
            }

            return x == 0 ? y : x;
        }
        
    }
}
