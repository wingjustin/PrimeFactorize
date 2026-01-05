using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prime_factorize
{
    public static class Pollards_Rho
    {
        private static Random rnd = new Random();
        private static int Accuracy = 4;

        public static List<int> Factorize(int n)
        {
            int[] consume;
            List<int> result = Factorize(n, out consume);
            return result;
        }

        public static List<int> Factorize(int n, out int[] consume)
        {
            consume = new int[]{ 0, 0, 0, 0, 0, 0, 0 };
            List<int> result = FactorizeInternal(n, ref consume);
            return result;
        }

        private static List<int> FactorizeInternal(int n, ref int[] consume)
        {
            int f1 = FactorizeSingleInternal(n, ref consume);

            if (f1 == n)
                return new List<int> { n };

            List<int> factors = FactorizeInternal(f1, ref consume);

            int f2 = n / f1;

            if (f2 == n)
                return factors;

            factors.AddRange(FactorizeInternal(f2, ref consume));
            factors.Sort();

            return factors;
        }

        private static int FactorizeSingleInternal(int n, ref int[] consume)
        {
            if (n <= 2)
                return n;
            if (PrimalityTest(n, Accuracy, ref consume))
                return n;

            int sqrtN = (int)Math.Sqrt(n);

            int x = rnd.Next(2, sqrtN + 1);
            int c = rnd.Next(0, sqrtN + 1);

            int gcd = CircleFinding(n, x, c, ref consume);

            while (gcd == n && !PrimalityTest(gcd, Accuracy, ref consume))
            {
                x = rnd.Next(2, sqrtN + 1);
                c = rnd.Next(0, sqrtN + 1);
                gcd = CircleFinding(n, x, c, ref consume);

                consume[(int)Pollards_Rho_Consume.All]++;
                consume[(int)Pollards_Rho_Consume.Common]++;
            }

            return gcd;
        }

        //private static int LongRandom(int min, int max)
        //{
        //    int a = rnd.Next(int.MinValue, int.MaxValue);
        //    int b = rnd.Next(0, int.MaxValue) >> 32;
        //    int result = a | b;
        //    result = min + ((result < 0 ? -result : result) % (max - min + 1));
        //    return result;
        //}

        private static bool PrimalityTest(int num, int accuracy, ref int[] consume)
        {
            if (num < 2)
                return false;
            if (num == 2)
                return true;
            if ((num & 1) == 0) // even
                return false;

            int d = num - 1;

            while (((d = (d >> 1) + (d < 0 ? (d & 1) : 0)) & 1) == 0)
            {
                consume[(int)Pollards_Rho_Consume.All]++;
                consume[(int)Pollards_Rho_Consume.PrimalityTest]++;
            }

            for (int i = 0; i < accuracy; i++)
            {
                if (!MillerTest(num, d, ref consume))
                    return false;
            }

            return true;
        }

        private static bool MillerTest(int num, int d, ref int[] consume)
        {
            int a = num < 5 ? 2 : rnd.Next(2, num - 2);
            int x = PowerModFunction(a, d, num, ref consume);
            if (x == 1 || x == (num - 1))
            {
                consume[(int)Pollards_Rho_Consume.All]++;
                consume[(int)Pollards_Rho_Consume.PrimalityTest]++;
                return true;
            }

            while (d != num - 1)
            {
                //x = (x * x) % num;
                //d *= 2;
                x = MultiplyModFunction(x, x, num, ref consume);
                d = d + d;

                consume[(int)Pollards_Rho_Consume.All]++;
                consume[(int)Pollards_Rho_Consume.PrimalityTest]++;

                if (x == 1)
                    return false;
                if (x == num - 1)
                    return true;
            }

            return false;
        }

        private static int SquareModFunction(int x, int c, int mod, ref int[] consume)
        {
            //return ((x % mod) * (x % mod)) % mod + c % mod;

            return (MultiplyModFunction(x, x, mod, ref consume) + c) % mod;
        }

        public static int PowerModFunction(int x, int y, int mod, ref int[] consume)
        {
            x = x % mod;

            if (x < 2)
                return x;

            int result = 1;

            while (y > 0)
            {
                if ((y & 1) == 1)
                    //result = result * x % mod;
                    result = MultiplyModFunction(result, x, mod, ref consume);

                y >>= 1;
                //x = x * x % mod;
                x = MultiplyModFunction(x, x, mod, ref consume);

                consume[(int)Pollards_Rho_Consume.All]++;
                consume[(int)Pollards_Rho_Consume.PowerModFunction]++;
            }

            return result;
        }

        private static int MultiplyModFunction(int x, int y, int mod, ref int[] consume)
        {
            /*
             (a+b)(c+d)
             = ac + ad + bc + bd
             = ad + bc + (a-b)(c-d) + bc + ad
             = 2ad + 2bc + (a-b)(c-d)
             */

            const int toler = 46340; // sqrt(2147483647)

            consume[(int)Pollards_Rho_Consume.All]++;
            consume[(int)Pollards_Rho_Consume.MultiplyModFunction]++;

            x = x % mod;
            if (x == 1)
                return y;
            if (x == -1)
                return -y;
            if (x == 0)
                return 0;

            y = y % mod;
            if (y == 1)
                return x;
            if (y == -1)
                return -x;
            if (y == 0)
                return 0;

            if (!(x > toler || y > toler))
                return (x * y) % mod;

            int a = (x >> 1) + (x < 0 ? (x & 1) : 0); // [a/2]
            int b = a + (x & 1); // (a mod 2)

            int c = (y >> 1) + (y < 0 ? (y & 1) : 0); // [a/2]
            int d = c + (y & 1); // (a mod 2)

            int tmp1 = a > toler || d > toler ? MultiplyModFunction(a, d, mod, ref consume) : ((a * d) % mod);
            int tmp2 = c > toler || b > toler ? MultiplyModFunction(c, b, mod, ref consume) : ((c * b) % mod);

            int tmp3 = (tmp1 + tmp2) % mod;

            //int m = a - b;
            //int n = c - d;
            //int tmp4 = m > toler || n > toler ? MultiplyModFunction(m, n, mod, ref consume) : (m * n) % mod;
            int tmp4 = (x & 1) * (y & 1);

            return ((tmp3 << 1) % mod + tmp4) % mod;
        }

        private static int CircleFinding(int n, int x, int c, ref int[] consume)
        {
            if (n == 4)
                return 2;

            int y1 = SquareModFunction(x, c, n, ref consume);   // (f)x
            int y2 = SquareModFunction(y1, c, n, ref consume);  // (ff)x

            int diff = y2 - y1;

            consume[(int)Pollards_Rho_Consume.All]++;
            consume[(int)Pollards_Rho_Consume.CircleFinding]++;

            int gcd = 1;

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

        private static int CalcGCD(int a, int b, ref int[] consume)
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
                int tmp = a;
                a = b;
                b = tmp;
            }

            // b = qa + r
            int r = b % a;

            while (r != 0)
            {
                int tmp = r;
                r = a % r;
                a = tmp;

                consume[(int)Pollards_Rho_Consume.All]++;
                consume[(int)Pollards_Rho_Consume.CalcGCD]++;
            } 

            return a;
        }
    }
}
