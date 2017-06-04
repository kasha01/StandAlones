using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// http://www.geeksforgeeks.org/dynamic-programming-set-13-cutting-a-rod/
namespace Algorithms.DynamicProgramming
{
    public class CuttingRod
    {
        int[] memo;

        public CuttingRod(int n)
        {
            memo = new int[n];
            for (int i = 0; i < n; i++)
                memo[i] = int.MinValue;

        }

        //My version TopDown
        public int getMaxValue_TopDown(int n, int[] s)
        {
            if (n <= 0)
                return 0;

            int max = int.MinValue;
            for (int i = 0; i < n; i++)
            {
                if (memo[n - 1 - i] < 0)
                    memo[n - 1 - i] = getMaxValue_TopDown(n - 1 - i, s);

                max = Math.Max(max, memo[n - 1 - i] + s[i]);
            }
            return max;
        }

        //Geeks For Geeks Version
        public int getMaxValue_bottomUp2(int n, int[] s)
        {
            int[] val = new int[n+1];
            val[0] = 0;
            int i, j;
            
            for (i = 1; i <= n; i++)
            {
                int max_val = int.MinValue;
                for (j = 0; j < i; j++)
                    max_val = Math.Max(max_val, s[j] + val[i - j - 1]);
                val[i] = max_val;
            }
            return val[n];
        }

        // my version
        public int getMaxValue_bottomUp(int n, int[] s)
        {
            int[] memo = new int[s.Length + 1];
            memo[0] = 0;
            for (int i = 0; i < s.Length; i++)
                memo[i + 1] = s[i];

            int max = int.MinValue;
            for (int i = 1; i <= n; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    int r = i - j;
                    // j = 0 ==> all in remainder (one piece rod)
                    max = Math.Max(memo[j] + memo[i - j], max);
                }
                memo[i] = max;
                max = 0;
            }
            return memo[s.Length];
        }
    }
}
