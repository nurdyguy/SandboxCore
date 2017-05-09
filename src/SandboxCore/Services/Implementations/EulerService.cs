using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SandboxCore.Services.Contracts;
using SandboxCore.Models.EulerModels;
using System.Numerics;

namespace SandboxCore.Services.Implementations
{
    public class EulerService : IEulerService
    {


        public double RunProblem566(int x, int y, int z)
        {
            var sim = new SandboxCore.Models.EulerModels.Problem566(x, y, z);
            do
            {
                sim.MakeNextCut();

            } while (sim.Slices.Any(s => !s.IsFrostingUp));


            return sim.LastEdge.Angle;
        }

        public BigInteger RunProblem590(int max)
        {


            return L(max);
        }


        private BigInteger L(int max)
        {
            var list = new List<long>(max);
            for (long i = 1; i <= max; i++)
                list.Add(i);

            return lcm(list);
        }
        
        private static BigInteger lcm(List<long> nums)
        {
            BigInteger l = 1;
            for (int i = nums.Count - 1; i >= 0; i--)
                if (l % nums[i] != 0)
                    l = lcm(l, nums[i]);
            return l;
        }

        private static BigInteger lcm(BigInteger x, BigInteger y)
        {
            return x * y / gcf(x, y);
        }

        private static BigInteger gcf(BigInteger x, BigInteger y)
        {

            BigInteger min = new BigInteger(1);
            BigInteger max = new BigInteger(1);
            if (x < y)
            {
                min = x;
                max = y;
            }
            else
            {
                min = y;
                max = x;
            }
            for (var i = min; i > 1; i--)
                if (x % i == 0 && y % i == 0)
                    return i;
            return 1;
        }
    }
}
