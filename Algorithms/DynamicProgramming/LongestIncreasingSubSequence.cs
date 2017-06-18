using System;
using System.Collections.Generic;

// http://www.geeksforgeeks.org/longest-monotonically-increasing-subsequence-size-n-log-n/
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

        #region Log(N^2) Solutions
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
        #endregion

        #region NLog(N) Solution
        /* This works for unique list
         * end element of shorter candidate subsequencee is ALWAYS smaller than the end element of a longer candidate subsequence.
         * There are 3 cases:
         * Case 1: if X < end element of shorted active list (smallest end element) ==> start a new active list with X element.
         * Case 2: if X > end element of longest active list (largest end element)  ==> extend the longest active list by X.
         * Case 3: if X is between smallest end element and largest end element ==> find the end element of an active list that's a ceiling of X
         * i.e. if end element of an active list is 9, and end element of another active list is 11 and X = 7..then 9 is the Ceiling of X=7 =>
         * thus replace end element of that active list (ending 9) with 7.
         * The longest active list is the length of the LIS.
         * In this Algorithm, I don't need to keep full active lists. I just need to keep the end elements of the active lists. in which
         * The smallest end element is tailtable[0], largest end element is the last element in tailtable, elements in between do get replaced
         * if an A[i]=X appears in which that end element is a ceiling of X.
         */


        /* 
         * Find the value A[i] that is smaller than key
         * Ceiling index only called in Case 3 situation
         * there are 3 methods to find Ceiling of x
         */

        // Original method - hard to understand - Use Binary Search - O(LogN)
        int CeilIndex(int[] A, int l, int r, int key)
        {
            while (r - l > 1)
            {
                int m = l + (r - l) / 2;
                if (A[m] >= key)
                    r = m;
                else
                    l = m;
            }

            return r;
        }

        // Binary Search method O(LogN)
        int CeilIndexBS2(int[] A, int l, int h, int key)
        {
            if (A[l] >= key)
                return l;

            int mid = (l + h) / 2;

            if (A[mid] == key)
                return mid;
            else if (key > A[mid])
            {
                if (mid + 1 <= h && A[mid + 1] >= key)
                    return mid + 1;
                else
                    return CeilIndexBS2(A, mid + 1, h, key);
            }
            else if (key < A[mid])
            {
                if (mid - 1 <= l && A[mid - 1] < key)
                    return mid;
                else
                    return CeilIndexBS2(A, l, mid - 1, key);
            }

            throw new InvalidOperationException("Ceiling of Key " + key + " couldn't be found!");
        }

        // Linear Search method O(N)
        int CeilIndexN(int[] A, int l, int h, int key)
        {
            if (A[0] >= key)
                return 0;

            for (int i = l; i < h; i++)
            {
                if (A[i] == key)
                    return i;

                if (A[i] < key && A[i + 1] >= key)
                    return i + 1;
            }

            // This should have been a case 2
            throw new InvalidOperationException("Ceiling of Key " + key + " couldn't be found!");
        }

        public int LongestIncreasingSubsequenceLength(int[] A, int size)
        {
            // Add boundary case, when array size is one

            int[] tailTable = new int[size];
            int len; // always points empty slot

            tailTable[0] = A[0];
            len = 1;
            for (int i = 1; i < size; i++)
            {
                int a = A[i];
                if (A[i] < tailTable[0])
                    // new smallest value  - Case 1
                    tailTable[0] = A[i];

                else if (A[i] > tailTable[len - 1])
                    // A[i] wants to extend largest subsequence - Case 2
                    tailTable[len++] = A[i];

                else
                    // Case 3
                    // A[i] wants to be the current end candidate of an existing
                    // subsequence. So It needs to replace ceil value in tailTable

                    // tailTable[CeilIndex(tailTable, -1, len - 1, A[i])] = A[i];
                    // tailTable[CeilIndexN(tailTable, 0, len - 1, A[i])] = A[i];
                    tailTable[CeilIndexBS2(tailTable, 0, len - 1, A[i])] = A[i];
            }

            return len;
        }
        #endregion

        #region Variation of LIS

        // Cities are 1-1 correspondance
        // http://www.geeksforgeeks.org/dynamic-programming-set-14-variations-of-lis/
        public void riverCrossingBridges(int[] cityN, int[] cityS, int n)
        {
            //put city South in a dic CityValue:Index
            Dictionary<int, int> map = new Dictionary<int, int>();
            for (int i = 0; i < n; i++)
                map.Add(cityS[i], i);

            int[] lis = new int[n];
            lis[0] = 1;
            for (int i = 1; i < n; i++)
            {
                lis[i] = 1; // you can have a minimum 1 bridge
                for (int j = i - 1; j >= 0; j--)
                {
                    if (map[cityN[j]] < map[cityN[i]])
                        lis[i] = Math.Max(lis[j] + 1, lis[i]);
                }
            }

            int max = 1;
            for (int i = 0; i < n; i++)
                max = Math.Max(max, lis[i]);

            Console.WriteLine("Maximum Number of non crossing bridges is " + max);
        }

        #endregion
    }
}

