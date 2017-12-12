using System;

/// <summary>
/// Thanks to Rohan Laishram
/// http://www.geeksforgeeks.org/dynamic-programming-set-7-coin-change/
/// </summary>
namespace Algorithms.DynamicProgramming
{
	/* The memo has the attempts to obtain each value upto sum. and the number of ways at each value is increment of old no of ways +
     * number of ways to obtain (V-coin) value. if I have a coin = 1, then surely the sum=3 can be obtained from the no of ways of 2.
     * because 1(mycoin) + 2 = myvalue(3)...and if 2 can be obtained by 2 ways, then I can obtain 3 by 2 ways also.
     
     * This idea can be implemented in a more optimized way using O(n) space, as I loop through all coins, and then loop through the sums that are
     * greater than my coin, as in, I am on sum=3, and I have coin=2, then number of ways is table[2]=table[2]+table[0]. as table[0] has the count
     * of ways to get sum=1; and as I said above to get sum=3. number of ways equals all the ways to get previous sums in which they (the prev sums) are
     * the difference of my current sum - my available coin. So I loop through all coins and all the sums...table[n-1] will have my final result
     */
	public class CoinChange
	{
		private int sum;
		private int m;
		private int[] coins;
		private int[] memo;
		public CoinChange(int n, int[] coins)
		{
			this.sum = n;
			this.coins = coins;
			this.m = coins.Length;
			this.memo = new int[n + 1];
		}

		public int getCoinChangeNaive(int[] coins, int m, int sum)
		{
			if (sum == 0)
				return 1;

			if (m <= 0 || sum < 0)
				return 0;

			return getCoinChangeNaive(coins, m - 1, sum) + getCoinChangeNaive(coins, m - 1, sum - coins[m - 1]);
		}

		// bottom up - geeksforgeeks implementation
		public int count(int[] S, int m, int sum)
		{
			int i, j, x, y;

			// rows: sum increments, columns:coins
			int[,] table = new int[sum + 1, m];

			// when sum = zero
			for (i = 0; i < m; i++)
				table[0, i] = 1;

			// Fill rest of the table enteries in bottom up manner  
			for (i = 1; i < sum + 1; i++)
			{
				for (j = 0; j < m; j++)
				{
					// Count of solutions including S[j]
					x = (i - S[j] >= 0) ? table[i - S[j], j] : 0;

					// Count of solutions excluding S[j]
					y = (j >= 1) ? table[i, j - 1] : 0;

					// total count
					table[i, j] = x + y;
				}
			}
			return table[sum, m - 1];
		}

		public void getCoinChangeCount()
		{
			memo[0] = 1;

			for (int i = 0; i < m; i++)
			{
				// for each coin
				// I start from mycoin coins[i], because for all sums < the designated coin in the outer loop, 
				// there is zero ways to get that sum, basically memo[j-coins[i]] will go out of bound.
				for (int j = coins[i]; j <= sum; j++)
				{
					memo[j] = memo[j] + memo[j - coins[i]];
				}
			}

			Console.WriteLine("coin chage ways are: " + memo[sum]);
		}
	}
}