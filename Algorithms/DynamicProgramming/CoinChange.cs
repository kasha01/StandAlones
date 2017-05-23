using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Thanks to Rohan Laishram
/// http://www.geeksforgeeks.org/dynamic-programming-set-7-coin-change/
/// </summary>
namespace Algorithms.DynamicProgramming
{
    /* The memo has the attempts to obtain each value upto sum. and the number of ways at each value is increment of old no of ways +
     * number of ways to obtain (V-coin) value. if I have a coin = 1, then surely the sum=3 can be obtained from the no of ways of 2.
     * because 1(mycoin) + 2 = myvalue(3)...and if 2 can be obtained by 2 ways, then I can obtain 3 by 2 ways also
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

        public void getCoinChangeCount()
        {
            memo[0] = 1;

            for (int i = 0; i < m; i++)
            {
                // for each coin
                for (int j = coins[i]; j <= sum; j++)
                {
                    memo[j] = memo[j] + memo[j - coins[i]];
                }
            }

            Console.WriteLine("coin chage ways are: " + memo[sum]);
        }
    }
}