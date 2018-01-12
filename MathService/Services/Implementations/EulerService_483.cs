using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Numerics;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using MathService.Services.Contracts;
using _calc = MathService.Calculators.Calculator;



namespace MathService.Services.Implementations
{
    public partial class EulerService : IEulerService    
    {
        //Problem 483 --- https://projecteuler.net/problem=483
        //We define a permutation as an operation that rearranges the order of the elements {1, 2, 3, ..., n}.
        //There are n! such permutations, one of which leaves the elements in their initial order.For n = 3 we have 3! = 6 
        //permutations:
        //- P1 = keep the initial order
        //- P2 = exchange the 1st and 2nd elements
        //- P3 = exchange the 1st and 3rd elements
        //- P4 = exchange the 2nd and 3rd elements
        //- P5 = rotate the elements to the right
        //- P6 = rotate the elements to the left

        //If we select one of these permutations, and we re-apply the same permutation repeatedly, we eventually restore the initial order.
        //For a permutation Pi, let f(Pi) be the number of steps required to restore the initial order by applying the permutation Pi repeatedly.
        //For n = 3, we obtain:
        //- f(P1) = 1 : (1,2,3) → (1,2,3)
        //- f(P2) = 2 : (1,2,3) → (2,1,3) → (1,2,3)
        //- f(P3) = 2 : (1,2,3) → (3,2,1) → (1,2,3)
        //- f(P4) = 2 : (1,2,3) → (1,3,2) → (1,2,3)
        //- f(P5) = 3 : (1,2,3) → (3,1,2) → (2,3,1) → (1,2,3)
        //- f(P6) = 3 : (1,2,3) → (2,3,1) → (3,1,2) → (1,2,3)

        //Let g(n) be the average value of f^2(Pi) over all permutations Pi of length n.
        //g(3) = (1^2 + 2^2 + 2^2 + 2^2 + 3^2 + 3^2)/3! = 31/6 ≈ 5.166666667e0
        //g(5) = 2081/120 ≈ 1.734166667e1
        //g(20) = 12422728886023769167301/2432902008176640000 ≈ 5.106136147e3

        //Find g(350) and write the answer in scientific notation rounded to 10 significant digits, using a lowercase e to separate mantissa and exponent, as in the examples above.


        // solution so far-------------------------------------------------------
        // A given permutation can be written as the product of cycles.  
        // The order for this permutation is the LCM of the lengths of the cycles.
        // ex:  n = 50, 1x 5-cycle, 2x 3-cycle, 2x 2-cycle so 15 numbers permute and 35 are left so 35x 1-cycle
        // the lcm is still 30 = lcm(5, 3, 2) which is independent of the number of each cycle type
        // the number of possible permutation like this can be calculated via formula
        // number = (50c5 + 45c3 + 42c3 + 39c2 + 37c2 + 36c1 + 35c1 + ... + 1c1)/41!  where 41 = total number of cycles
        //
        // so g(n) = sum(LCM^2 * number)/n! over each combination of sums
        //
        // Steps:
        // 1.  Find each possible sum of integers (i > 1) where sum <= n ---- in ex above 5 + 3 + 2 <= 50
        // 2.  For each of these, find each linear combination of the integers where total <= n ---- in ex above 5*1 + 3*2 + 2*2 <= 50
        // 3.  How many of these combinations are there?  Total combx * LCM^2
        // 4.  Sum it up and divide by n!
               
        
        public object RunProblem483(int max)
        {
            var result = FindSumCombo(max);
            return result;
            
        }

        private int FindSumCombo(int max)
        {
            var startLevel = 10;
            var combs = _calc.GenAllCombinations(startLevel);
            combs.RemoveAt(0);

            var combBag = new ConcurrentBag<SumCombination>();
            
            //Parallel.ForEach(combs, c =>
            combs.ForEach(c => 
            {
                var sum = 0;
                for (var i = 0; sum <= startLevel && i < c.Count; i++)
                {
                    c[i] += 2;
                    sum += c[i];                    
                }

                if (sum <= startLevel)
                    combBag.Add(new SumCombination(c, startLevel));
            });

            var combLevel = new CombinationLevel()
            {
                Combinations = combBag.ToList(),
                Level = startLevel
            };



            for (var i = startLevel + 1; i <= max; i++)
            {
                combLevel = combLevel.GetNextLevel();
                //combLevel.Print();
            }

            //combLevel.Print();

            return combLevel.Combinations.Count;
        }

        private int GetNextNumber(int total, int testNum)
        {
            return 0;
        }


        private class CombinationLevel
        {
            public int Level { get; set; }
            public int MaxLength { get; set; }
            public List<SumCombination> Combinations { get; set; }
            
            public CombinationLevel GetNextLevel()
            {
                this.Level++;

                var additions = new ConcurrentBag<SumCombination>();

                Parallel.ForEach(this.Combinations, c =>
                //this.Combinations.ForEach(c =>
                {
                    //c.Print();

                    switch(this.Level - c.Sum)
                    {
                        case 1:// room to iterate last number
                            if (c.Numbers.Count > 1 && c.Numbers[1] != c.Numbers[0] + 1)
                            {
                                var newNumbs = new List<int>(c.Numbers);
                                newNumbs[0]++;
                                additions.Add(new SumCombination(newNumbs, this.Level));
                            }
                            break;
                        case 2:// room to add a new 2
                            if (c.Numbers[0] != 2)
                            {
                                var newNumbs = new List<int>(c.Numbers).Prepend(2).ToList();
                                additions.Add(new SumCombination(newNumbs, this.Level));                                
                            }                            
                            break;
                    }
                    
                });

                // adds additions
                this.Combinations.AddRange(additions.ToList());

                // adds new largest singleton
                this.Combinations.Add(new SumCombination(new List<int>() { this.Level }, this.Level));
                
                return this;
            }

            public void Print()
            {
                Debug.WriteLine($"Level:  {this.Level} ------------------");
                this.Combinations.ForEach(c => c.Print());
            }
        }

        private class SumCombination
        {
            //public int Max { get; }
            public int Sum { get; set; }

            public List<int> Numbers { get; set; }

            public List<int> Multiples { get; set; }

            public SumCombination(int max)
            {
                //Max = max;
                Sum = 0;

                var digits = 0;

                while (digits * (digits + 1) / 2 < max)
                    digits++;

                Numbers = new List<int>(digits);
                Multiples = new List<int>(digits);                
            }

            public SumCombination(List<int> comb, int max) : this(max)
            {
                for (var i = 0; i < comb.Count; i++)
                {
                    Numbers.Add(comb[i]);
                    Sum += comb[i];
                }                
            }

            
            public void Print()
            {
                var str = "--- ";

                for(var i = 0; i < Numbers.Count; i++)
                {
                    str += $" {Numbers[i]} ";
                }

                str += $" ---  Sum: {this.Sum}";

                Debug.WriteLine(str);
            }           
        }

    }
}
