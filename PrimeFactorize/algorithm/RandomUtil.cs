using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prime_factorize
{
    internal static class RandomUtil
    {
        private static Random rnd = new Random();

        internal static long LongRandom(long min, long max)
        {
            long a = rnd.Next(int.MinValue, int.MaxValue);
            long b = rnd.Next(0, int.MaxValue) >> 32;
            long result = a | b;
            result = min + ((result < 0 ? -result : result) % (max - min + 1));
            return result;
        }
    }
}
