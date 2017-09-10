using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.DynamicProgramming
{
    class MobileNumericKeypad
    {
        int[,] table;

        List<string> validNumKeyNumbers = new List<string>();

        int[,] arr = new int[4, 3]
        {
                {1,2,3 },
                {4,5,6 },
                {7,8,9 },
                {-1,0,-1 },
        };

        public void Driver(int n, bool optimizeSolution)
        {
            int sum = 0;
            table = new int[10, n];
            int counter = 0;

            for (int i = 0; i <= 9; i++)
            {
                if (optimizeSolution)
                {
                    // using Dynamic programming
                    sum = sum + dp2(i, n - 1, i.ToString(), ref counter);
                    table[i, n - 1] = sum;
                }
                else
                {
                    // using recurssion
                    sum = sum + dp(i, n - 1, i.ToString(), ref counter);
                }
            }

            if (!optimizeSolution)
            {
                // Laziness, I can only print it in recurssion...Lazy!
                foreach (var item in validNumKeyNumbers)
                    Console.WriteLine(item);
            }

            Console.WriteLine("Count of method calls: " + counter);
            Console.WriteLine("Number of ways: " + sum);
            // Console.WriteLine(table[9, n - 1]);
        }

        int dp(int s, int n, string st, ref int c)
        {
            c++;
            if (n <= 0)
            {
                validNumKeyNumbers.Add(st);
                return 1;
            }
            List<int> adj = getAdj(s);
            int sum = 0; string nn = st;
            for (int i = 0; i < adj.Count; i++)
            {
                st = st + adj[i].ToString();
                sum = sum + dp(adj[i], n - 1, st, ref c);
                st = nn;
            }
            return sum;
        }

        int dp2(int s, int n, string st, ref int c)
        {
            c++;
            if (n <= 0)
            {
                validNumKeyNumbers.Add(st);
                return 1;
            }
            List<int> adj = getAdj(s);
            for (int i = 0; i < adj.Count; i++)
            {
                if (table[adj[i], n - 1] == 0)
                    table[adj[i], n - 1] = dp2(adj[i], n - 1, st, ref c);

                table[s, n] = table[s, n] + table[adj[i], n - 1];
            }
            return table[s, n];
        }

        private List<int> getAdj(int s)
        {
            int row = 0; int col = 0;

            if (s == 0)
            {
                col = 1; row = 3;
            }
            else if (s % 3 == 0)
            {
                row = (s / 3) - 1;
                col = 2;
            }
            else
            {
                col = (s % 3) - 1;
                row = s / 3;
            }

            List<int> adj = new List<int>();
            adj.Add(arr[row, col]);

            // top
            if (row - 1 >= 0)
                adj.Add(arr[row - 1, col]);

            // bottom
            if (row + 1 < 4 && arr[row + 1, col] != -1)
                adj.Add(arr[row + 1, col]);

            // right
            if (col - 1 >= 0 && arr[row, col - 1] != -1)
                adj.Add(arr[row, col - 1]);

            // left
            if (col + 1 < 3 && arr[row, col + 1] != -1)
                adj.Add(arr[row, col + 1]);

            return adj;
        }
    }
}