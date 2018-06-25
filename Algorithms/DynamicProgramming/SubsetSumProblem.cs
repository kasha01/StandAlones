using System.Collections.Generic;

// http://www.geeksforgeeks.org/dynamic-programming-subset-sum-problem/
namespace Algorithms.DynamicProgramming
{
	class SubsetSumProblem
	{
		/* naive approach 
         * recurssively: last item in set is either included or excluded 
         */
		public static bool dp(int[] set, int n, int sum)
		{
			if (sum == 0)
				return true;

			if (n <= 0 || set[n - 1] > sum)
				return false;

			return
				dp(set, n - 1, sum - set[n - 1])    // last item in set is included (item was subtracted from sum)
				|| dp(set, n - 1, sum);             // last item in set is NOT included (sum is passed as is)
		}

		// get all the subsets, and check the sum of each subset - returns the subset the matches sum O(n^2)
		public static List<int> getSubSets(int[] set, int target_sum)
		{
			// in short the key is generate all binary numbers from (0) to (n-1), then loop through the bits of each number to see what character repsentation it
			// contains e.g. 100 - has the 3rd character in the set, 011, has the 1st and 2nd character in the set and so on
			int n = set.Length;
			List<int> result = new List<int>();
			int ii = 1 << n;
			for (int i = 0; i < ii; i++)
			{
				var l = new List<int>();
				int sum = 0;
				for (int j = 0; j < n; j++)
					if ((i & (1 << j)) > 0)
					{
						sum = sum + set[j];
						l.Add(set[j]);
						if (sum == target_sum) { return l; }
					}
			}
			return null;
		}

		// there is a DP solution on geeksforgeeks.org that one is O(nm) Time, O(nm) Space
		bool isSubsetSum(int[] set, int n, int sum)
		{
			// The value of subset[i][j] will be true if there is a 
			// subset of set[0..j-1] with sum equal to i
			var subset = new bool[n + 1, sum + 1];

			// If sum is 0, then answer is true
			for (int i = 0; i <= n; i++)
				subset[i, 0] = true;

			// If sum is not 0 and set is empty, then answer is false
			for (int i = 1; i <= sum; i++)
				subset[0, i] = false;

			// Fill the subset table in botton up manner
			for (int i = 1; i <= n; i++)
			{
				for (int j = 1; j <= sum; j++)
				{
					// if sum considered >= last item in the subset considered. then I have the potential of have an answer
					// were the sum was of j-last item in the subset considered for the previous subset (subset[i-1][sum diff])
					// or just take the previous answer, which ever is true.
					if (j >= set[i - 1])
						subset[i, j] = subset[i - 1, j] ||
											  subset[i - 1, j - set[i - 1]];
					else
						// else, the sum considered < last set item considered -> just carry over the previous result
						// (shorter subset) for the same sum.
						subset[i, j] = subset[i - 1, j];

				}
			}
			return subset[n, sum];
		}
	}
}
