using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.DynamicProgramming
{
    public class LongestCommonSubSequence
    {
        string s1; string s2;
        int[,] memo; int x; int y; int s1L; int s2L;

        public LongestCommonSubSequence(string s1, string s2)
        {
            this.s1 = s1; this.s2 = s2;
            s1L = s1.Length; s2L = s2.Length;
            x = s1L + 1;  // x: columns count
            y = s2L + 1;  // y: rows count
            memo = new int[x, y];
        }

        public void getCountOfLCS()
        {
            char row_char = '\0';
            char column_char = '\0';

            // init memo - not needed for C# - int arrays gets initialized with 0
            // for (int i = 0; i < x; i++) { memo[0, i] = 0; }
            // for (int i = 0; i < y; i++) { memo[i, 0] = 0; }

            // Do the work
            for (int i = 0; i < s2L; i++)
            {
                // first row
                row_char = s2[i];
                for (int j = 0; j < s1L; j++)
                {
                    column_char = s1[j];
                    if (row_char == column_char)
                    {
                        memo[i + 1, j + 1] = memo[i, j] + 1;
                    }
                    else
                    {
                        memo[i + 1, j + 1] = Math.Max(memo[i, j + 1], memo[i + 1, j]);
                    }
                }
            }

            // Print the length of the LCS
            Console.WriteLine("Length of LCS is: " + memo[x - 1, y - 1]);

            // TODO: track memo to get the LCS
        }
    }
}
