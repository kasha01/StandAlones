using System;

namespace Algorithms.DynamicProgramming
{
	// http://www.geeksforgeeks.org/dynamic-programming-set-9-binomial-coefficient/

	// the formula for Binomial Coefficient is :
	/*	C(n, k) = C(n-1, k-1) + C(n-1, k)
		C(n, 0) = C(n, n) = 1 */
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
		public int binomialCoeff2(int n, int k)
		{
			var C = new int[k + 1];

			C[0] = 1;  // nC0 is 1

			for (int i = 1; i <= n; i++)
			{
				// Compute next row of pascal triangle using
				// the previous row - Check Road Trip
				for (int j = Math.Min(i, k); j > 0; j--)
					C[j] = C[j] + C[j - 1];
			}
			return C[k];
		}

		// O(k)
		/* Notice the simplified expression for Binomial Coefficient nCk = [n * (n-1) *---* (n-k+1)] / [k * (k-1) *----* 1]
		 * there are k terms in the denominator (from k to 1), and k terms (n to n-k+1) in the numerator. thus we do 
		 * for loop 0 to k. notice also the result is n-i (n;n-1;n-2) multiplied k times in the numerator, and divided by i+1 
		 * (1, 2; .... k) k times in the denominator.
		 */
		int binomialCoeff3(int n, int k)
		{
			int res = 1;

			// Since C(n, k) = C(n, n-k)
			if (k > n - k)
				k = n - k;

			// Calculate value of [n * (n-1) *---* (n-k+1)] / [k * (k-1) *----* 1]
			for (int i = 0; i < k; ++i)
			{
				res *= (n - i); // will start with n (i=0) start term of numerator, and end with n-k+1 (i=k-1 => n-(k-1) = n-k+1)
				res /= (i + 1); // will start with 1 (i=0) end term of denominator, and end with k (i=k-1) 
			}

			return res;
		}


		#region Catalan NUmbeer
		// Applications: number of BST with n keys


		// Catalan number (n+1) = sumattion(i=0->n) [Ci * Cn-i]
		int catalanDP(int n)
		{
			// Table to store results of subproblems
			int[] catalan = new int[n + 1];

			// Initialize first two values in table
			catalan[0] = catalan[1] = 1;

			// Fill entries in catalan[] using recursive formula
			for (int i = 2; i <= n; i++)
			{
				catalan[i] = 0;
				for (int j = 0; j < i; j++)
					catalan[i] += catalan[j] * catalan[i - j - 1];
			}

			// Return last entry
			return catalan[n];
		}

		// Catalan number can also be calculated with binomial coefficient, Catalan(x) = (2xCx)/(x+1) i.e. binomial coefficient
		// where k = x, n = 2x
		int catalan(int n)
		{
			// Calculate value of 2nCn
			int c = binomialCoeff3(2 * n, n);

			// return 2nCn/(n+1)
			return c / (n + 1);
		}

		#endregion

	}
}
