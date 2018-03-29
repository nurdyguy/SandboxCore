using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

using MathService.Repositories.Contracts;
using System.Collections;


//using MathService.Repositories.Constants;

namespace MathService.Repositories.Implementations
{
    public class PrimeRepository : IPrimeRepository
    {
        private readonly string _primesFile_1 = "../MathService/Repositories/Constants/primes_1.txt";
        private readonly int[] _primes;
        private readonly int _maxPrime;

        public PrimeRepository()
        {
            HashSet<int> primeHashSet = new HashSet<int>(
            File.ReadLines(_primesFile_1)
                //.AsParallel() //maybe?
                .SelectMany(line => Regex.Matches(line, @"\d+").Cast<Match>())
                .Select(m => m.Value)
                .Select(int.Parse));

            //Predicate<int> isPrime = primeHashSet.Contains;

            _primes = primeHashSet.ToArray();
            _maxPrime = _primes[_primes.Count() - 1];            
        }
        
        public bool IsPrime(ulong num)
        {
            

            return true;
        }

        public bool IsPrime(long num)
        {
            return IsPrime((ulong)num);
        }

        public bool IsPrime(int num)
        {
            return IsPrime((ulong)num);
        }

        public int GetPrime(int index)
        {
            if (index < _primes.Length)
                return _primes[index];
            else
                return -1;
           
        }

        public List<int> GetAllPrimes(int max)
        {
            return _primes.Where(p => p <= max).ToList();;
        }

        public List<int> GetFirstNPrimes(int n)
        {
            var primes = new List<int>(_primes.Take(n));
            return primes;
        }

        public List<bool> SieveOfErat(int max)
        {
            // runs Sieve of Eratsthenes for odd numbers
            //max = max / 2;
            //var nums = new List<bool>(max) { false };
            //for (var i = 1; i < max; i++)
            //    nums.Add(true);

            //var maxCheck = (int)Math.Sqrt(nums.Count());
            //for (var i = 1; i < maxCheck; i++)
            //    if (nums[i])
            //    {
            //        var add = 2 * i + 1;
            //        for (var j = i + add; j < nums.Count(); j += add)
            //            nums[j] = false;
            //    }

            //max = 2000000000;
            //max /= 2;
            //var nums = new BitArray(max);
            //nums.SetAll(true);
            //nums[0] = false;

            //var maxCheck = (int)Math.Sqrt(nums.Length);
            //for (var i = 1; i < maxCheck; i++)
            //    if (nums[i])
            //    {
            //        var add = 2 * i + 1;
            //        for (var j = i + add; j < nums.Length; j += add)
            //            nums[j] = false;
            //    }

            max = 2000000000;
            max /= 2;
            var nums = new BitArray(max);
            nums.SetAll(true);
            nums[0] = false;
            {
                int i = 1;
                var maxCheck = (int)Math.Sqrt(nums.Length);
                while (i < maxCheck)
                {
                    if (nums[i])
                    {
                        var add = 2 * i + 1;
                        for (var j = i + add; j < nums.Length; j += add)
                            nums[j] = false;
                    }
                    i++;
                }
            }
            var bits = new List<bool>(nums.Length);
            for (var i = 0; i < nums.Length; i++)
                bits.Add(nums[i]);

            //WriteFile(nums);

            //var bits = ReadFile();
            //var bools = new List<bool>(bits.Length);
            //var p = 0;
            //for (var i = 0; i < bits.Length; i++)
            //    if (bits[i]) p++;
            return bits;
        }
        
        private List<byte> BoolsToBytes(List<bool> bools)
        {
            var bytesCount = bools.Count() / 8;
            var bytes = new List<byte>(bytesCount);
            
            for(var i = 0; i < bytesCount; i++)
            {
                byte val = 0;
                for (var j = 0; j < 8; j++)
                {
                    val <<= 1;
                    if (bools[8*i+j]) val |= 1;
                }
                bytes.Add(val);
            }
            return bytes;
        }

        private byte[] ToByteArray(BitArray bits)
        {
            var bytesCount = bits.Length / 8;
            var bytes = new byte[bytesCount];

            for (var i = 0; i < bytesCount; i++)
            {
                byte val = 0;
                for (var j = 0; j < 8; j++)
                {
                    val <<= 1;
                    if (bits[8 * i + j]) val |= 1;
                }
                bytes[i] = val;
            }
            return bytes;
        }

        private List<bool> BytesToBools(byte[] bytes)
        {
            var bools = new List<bool>();
            var bits = new BitArray(bytes);
            
            return bools;
        }

        private void WriteFile(byte[] bytes)
        {
            File.WriteAllBytes("../MathService/Repositories/Constants/prime_bools.txt", bytes);
        }

        private void WriteFile(BitArray bits)
        {
            var bytes = ToByteArray(bits);
            File.WriteAllBytes("../MathService/Repositories/Constants/prime_bools.txt", bytes);
        }

        private BitArray ReadFile()
        {
            var bytes = File.ReadAllBytes("../MathService/Repositories/Constants/prime_bools.txt");
            var intList = new BitArray(bytes);

            return intList;
        }
    }
}
