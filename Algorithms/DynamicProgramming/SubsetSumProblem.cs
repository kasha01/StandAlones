using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
