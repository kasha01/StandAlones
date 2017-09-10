using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.DynamicProgramming
{
    public class MinInitialPointsToReachDestination
    {
        int R = 0; int C = 0;

        public void Driver(int[,] arr, int rows, int columns)
        {
            //int[,] arr ={ {-2, -3,   3},
            //            {-5, -10,  1},
            //            {10,  30, -5}
            //          };

            R = rows; C = columns;
            int res = minInitialPoints(arr);

        }

        public int minInitialPoints(int[,] points)
        {
            // dp[i][j] represents the minimum initial points player
            // should have so that when starts with cell(i, j) successfully
            // reaches the destination cell(m-1, n-1)
            int[,] dp = new int[R, C];
            int m = R; int n = C;

            // Base case
            dp[m - 1, n - 1] = points[m - 1, n - 1] > 0 ? 1 :
                       Math.Abs(points[m - 1, n - 1]) + 1;

            // Fill last row and last column as base to fill
            // entire table
            for (int i = m - 2; i >= 0; i--)
                dp[i, n - 1] = Math.Max(dp[i + 1, n - 1] - points[i, n - 1], 1);
            for (int j = n - 2; j >= 0; j--)
                dp[m - 1, j] = Math.Max(dp[m - 1, j + 1] - points[m - 1, j], 1);

            // fill the table in bottom-up fashion
            for (int i = m - 2; i >= 0; i--)
            {
                for (int j = n - 2; j >= 0; j--)
                {
                    int min_points_on_exit = Math.Min(dp[i + 1, j], dp[i, j + 1]);
                    dp[i, j] = Math.Max(min_points_on_exit - points[i, j], 1);
                }
            }

            return dp[0, 0];
        }
    }
}
