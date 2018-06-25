using System;

// http://www.geeksforgeeks.org/dynamic-programming-set-15-longest-bitonic-subsequence/
namespace Algorithms.DynamicProgramming
{
	public class LongestBitonicSubsequence
	{
		public void getLongestBitonic(int[] seq)
		{
			int[] lis = new int[seq.Length];
			int[] lis_reverse = new int[seq.Length];

			int n = seq.Length - 1;
			for (int i = 0; i < seq.Length; i++)
			{
				lis_reverse[n - i] = 1; lis[i] = 1;
				for (int j = 0; j < i; j++)
				{
					// LIS in forward direction
					if (seq[j] < seq[i])
						lis[i] = Math.Max(lis[i], lis[j] + 1);

					// LIS in reverse
					if (seq[n - j] < seq[n - i])
						lis_reverse[n - i] = Math.Max(lis_reverse[n - i], lis_reverse[n - j] + 1);
				}
			}

			int res = 0;
			for (int i = 0; i < n; i++)
			{
				// lis[i] : longest increasing sequence [from 0 to i]
				// lis_reverse[i]: longest increasing sequence [from end to i] = longest decreasing seq [i - to end]
				// LIS + LIS_Reverse (reverse increasing curve which will be the decreasing curve) - 1(-1 is to eliminate the common
				// shared point between LIS and LIS_reverse)
				res = Math.Max(res, lis[i] + lis_reverse[i] - 1);
			}

			Console.WriteLine(res);
		}
	}
}
