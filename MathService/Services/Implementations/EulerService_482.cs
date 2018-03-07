using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Linq;
using MathService.Services.Contracts;
using MathService.Models.EulerModels;
using MathService.Models;
using _calc = MathService.Calculators.Calculator;


namespace MathService.Services.Implementations
{
    public partial class EulerService : IEulerService    
    {
        //https://projecteuler.net/problem=482
        //https://en.wikipedia.org/wiki/Incenter

        //ABC is an integer sided triangle with incenter I and perimeter p.
        //The segments IA, IB and IC have integral length as well.

        //Let L = p + | IA | + | IB | + | IC |.

        //Let S(P) = ∑L for all such triangles where p ≤ P.For example, S(103) = 3619.

        //Find S(107).

        //-----------------------------------------------------------------------------------
        //Notes:
        // IA is the angle bisector of A
        //Given the coordinates of the three vertices of a triangle ABC, 
        //the coordinates of the incenter O are
        // Ox = (aAx +	bBx	+ cCx)/p
        // Oy = (aAy + bBy + cCy)/p
        //
        // where
        // Ax and Ay are the x and y coordinates of the point A etc..
        // a, b and c are the side lengths opposite vertex A, B and C
        // p	is perimeter of the triangle (a+b+c)


        //where:
        //Ax and Ay are the x and y coordinates of the point A etc..
        //a, b and c are the side lengths opposite vertex A, B and C
        //p	is perimeter of the triangle (a+b+c)


        public BigInteger RunProblem482(int P)
        {
            
            return BigInteger.One;
        }


        //private bool TestAllInts(Point<int> A, Point<int> B, Point<int> C)
        //{
        //    var AB = _calc.Distance(A, B);
        //    if (AB == -1) return false;

        //    var BC = _calc.Distance(B, C);
        //    if (BC == -1) return false;

        //    var AC = _calc.Distance(A, C);
        //    if (AC == -1) return false;
            


        //}

        //private Point<double> FindIncenter(Point<int> A, Point<int> B, Point<int> C)
        //{

        //}
    }
}
