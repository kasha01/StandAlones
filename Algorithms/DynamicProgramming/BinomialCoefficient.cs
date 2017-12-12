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
		// number of way to choose k elements from n objects
		long n; long k;
		long[] memo;
		public BinomialCoefficient(long n, long k)
		{
			memo = new long[n - k + 1];
			this.n = n; this.k = k;
		}

		// naive
		public int getBinomialCoefficient(int n, int k)
		{
			if (n == k || k == 0)
				return 1;

			return getBinomialCoefficient(n - 1, k - 1) + getBinomialCoefficient(n - 1, k);
		}

		public int binomialCoeff(int n, int k)
		{
			int[,] C = new int[n + 1, k + 1];
			int i, j;

			// Caculate value of Binomial Coefficient in bottom up manner
			for (i = 0; i <= n; i++)
			{
				// usually this will be (j = 0; j <= k; j++)
				// but here, for small i's k can get greater than i which is inapplicable. for example if n=4,k=2
				// -> 4C2 = C(4,2)...for the loops of i where i=0,1..j can extend to k, we will have a case of
				// C(0,1) C(0,2) which are all not applicable. so we do a min which will exclude these cases, once
				// the i loop becomes >=k, the inner loop min(i,k) will default to K and we will have j<=k
				//Min is to eliminate the cases of small i (i<k). 
				for (j = 0; j <= Math.Min(i, k); j++)
				{
					// Base Cases
					if (j == 0 || j == i)
						C[i, j] = 1;

					// Calculate value using previosly stored values
					else
						C[i, j] = C[i - 1, j - 1] + C[i - 1, j];
				}
			}

			return C[n, k];
		}

		// O(k) space
		public int binomialCoeff(int n, int k)
		{
			var C = new int[k + 1];

			C[0] = 1;  // nC0 is 1

			for (int i = 1; i <= n; i++)
			{
				// Compute next row of pascal triangle using
				// the previous row
				for (int j = Math.Min(i, k); j > 0; j--)
					C[j] = C[j] + C[j - 1];
			}
			return C[k];
		}
	}
}
