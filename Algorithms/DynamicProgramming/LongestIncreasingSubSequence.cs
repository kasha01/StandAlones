using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.DynamicProgramming
{
    /* To find the LCS of sequences s1 & s2, we need to find the LCS of their subsequence and to find that, we need to find the length 
     * of their sub-sub sequence and so on, we have two cases, if the X[last] = Y[last] that means their LCS is
     * the LCS of (X[last-1], Y[last-1]) in other words it is the previous solution.
     * second case if X[last] != Y[last] here we have two sub cases:
     * A) since X[last] is not common as it doesn't equal Y[last] we can omit it and find LCS of (X[last-1], Y[last]) OR
     * B) we can do this: since Y[last] is not common as it doesn't equal X[last] we can omit it and find LCS of (X[last], Y[last-1])
     * and get the Max of the two cases A,B.
     * Check AlgoNotes word doc
     */
    public class LongestIncreasingSubSequence
    {
        private int[] arr;
        private int[] lis;
        public LongestIncreasingSubSequence(int[] arr)
        {
            this.arr = arr;
            lis = new int[arr.Length];
        }

        public void LIS()
        {
            int n = arr.Length;
            lis[0] = 1;

            for (int i = 1; i < n; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (arr[j] < arr[i])
                    {
                        lis[i] = Math.Max(lis[i], lis[j] + 1);
                    }
                    else { lis[i] = Math.Max(lis[i], 1); }
                }
            }

            // Pick Max of LIS - To print LIS elements, I need to sort my array by LIS - use Priority Queue
            // TODO: Print the elements of LIS, need to sort array by their LIS values...use Priority Queue
            List<int> myLIS = new List<int>();
            int max = 0;
            for (int i = 0; i < n; i++)
            {
                if (lis[i] > max)
                    max = lis[i];
            }

            Console.WriteLine("Count of LIS is: " + max);
        }
    }
}
