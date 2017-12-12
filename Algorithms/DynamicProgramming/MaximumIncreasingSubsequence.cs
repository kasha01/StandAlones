using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// http://www.geeksforgeeks.org/dynamic-programming-set-14-maximum-sum-increasing-subsequence/
namespace Algorithms.DynamicProgramming
{
	// example input array= {1, 101, 2, 3, 100, 4, 5}, then output should be 106 (1 + 2 + 3 + 100)
	// example2 input array= {1, 99, 2, 3, 100, 4, 5}, then output should be 199. note, I cannot loop if seq[j]<seq[i] then add
	// this would give me the wrong answer as it would give me 1+99+2+3+100 not an increasing seq.
	public class MaximumIncreasingSubsequence
    {
        // TODO: Using Segment Tree gives a more efficient log(n) solution
        public int getMaxSum(int[] seq)
        {
            if (seq.Length == 0)
                return 0;

            int[] memo = new int[seq.Length];

            int max = seq[0];
            memo[0] = seq[0];

            for (int i = 1; i < seq.Length; i++)
            {
                // Initialization: max sum at my item is the item initial value 
                memo[i] = seq[i];

                for (int j = 0; j < i; j++)
                {
                    if (seq[j] < seq[i])
                    {
                        // memo holds the maximum sum of the increasing subsequence ending at i
                        memo[i] = Math.Max(memo[i], memo[j] + seq[i]);
                    }
                }

                if (max < memo[i])
                    max = memo[i];
            }

            return max;
        }
    }
}
