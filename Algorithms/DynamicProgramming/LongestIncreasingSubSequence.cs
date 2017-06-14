using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.DynamicProgramming
{
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

        // This also works but 1st approach is better as I can implicity initialize lic array
        public void getLIS_2()
        {
            int n = arr.Length;
            int[] lic = new int[n];

            for (int i = 0; i < n; i++)
                lic[i] = 1;

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (arr[i] < arr[j])
                    {
                        lic[j] = Math.Max(lic[j], lic[i] + 1);
                    }
                }
            }

            int mx = 0;
            for (int i = 0; i < n; i++)
                mx = Math.Max(mx, lic[i]);

            Console.WriteLine("Longest Increasing subsequence Length is " + mx);
        }
    }
}

