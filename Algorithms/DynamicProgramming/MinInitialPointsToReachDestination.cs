using System;

// http://www.geeksforgeeks.org/minimum-positive-points-to-reach-destination/
namespace Algorithms.DynamicProgramming
{
	/*
	 * Given a grid with each cell consisting of positive, negative or no points i.e, zero points.
	 * We can move across a cell only if we have positive points ( > 0 ). Whenever we pass through a cell,
	 * points in that cell are added to our overall points. We need to find minimum initial points to reach
	 * cell (m-1, n-1) from (0, 0). 
	 * I move from cell(0,0), my goal is to reach (m-1,n-1) cell and I must have a value of > 0 when I reach my destination.
	 * I can't move diagonally.
	 */
	public class MinInitialPointsToReachDestination
	{
		int R = 0; int C = 0;

		public void Driver(int rows, int columns)
		{
			int[,] arr ={ {-2, -3,   3},
						{-5, -10,  10},
						{10,  30, -5}
					  };

			R = rows; C = columns;
			int res = minInitialPoints(arr);

		}

		public int minInitialPoints(int[,] points)
		{
			// dp[i][j] represents the minimum initial points player
			// should have so that when starts with cell(i, j) successfully
			// reaches the destination cell(m-1, n-1).
			/* i.e. dp[2,2] will be in this case 6 =>meaning, whoever reaches me (2,2), needs to be at least 6. which makes
			 * sense, since arr[2,2] = -5, so whoever reaches [2,2] needs to have 6 in advance, so when it reaches [2,2] it
			 * is 6-5=1 as the requirement states, even when reaching the destination, my total points should be > 0
			 * Another example, now dp[2,2]=6...for arr[1,2]=10...its dp[1,2] would equal to 1, meaning, whoever reaches me
			 * (1,2) needs to have in advances at least 1 point, which makes sense because I'm 10 which is move than the next
			 * destination, so it will be 10+1=11 ->move to [2,2] -> 11-5=6>0 meets requirement. 
			 * Notice I could have said, whoever reaches me [1,2] can be 0 or even -4..BUT the other requirement states I cannot
			 * move from a cell unless my total is >0...that is why I have Math.Max(dp[i + 1, n - 1] - points[i, n - 1], 1);
			 * hypothetically if dp[1,2]=0..that is me telling whoever wants to reach me (1,2) that they can be zero, so 
			 * player at [0,2] and [1,1] can have a total of zero points to reach [1,2], but this is wrong as they won't be able
			 * to move in the first place.
			 * In short, dp[i,j] is telling the other cells the amount of points they need to reach point[i,j]
			 * so dp[0,0] will be telling me the minimum amount of points I need to start with at [0,0] in order to reach the
			 * destination with total points > 0
			*/
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
