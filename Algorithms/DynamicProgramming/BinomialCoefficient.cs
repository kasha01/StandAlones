using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.DynamicProgramming
{
    // http://www.geeksforgeeks.org/dynamic-programming-set-9-binomial-coefficient/
    public class BinomialCoefficient
    {
        int n; int k;
        int[] memo;
        public BinomialCoefficient(int n, int k)
        {
            memo = new int[n - k + 1];
            this.n = n; this.k = k;
        }

        public void getBinomialCoefficient()
        {
            if (n < k)
            {
                Console.WriteLine("Binomial Coefficient is " + 0);
                return;
            }

            memo[0] = 1;    // there is one way to arrange k elements in k seats
            int sz = memo.Length;
            int nn = k;     // number of elements in the subset - start from k elements --> n elemetns
            for (int i = 1; i < sz; i++)
            {
                memo[i] = memo[i - 1] + nn;
                nn++;
            }

            Console.WriteLine("Binomial Coefficient is " + memo[sz - 1]);
        }
    }
}
