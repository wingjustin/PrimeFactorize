using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static prime_factorize.MillerRabinTest;
using static prime_factorize.ModuleOperateUtil;

namespace prime_factorize
{
    public static class Pollards_Rho_Long
    {
        public static List<long> Factorize(long n)
        {
            long[] consume;
            List<long> result = Factorize(n, out consume);
            return result;
        }

        public static List<long> Factorize(long n, out long[] consume)
        {
            consume = new long[]{ 0, 0, 0, 0, 0, 0, 0 };
            if (n < 0) n = -n;
            List<long> result = FactorizeInternal(n, ref consume);
            return result;
        }

        private static List<long> FactorizeInternal(long n, ref long[] consume)
        {
            long f1 = FactorizeSingleInternal(n, ref consume);

            if (f1 == n)
                return new List<long> { n };

            List<long> factors = FactorizeInternal(f1, ref consume);

            long f2 = n / f1;

            if (f2 == n)
                return factors;

            factors.AddRange(FactorizeInternal(f2, ref consume));
            factors.Sort();

            return factors;
        }

        private static long FactorizeSingleInternal(long n, ref long[] consume)
        {
            if (n <= 2)
                return n;
            if (PrimalityTest(n, ref consume))
                return n;

            long sqrtN = (long)Math.Sqrt(n);

            long x = RandomUtil.LongRandom(2, sqrtN + 1);
            long c = RandomUtil.LongRandom(0, sqrtN + 1);

            long gcd = CircleFinding(n, x, c, ref consume);

            while (gcd == n && !PrimalityTest(gcd, ref consume))
            {
                x = RandomUtil.LongRandom(2, sqrtN + 1);
                c = RandomUtil.LongRandom(0, sqrtN + 1);
                gcd = CircleFinding(n, x, c, ref consume);

                consume[(int)Pollards_Rho_Consume.All]++;
                consume[(int)Pollards_Rho_Consume.Common]++;
            }

            return gcd;
        }

        

        private static long CircleFinding(long n, long x, long c, ref long[] consume)
        {
            if (n == 4)
                return 2;

            long y1 = SquareModFunction(x, c, n, ref consume);   // (f)x
            long y2 = SquareModFunction(y1, c, n, ref consume);  // (ff)x

            long diff = y2 - y1;

            consume[(int)Pollards_Rho_Consume.All]++;
            consume[(int)Pollards_Rho_Consume.CircleFinding]++;

            long gcd = 1;

            while ((gcd = CalcGCD(diff, n, ref consume)) == 1)
            {
                y1 = SquareModFunction(y1, c, n, ref consume);   // (ff)x
                y2 = SquareModFunction(SquareModFunction(y2, c, n, ref consume), c, n, ref consume); // (ff)(ff)x

                diff = y2 - y1;

                consume[(int)Pollards_Rho_Consume.All]++;
                consume[(int)Pollards_Rho_Consume.CircleFinding]++;

                if (diff == 0)
                    return n;
            }

            return gcd;
        }

        private static long CalcGCD(long a, long b, ref long[] consume)
        {
            consume[(int)Pollards_Rho_Consume.All]++;
            consume[(int)Pollards_Rho_Consume.CalcGCD]++;

            a = a < 0 ? -a : a;
            b = b < 0 ? -b : b;

            if (a == 0)
                return b;

            if (b == 0)
                return a;

            if (a == b)
                return a;

            if(a > b)
            {
                long tmp = a;
                a = b;
                b = tmp;
            }

            // b = qa + r
            long r = b % a;

            while (r != 0)
            {
                long tmp = r;
                r = a % r;
                a = tmp;

                consume[(int)Pollards_Rho_Consume.All]++;
                consume[(int)Pollards_Rho_Consume.CalcGCD]++;
            } 

            return a;
        }
    }
}
