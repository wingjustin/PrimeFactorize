using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static prime_factorize.ModuleOperateUtil;

namespace prime_factorize
{
    internal static class MillerRabinTest
    {
        private static int Accuracy = 4;

        internal static bool PrimalityTest(long num, ref long[] consume)
        {
            if (num < 2)
                return false;
            if (num == 2)
                return true;
            if ((num & 1) == 0) // even
                return false;

            long d = num - 1;

            while (((d = (d >> 1) + (d < 0 ? (d & 1) : 0)) & 1) == 0)
            {
                consume[(int)Pollards_Rho_Consume.All]++;
                consume[(int)Pollards_Rho_Consume.PrimalityTest]++;
            }

            for (long i = 0; i < Accuracy; i++)
            {
                if (!MillerTest(num, d, ref consume))
                    return false;
            }

            return true;
        }

        private static bool MillerTest(long num, long d, ref long[] consume)
        {
            long a = num < 5 ? 2 : RandomUtil.LongRandom(2, num - 2);
            long x = PowerModFunction(a, d, num, ref consume);
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
    }
}
