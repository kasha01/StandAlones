using System;

namespace Algorithms.DynamicProgramming
{
    public class LongestPalindromicSubsequence
    {
        private int[,] map;
        private readonly string s;
        public LongestPalindromicSubsequence(string s)
        {
            this.s = s;
            map = new int[s.Length, s.Length];

            // init map
            for (int i = 0; i < s.Length; i++)
            {
                for (int j = 0; j < s.Length; j++)
                {
                    if (i == j)
                        map[i, j] = 1;
                    else
                        map[i, j] = int.MinValue;
                }
            }
        }

        public int getLPS_naive(int i, int j, string s)
        {
            if (i == j)
                return 1;

            if (i > j)
                return 0;

            if (s[i].Equals(s[j]))
                return getLPS_naive(i + 1, j - 1, s) + 2;

            return Math.Max(getLPS_naive(i + 1, j, s), getLPS_naive(i, j - 1, s));
        }

        public int getLPS_TopDown(int i, int j, string s)
        {
            if (i == j)
                return 1;

            if (i > j)
                return 0;

            if (map[i, j] >= 0)
                return map[i, j];

            int res = 0;
            if (s[i].Equals(s[j]))
            {
                res = getLPS_TopDown(i + 1, j - 1, s) + 2;
            }
            else
            {
                res = Math.Max(getLPS_TopDown(i + 1, j, s), getLPS_TopDown(i, j - 1, s));
            }

            map[i, j] = res;
            return res;
        }
    }
}
