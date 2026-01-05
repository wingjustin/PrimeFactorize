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
        int rPow;//r=2^(rPow)

        public MontgomeryModuleMultiply(long mod)
        {
            init(mod);
        }

        private void init(long mod)
        {
            this.mod = mod;

            long n = mod;
            long n_ = 1;
            int rPow = 0;

            while ((n & 1) == 0)
            {
                n >>= 1;
                rPow++;
            }

            for (int i = 1; i < rPow; i = i << 1)
            {
                n_ = n_ * (2 - n_ * n) % (1 << i);
                if (n_ < 0)
                    n_ += (1 << i);
            }

            this.n = n;
            this.n_ = n_;
            this.rPow = rPow;
        }

        private long reduce(long x) // x / r
        {
            long r = 1 << this.rPow;
            long q = ((x % r) * this.n_) % r;
            long a = (x - q * this.n) >> this.rPow;

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
