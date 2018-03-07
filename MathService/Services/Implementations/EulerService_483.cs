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
            var result = FindSumComboRecursive(max);
            return result;
            
        }

        private BigInteger FindSumComboRecursive(int max)
        {
            var startLevel = 4;
            
            Combinations[0] = new CombinationLevel()
            {
                Level = 0,
                Combinations = new List<SumCombination>()
            };

            Combinations[1] = new CombinationLevel()
            {
                Level = 1,
                Combinations = new List<SumCombination>()
            };

            Combinations[2] = new CombinationLevel()
            {
                Level = 2,
                Combinations = new List<SumCombination>()
            };
            Combinations[2].Combinations.Add(new SumCombination(new List<int>() { 2 }, 1));

            Combinations[3] = new CombinationLevel()
            {
                Level = 3,
                Combinations = new List<SumCombination>()
            };
            Combinations[3].Combinations.Add(new SumCombination(new List<int>() { 3 }, 1));

            BigInteger combCounter = 2;

            for (var i = startLevel; i <= max; i++)
            {
                Combinations[i - 3] = null;// new CombinationLevel();
                Combinations[i] = new CombinationLevel();
                Combinations[i] = GetNextLevel(Combinations[i - 1], Combinations[i - 2]);
                //combCounter+= Combinations[i]
                //Combinations[i].Print();
                combCounter += Combinations[i].Combinations.Count;
                //Debug.WriteLine($"//------ Level:  {i} ------- Count:   {Combinations[i].Combinations.Count} -----------");
                Combinations[i].Print();
            }


            //var l0 = new int[] { };
            //var l1 = new int[] { };
            //var l2 = new int[] { 2 };
            //var l3 = new int[] { 3 };
            //var levels = new List<List<int[]>>
            //{
            //    new List<int[]> { },
            //    new List<int[]> { },
            //    new List<int[]> { new int[] { 2 } },
            //    new List<int[]> { new int[] { 3 } }
            //};

            //for (var i = startLevel; i <= max; i++)
            //{
            //    var next = GetNextLevel(levels[i - 1], levels[i - 2], i);
            //    levels.Add(next);
            //    combCounter += next.Count();
            //    Debug.WriteLine($"Level:  {i} ------- Count:   {levels[i].Count()} -----------");
            //    next.ForEach(n =>
            //    {
            //        var str = "";
            //        for (var j = 0; j < n.Count(); j++)
            //        { 
            //            str += n[j] + ", ";
            //            str = str.TrimEnd([',']);
            //            Debug.WriteLine(str);
            //        }
            //    });
            //}


            //combLevel.Print();

            return combCounter;
        }

        private BigInteger FindSumCombo(int max)
        {
            BigInteger counter = 2;

            for(var i = max; i >= 2; i--)
            {

            }



            return counter;
        }

        private int GetNextNumber(int total, int testNum)
        {
            return 0;
        }


        private CombinationLevel[] Combinations = new CombinationLevel[350];

        private CombinationLevel GetNextLevel(CombinationLevel prevLevel, CombinationLevel prevLevel2)
        {
            var additions = new ConcurrentBag<SumCombination>();
            var newLevel = prevLevel.Level + 1;
            // room to iterate last number
            //Parallel.ForEach(prevLevel.Combinations, c =>
            prevLevel.Combinations.ForEach(c =>
            {
                if (c.Numbers.Count == 1 || (c.Numbers.Count > 1 && c.Numbers[c.Numbers.Count - 1] != c.Numbers[c.Numbers.Count - 2] - 1))
                {
                    var newNumbs = new List<int>(c.Numbers);
                    newNumbs[c.Numbers.Count - 1]++;
                    additions.Add(new SumCombination(newNumbs, newLevel));
                }
            });

            // room to add a new 2
            //Parallel.ForEach(prevLevel2.Combinations, c =>
            prevLevel2.Combinations.ForEach(c =>
            {
                if (c.Numbers.Count > 0 && c.Numbers[c.Numbers.Count - 1] != 2)
                {
                    var newNumbs = new List<int>(c.Numbers).Append(2).ToList();
                    additions.Add(new SumCombination(newNumbs, newLevel));
                }
            });

            var newCombLevel = new CombinationLevel();
            newCombLevel.Level = newLevel;
            newCombLevel.Combinations = additions.ToList();
            return newCombLevel;
        }

        private List<int[]> GetNextLevel(List<int[]> prevLevel, List<int[]> prevLevel2, int nextlevel)
        {
            //var additions = new ConcurrentBag<int[]>();

            //// room to iterate last number
            //Parallel.ForEach(prevLevel, c =>
            //{
            //    if (c.Count() == 1 || (c.Count() > 1 && c[c.Count() - 1] != c[c.Count() - 2] - 1))
            //    {
            //        var newNumbs = new int[c.Count()];
            //        c.CopyTo(newNumbs, 0);
            //        newNumbs[c.Count() - 1]++;
            //        additions.Add(newNumbs);
            //    }
            //});

            //// room to add a new 2
            //Parallel.ForEach(prevLevel2, c =>            
            //{
            //    if (c.Count() > 0 && c[c.Count() - 1] != 2)
            //    {
            //        var newNumbs = new int[c.Count() + 1];
            //        c.CopyTo(newNumbs, 0);
            //        newNumbs[c.Count()] = 2;
            //        additions.Add(newNumbs);
            //    }
            //});
//
            var additions = new List<int[]>((int)(prevLevel.Count*1.1));

            // room to iterate last number
            for(var i = 0; i < prevLevel.Count(); i++)
            {
                if (prevLevel[i].Count() == 1 || (prevLevel[i].Count() > 1 && prevLevel[i][prevLevel[i].Count() - 1] != prevLevel[i][prevLevel[i].Count() - 2] - 1))
                {
                    var newNumbs = new int[prevLevel[i].Count()];
                    prevLevel[i].CopyTo(newNumbs, 0);
                    newNumbs[prevLevel[i].Count() - 1]++;
                    additions.Add(newNumbs);
                }
            }

            // room to add a new 2
            for (var i = 0; i < prevLevel2.Count(); i++)
            {
                if (prevLevel2[i].Count() > 0 && prevLevel2[i][prevLevel2[i].Count() - 1] != 2)
                {
                    var newNumbs = new int[prevLevel2[i].Count() + 1];
                    prevLevel2[i].CopyTo(newNumbs, 0);
                    newNumbs[prevLevel2[i].Count()] = 2;
                    additions.Add(newNumbs);
                }
            }

            return additions;
        }

        private class CombinationLevel
        {
            public int Level { get; set; }
            public int MaxLength { get; set; }
            public List<SumCombination> Combinations { get; set; }

            //public CombinationLevel[] Combinations = new CombinationLevel[350];
            
            public void Print()
            {
                Debug.WriteLine($"//----- Level:  {this.Level} -------- Count: {this.Combinations.Count()}----------");
                Debug.Write("{ ");
                var str = "";
                for(var i = 0; i < this.Combinations.Count(); i++)
                {
                    if(i == this.Combinations.Count() - 1)
                        str += $"{this.Combinations[i].ToString()}";
                    else
                        str += $"{this.Combinations[i].ToString()}, ";
                    if(str.Length > 200 && i < this.Combinations.Count() - 1)
                    {
                        //str += '\n';
                        Debug.WriteLine(str);
                        str = "";
                    }
                };
                Debug.WriteLine(str + " }, ");
                //str = "";
                //Debug.Write();
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
                Debug.WriteLine(this.ToString());
            }        
            
            public override string ToString()
            {
                var str = $"{{{Numbers[0]}";

                for (var i = 1; i < Numbers.Count; i++)
                {
                    str += $", {Numbers[i]}";
                }

                str += $"}}";
                return str;
            }
        }

    }
}
