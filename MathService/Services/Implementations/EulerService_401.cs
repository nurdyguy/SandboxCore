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
        //https://projecteuler.net/problem=401 difficulty: 25%
        //
        //The divisors of 6 are 1,2,3 and 6.
        //                            
        //The sum of the squares of these numbers is 1+4+9+36=50.
        //                        
        //Let sigma2(n) represent the sum of the squares of the divisors of n.
        //Thus sigma2(6)=50.
        //
        //Let SIGMA2 represent the summatory function of sigma2, that is SIGMA2(n)=∑sigma2(i) for i=1 to n.
        //                        
        //The first 6 values of SIGMA2 are: 1,6,16,37,63 and 113.
        //                        
        //Find SIGMA2(10^15) modulo 10^9.                                                                                                                                                                                                                            
        
        //-----------------------------------------------------------------------------------
        //Notes:
        // 1.  Loop through and divide each number into max
        // 2.  The quotient is the number of times that number divides each number less than max
        // 3.  Add quotient*num^2
        // 

        public object RunProblem401(int exp)
        {
            var mod = BigInteger.Pow(10, 9);
            var result = LoopForward(exp);

            var tail = CalcTail(exp);

            return BigInteger.Remainder(result + tail, mod);
        }

        private BigInteger LoopForward(int exp)
        {
            var max = (long)Math.Pow(10, exp);
            var result = new BigInteger(max);
            var mod = BigInteger.Pow(10, 9);
            
            // all numbers less than half of max
            var loopEnd = (int)Math.Sqrt(max) - 1;
            for (long i = 2; i <= loopEnd; i++)
            {
                var num = max / i;
                var next = max / (i + 1);
                
                switch(num - next)
                {
                    case 1:
                        result += i * new BigInteger(num) * num;
                        break;
                    case 2:
                        result += i *(num * new BigInteger(num) + (num-1)*new BigInteger(num-1));
                        break;
                    default:
                        result += i * SumOfSquares(next, num);
                        break;
                }

                result += num * new BigInteger(i) * i;
                result = BigInteger.Remainder(result, mod);
            }

            var last = max / (++loopEnd);
            if (last == loopEnd)
            {
                result += BigInteger.Pow(loopEnd + 1, 3);
            }
            else
            {
                result += last * new BigInteger(loopEnd) * loopEnd;
                result += loopEnd * new BigInteger(last) * last;
            }
            //Debug.WriteLine($"--last: {last} - {loopEnd}--");
            //result = BigInteger.Remainder(result, mod);

            return result;
        }
        
        private BigInteger CalcTail(int exp)
        {
            var max = (long)Math.Pow(10, exp);
            // gets all ends
            return SumOfSquares(max / 2, max);
        }

        // from start + 1 to end
        private BigInteger SumOfSquares(long start, long end)
        {
            // from 1 to end
            var result = (2 * end + 1) * new BigInteger(end + 1) * end;
            // subtract from 1 to start
            result -= (2 * start + 1) * new BigInteger(start + 1) * start;
            return result / 6;
        }


    }    
}
