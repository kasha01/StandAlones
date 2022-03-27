using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.DynamicProgramming
{
    public class Knapsack
    {
        int[] weight;
        int[] value;
        int MaxWeight;

        public Knapsack() { }
        public Knapsack(int[] w, int[] v, int W)
        {
            this.weight = w;
            this.value = v;
            this.MaxWeight = W;
        }

        /// Knapsack - Max Value
        // unbounded - repetition allowed
		int getKnapsack_Unbounded_maxValue_best(int W, int n, int[] val, int[] wt){
			var dp = new int[W+1];
			int mx = 0;

			for(int i=0;i<n;++i){
                // notice weight here is ++w ==> so dp[w - wt[i]] has the results of i item in it. meaning i-item can be picked up multiple times.
				for(int w=wt[i];w<=W;++w){
					dp[w] = dp[w - wt[i]] + val[i];
					mx = Math.Max(mx, dp[w]);
				}
			}

			return mx;
		}
        
        // bounded - cannot use same item twice        
        int getKnapsack_01_maxValue_best(int W, int []wt, int []val, int n)
        {
            int[] dp = new int[W + 1];

            for (int i = 0; i < n; i++)
            {
                // notice weight here is w-- ==> so dp[w-wt[i]] does not have the result of i-item in it, it has only the results of [0 to i-1] items.
                // thus i-item is only picked once.
                for (int w = W; w >= 0; w--)
                {
                    if (wt[i] <= w)
                        dp[w] = Math.Max(dp[w], dp[w - wt[i]] + val[i]);
                }
            }
            return dp[W]; // returning the maximum value of knapsack
        }        
        
        /// Knapsack - number of ways to make sum s
        // source: https://leetcode.com/problems/coin-change-2/discuss/99212/Knapsack-problem-Java-solution-with-thinking-process-O(nm)-Time-and-O(m)-Space
        /** 
         * @return number of ways to make sum s using repeated coins
         */
        public static int coinrep(int[] coins, int s) {
            int[] dp = new int[s + 1]; 
            dp[0] = 1;          
            for (int coin : coins)      
                for (int i = coin; i <= s; i++)         
                    dp[i] += dp[i - coin];                                  
            return dp[s];
        }                                       

        /**
         * @return number of ways to make sum s using non-repeated coins
         */
        public static int coinnonrep(int[] coins, int s) {
            int[] dp = new int[s + 1];
            dp[0] = 1;  
            for (int coin : coins)
                for (int i = s; i >= coin; i--)
                    dp[i] += dp[i - coin];              
            return dp[s];                                                   
        } 
        
        
        /// Knapsack GeeksForGeeks
        // unbounded
        // https://www.geeksforgeeks.org/unbounded-knapsack-repetition-items-allowed/
        public int getMax_UnboundedKnapsack()
        {
            int itemsCount = weight.Length;             // How many Item I totally have
            int[] m = new int[MaxWeight + 1];
            m[0] = 0;                                   // No item can fit in sub_weight = 0; so Max value solution for sub_weight(0) is Zero

            for (int w = 1; w <= MaxWeight; w++)         // For each weight [0-MaxWeigth]
            {
                int best = 0;
                for (int i = 0; i < itemsCount; i++)    // For each Item - see which Item will give me the Max solution per my sub_weight problem
                {                                       // If there is more weight that can fit the sub_weight, optimal use is to have the solution of the w-item_weight problem
                    if (!(weight[i] <= w)) { continue; }

                    best = Math.Max(value[i] + m[w - weight[i]], best);
                }
                m[w] = best;
            }
            return m[MaxWeight];
        }        

        // geeks for geeks version - bounded
        public int getMax_01Knapsack()
        {
            int itemsCount = value.Length;
            int[,] m = new int[itemsCount + 1, this.MaxWeight + 1];
            for (int i = 0; i <= this.MaxWeight; i++) { m[0, i] = 0; }      // First row (no item selected) = 0
            for (int i = 0; i <= itemsCount; i++) { m[i, 0] = 0; }          // First column (weight=0) = 0

            int itemWeight = 0; int itemValue = 0;
            for (int i = 1; i <= itemsCount; i++)                           // Loop thru all items/rows
            {
                itemWeight = weight[i - 1]; itemValue = value[i - 1];
                for (int w = 1; w <= MaxWeight; w++)                        // Loop thru all sub weights to find optimal solution for all sub_weights per item
                {
                    if (itemWeight > w)                                     // Item weight cannot fit --> pass the previous optimal solution I got for that sub weight
                    {
                        m[i, w] = m[i - 1, w];
                    }
                    else
                    {
                        m[i, w] = Math.Max(m[i - 1, w], m[i - 1, w - itemWeight] + itemValue);  // else get max
                    }
                }
            }

            // get items selected
            // Tuple --> Item Weight, Item Index/Name in weight array
            List<Tuple<int, int>> items = new List<Tuple<int, int>>();
            int sumw = m[itemsCount, MaxWeight]; int _i = itemsCount; int _w = MaxWeight;
            while (_i > 0)
            {
                if (m[_i, _w] == m[_i - 1, _w])
                {
                    _i--;
                }
                else
                {
                    items.Add(new Tuple<int, int>(this.weight[_i - 1], _i - 1));
                    _w = _w - this.weight[_i - 1]; _i--;
                }
            }

            // Print Selected Items
            foreach (var t in items) { Console.WriteLine(String.Format("Item Weight: {0} - Item Index: {1}", t.Item1, t.Item2)); }

            return m[itemsCount, MaxWeight];
        }

        public int getMax_01Knapsack_naive(int W, int[] w, int[] v, int n)
        {
            // knapsack is empty OR there is no more items
            if (W == 0 || n == 0)
                return 0;

            // item is heavier than available knapsack weight, exclude it (n-1) and move on with the remaining items
            if (w[n - 1] > W)
                return getMax_01Knapsack_naive(W, w, v, n - 1);
            
            // return Max if the item exist or not exist in the optimal set
            return Math.Max(v[n - 1] + getMax_01Knapsack_naive(W - w[n - 1], w, v, n - 1),
                getMax_01Knapsack_naive(W, w, v, n - 1));
        }
    }
}
