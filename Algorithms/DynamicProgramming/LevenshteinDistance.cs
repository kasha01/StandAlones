using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.DynamicProgramming
{
    /// <summary>
    /// minimum operations (insert, remove, replace) to convert string source into string target
    /// </summary>
    public class LevenshteinDistance
    {
        private readonly string source; private readonly string target;
        int[,] memo;
        int col; int row;
        int sourceL; int targetL;

        public LevenshteinDistance(string source, string target)
        {
            this.source = source; this.target = target;
            sourceL = this.source.Length; targetL = this.target.Length;
            col = sourceL + 1; row = targetL + 1;

            memo = new int[row, col];
        }

        public void getLevenshteinDistance()
        {
            // init memo
            for (int i = 0; i < col; i++) { memo[0, i] = i; }
            for (int i = 0; i < row; i++) { memo[i, 0] = i; }

            char s_char = '\0'; char t_char = '\0';
            for (int i = 0; i < targetL; i++)
            {
                t_char = target[i];
                for (int j = 0; j < sourceL; j++)
                {
                    s_char = source[j];

                    if (s_char == t_char)
                    {
                        memo[i + 1, j + 1] = memo[i,j]; // recur the last operation
                    }
                    else
                    {
						// memo[i,j] : replace, memo[i+1,j]: insert, memo[i,j+1]:remove
                        memo[i + 1, j + 1] = 1 + Math.Min(Math.Min(memo[i + 1, j], memo[i, j + 1]), memo[i, j]);
                    }
                }
            }

            // Print Levenshtein Distance
            Console.WriteLine(String.Format("Minimum operations to convert {0} to {1} is {2}", source, target, memo[row - 1, col - 1]));

            // TODO: Track the operations
        }
    }
}
