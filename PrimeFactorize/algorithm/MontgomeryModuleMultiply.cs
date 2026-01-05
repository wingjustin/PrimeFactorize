using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prime_factorize
{
    public class MontgomeryModuleMultiply
    {
        /*
            ** reducing
              x/r mod n
            = x(r/r)/r mod n
            = x(-n'n + 1)/r mod n
            = (-n'nx + lrn + x)/r mod n
            = [n(-n'x + lr) + x]/r mod n

            let q := n'x mod r

            = [n(-q + l'r) + x]/r mod n
            = (-qn + x)/r mod n

        ---------------------------------------------

            * find n' 
            
            ax = q*2^k + 1

            axax = (q*2^k)^2 + 2*q*2^k + 1
                 = (q*2^k)^2 + 2*q*2^k + 2 - 1
                 = (q*2^k)^2 + 2(q*2^k + 1) - 1
                 = (q*2^k)^2 + 2ax - 1
	 
            2ax  = axax - (q*2^k)^2 + 1

            ax(2 - ax) = -qq*2^(2k) + 1

            ax(2 - ax)[2 - ax(2 - ax)] = q'*2^(4k) + 1

         */

        long mod;
        long y;

        long n;
        long n_;// n'
        int pr; // power of r by 2
        int nTwoPow;

        public MontgomeryModuleMultiply(long mod)
        {
            init(mod);
        }

        private void init(long mod)
        {
            this.mod = mod;

            long n = mod;
            long n_ = 1;
            int nTwoPow = 0;
            int pr = 1; // 2^1

            while ((n & 1) == 0)
            {
                n >>= 1;
                nTwoPow++;
            }

            while ((1 << pr) < n)
            {
                pr += 2;
                long tmp1 = n_ & ((1 << pr) - 1); // n' mod r
                long tmp2 = (1 << pr) + 2 - tmp1 * (n & ((1 << pr) - 1)); // r + 2 - n*(n') mod r
                n_ = (tmp1 * tmp2) & ((1 << pr) - 1);
            }

            this.n = n;
            this.n_ = n_;
            this.pr = pr;
            this.nTwoPow = nTwoPow;

            this.y = 1 << pr;
        }

        private long reduce(long x)
        {
            long q = ((x & ((1 << pr) - 1)) * n_) & ((1 << pr) - 1); // let q := n'x mod r
            long a = (x - q * n) >> pr;

            if (a < 0)
                a += n;

            return a;
        }

        public void Multiply(long x)
        {
            y = reduce(y * x);
        }

        public long Result()
        {
            return y;
        }
    }
}
