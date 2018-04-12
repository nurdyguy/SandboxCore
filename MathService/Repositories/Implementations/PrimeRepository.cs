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
        //private readonly string _primesFile_1 = "../MathService/Repositories/Constants/_primes_1.txt";
        //private readonly int[] _primes;
        
        private readonly string[] _primes_files = new string[10]
        {
            "../MathService/Repositories/Constants/_prime_bits_0.txt",
            "../MathService/Repositories/Constants/_prime_bits_1.txt",
            "../MathService/Repositories/Constants/_prime_bits_2.txt",
            "../MathService/Repositories/Constants/_prime_bits_3.txt",
            "../MathService/Repositories/Constants/_prime_bits_4.txt",
            "../MathService/Repositories/Constants/_prime_bits_5.txt",
            "../MathService/Repositories/Constants/_prime_bits_6.txt",
            "../MathService/Repositories/Constants/_prime_bits_7.txt",
            "../MathService/Repositories/Constants/_prime_bits_8.txt",
            "../MathService/Repositories/Constants/_prime_bits_9.txt",
        };

        private readonly List<BitArray> _primes = new List<BitArray>(10);
        private readonly List<ulong> _primes_count = new List<ulong>(10);
        private readonly List<ulong> _primes_max = new List<ulong>(10);

        public PrimeRepository()
        {
            //HashSet<int> primeHashSet = new HashSet<int>(
            //File.ReadLines(_primesFile_1)
            //    .SelectMany(line => Regex.Matches(line, @"\d+").Cast<Match>())
            //    .Select(m => m.Value)
            //    .Select(int.Parse));

            //Predicate<int> isPrime = primeHashSet.Contains;

            //_primes = primeHashSet.ToArray();
            //_maxPrime = _primes[_primes.Count() - 1];       

            for (var i = 0; i < 2; i++)
            {
                _primes.Add(ReadBitArrayFile(_primes_files[i]));

            }

        }
        
        public bool IsPrime(ulong num)
        {
            if (num % 2 == 0)
                return false;

            if (num < int.MaxValue)
                return _primes[0][(int)num];

            return false;
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
            // will need to loop through...
            return -1;
           
        }

        public List<int> GetAllPrimes(int max)
        {
            var primes = new List<int>(max) { 2 };
            for (var i = 0; i < max; i++)
                if (_primes[0][i])
                    primes.Add(2*i + 1);
            return primes;
        }

        public List<int> GetAllPrimes(long max)
        {
            if (max < int.MaxValue)
                return GetAllPrimes((int)max);
            return new List<int>();
        }

        public List<int> GetAllPrimes(ulong max)
        {
            if(max < int.MaxValue)
                return GetAllPrimes((int)max);
            return new List<int>();
        }

        public List<int> GetFirstNPrimes(int n)
        {
            var primes = new List<int>(n) { 2 };
            var count = 0;
            var max = n - 1;
            for(var i = 0; count < max && i < _primes[0].Length; i++)
                if(_primes[0][i])
                {
                    primes.Add(2*i + 1);
                    count++;
                }
            return primes;
        }

        public BitArray GetPrimeBitArray(int length)
        {
            var result = new BitArray(_primes[0]);
            result.Length = length;
            return result;

        }

        public ulong GetPrimeCount(int max)
        {
            return GetPrimeCount((ulong)max);
        }

        public ulong GetPrimeCount(long max)
        {
            return GetPrimeCount((ulong)max);
        }

        // number of primes up to max
        public ulong GetPrimeCount(ulong max)
        {
            ulong count = 0;
            for (var i = 0; i < _primes.Count; i++)
                if (max == _primes_max[i])
                    return count += _primes_count[i];
                else if (max > _primes_max[i])
                    count += _primes_count[i];
                else
                    for (var j = 0; j < _primes[i].Length; j++)
                        if (_primes[i][j])
                            count++;
            if (max > _primes_max.Last())
                // manual loop...
                count++;
            return count;
        }

        public BitArray SieveOfErat(long max)
        {
            //max = 5000000000;
            //var fileMax = 500000000;
            //var numFiles = (int)(max / fileMax);
            
            //var nums = new List<BitArray>(numFiles);
            //for(var i = 0; i < numFiles; i++)
            //{
            //    nums.Add(new BitArray(fileMax));
            //    nums[i].SetAll(true);
            //}
            
            //nums[0][0] = false;

            //var maxCheck = (int)Math.Sqrt(max);
            //for (var i = 1; i < maxCheck; i++)
            //{
            //    var fileNum = i / fileMax;
            //    var fileIndex = i % fileMax;
            //    if (nums[fileNum][fileIndex])
            //    {
            //        var adjust = 2 * i + 1;
            //        long j = i + adjust;
            //        while(j < max)
            //        {
            //            var fNum = (int)(j / fileMax);
            //            var fIndex = (int)(j % fileMax);
            //            nums[fNum][fIndex] = false;
            //            j += adjust;
            //        }                        
            //    }
            //}
            
            //max = 2000000000;
            //max /= 2;
            //var nums = new BitArray((int)max);
            //nums.SetAll(true);
            //nums[0] = false;
            //{
            //    int i = 1;
            //    var maxCheck = (int)Math.Sqrt(nums.Length);
            //    while (i < maxCheck)
            //    {
            //        if (nums[i])
            //        {
            //            var add = 2 * i + 1;
            //            for (var j = i + add; j < nums.Length; j += add)
            //                nums[j] = false;
            //        }
            //        i++;
            //    }
            //}
            //var bits = new List<bool>(nums.Length);
            //for (var i = 0; i < nums.Length; i++)
            //    bits.Add(nums[i]);
            
            
            
            //var bools = new List<bool>();
            

            var bits = ReadBitArrayFile(_primes_files[(int)max]);
            return bits;
        }

        public List<bool> SieveOfErat(int max)
        {
            return null;
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
                for (var j = 7; j >= 0; j--)
                {
                    val <<= 1;
                    if (bits[8 * i + j]) val |= 1;
                }
                bytes[i] = val;
            }
            return bytes;
        }

        private BitArray ToBitArray(byte[] bytes)
        {
            var bits = new BitArray(bytes.Length*8);
            for(var i = 0; i < bytes.Length; i++)
            {
                //bits.
            }
            return bits;
        }

        private void WriteFile(byte[] bytes)
        {
            File.WriteAllBytes("../MathService/Repositories/Constants/prime_bools.txt", bytes);
        }

        private void WriteFile(BitArray bits, string file)
        {
            var bytes = ToByteArray(bits);
            File.WriteAllBytes(file, bytes);
        }

        private BitArray ReadBitArrayFile(string file)
        {
            var bytes = File.ReadAllBytes(file);
            return new BitArray(bytes);
        }
        

    }
}
