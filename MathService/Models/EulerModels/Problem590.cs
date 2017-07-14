using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MathService.Models.EulerModels
{

    public class Problem590
    {

        public long L(int max)
        {
            var list = new List<int>(max);
            for (int i = 1; i <= max; i++)
                list.Add(i);

            return (long)lcm(list);
        }




        private static int lcm(List<int> nums)
        {
            int l = 1;
            for (int i = nums.Count - 1; i >= 0; i--)
                if (l % nums[i] != 0)
                    l = lcm(l, nums[i]);
            return l;
        }

        private static int lcm(int x, int y)
        {
            return x * y / gcf(x, y);
        }

        private static int gcf(int x, int y)
        {
            int min = System.Math.Min(x, y);
            int max = System.Math.Max(x, y);
            for (var i = min; i > 1; i--)
                if (i % x == 0 && i % y == 0)
                    return i;
            return 1;
        }

    }
    
}
