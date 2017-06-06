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
                    if (seq[j] < seq[i])
                        lis[i] = Math.Max(lis[i], lis[j] + 1);
                    else if (seq[n - j] < seq[n - i])
                        lis_reverse[n - i] = Math.Max(lis_reverse[n - i], lis_reverse[n - j] + 1);
                    else
                    {
                        lis[i] = lis[j];
                        lis_reverse[n - i] = lis_reverse[n - j];
                    }
                }
            }

            int res = 0;
            for (int i = 0; i < n; i++)
            {
                // LIS + LIS_Reverse (reverse increasing curve which will be the decreasing curve) - 1(-1 is to eliminate the common
                // shared point between LIS and LIS_reverse)
                res = Math.Max(res, lis[i] + lis_reverse[i] - 1);
            }

            Console.WriteLine(res);
        }
    }
}
