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

        public Knapsack(int[] w, int[] v, int W)
        {
            this.weight = w;
            this.value = v;
            this.MaxWeight = W;
        }

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
            while (_w > 0)
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
    }
}
