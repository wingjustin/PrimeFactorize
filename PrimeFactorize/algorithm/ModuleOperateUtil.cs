using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prime_factorize
{
    internal static class ModuleOperateUtil
    {
        internal static long SquareModFunction(long x, long c, long mod, ref long[] consume)
        {
            //return ((x % mod) * (x % mod)) % mod + c % mod;

            return (MultiplyModFunction(x, x, mod, ref consume) + c) % mod;
        }

        internal static long PowerModFunction(long x, long y, long mod, ref long[] consume)
        {
            x = x % mod;

            if (x < 2)
                return x;

            long result = 1;

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

        internal static long MultiplyModFunction(long x, long y, long mod, ref long[] consume)
        {
            /*
             (a+b)(c+d)
             = ac + ad + bc + bd
             = ad + bc + (a-b)(c-d) + bc + ad
             = 2ad + 2bc + (a-b)(c-d)
             */

            const long toler = 3037000499; // sqrt(9223372036854775807)

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

            long a = (x >> 1) + (x < 0 ? (x & 1) : 0); // [a/2]
            long b = a + (x & 1); // (a mod 2)

            long c = (y >> 1) + (y < 0 ? (y & 1) : 0); // [a/2]
            long d = c + (y & 1); // (a mod 2)

            long tmp1 = a > toler || d > toler ? MultiplyModFunction(a, d, mod, ref consume) : ((a * d) % mod);
            long tmp2 = c > toler || b > toler ? MultiplyModFunction(c, b, mod, ref consume) : ((c * b) % mod);

            long tmp3 = (tmp1 + tmp2) % mod;

            //int m = a - b;
            //int n = c - d;
            //int tmp4 = m > toler || n > toler ? MultiplyModFunction(m, n, mod, ref consume) : (m * n) % mod;
            long tmp4 = (x & 1) * (y & 1);

            return ((tmp3 << 1) % mod + tmp4) % mod;
        }
    }
}
