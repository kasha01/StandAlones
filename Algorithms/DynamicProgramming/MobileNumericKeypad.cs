using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.DynamicProgramming
{
	//http://www.geeksforgeeks.org/mobile-numeric-keypad-problem/
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


		// geeksforgeeks implementation
		int getCount(char[,] keypad, int n)
		{
			if (keypad == null || n <= 0)
				return 0;
			if (n == 1)
				return 10;

			// left, up, right, down move from current location
			int[] row = { 0, 0, -1, 0, 1 };
			int[] col = { 0, -1, 0, 1, 0 };

			// taking n+1 for simplicity - count[i][j] will store
			// number count starting with digit i and length j
			var count = new int[10, n + 1];
			int i = 0, j = 0, k = 0, move = 0, ro = 0, co = 0, num = 0;
			int nextNum = 0, totalCount = 0;

			// count numbers starting with digit i and of lengths 0 and 1
			for (i = 0; i <= 9; i++)
			{
				count[i, 0] = 0;
				count[i, 1] = 1;
			}

			// Bottom up - Get number count of length 2, 3, 4, ... , n
			for (k = 2; k <= n; k++)
			{
				for (i = 0; i < 4; i++)  // Loop on keypad row
				{
					for (j = 0; j < 3; j++)   // Loop on keypad column
					{
						// Process for 0 to 9 digits
						if (keypad[i,j] != '*' && keypad[i, j] != '#')
						{
							// Here we are counting the numbers starting with
							// digit keypad[i,j] and of length k keypad[i,j]
							// will become 1st digit, and we need to look for
							// (k-1) more digits
							num = keypad[i, j] - '0';
							count[num, k] = 0;

							// move left, up, right, down from current location
							// and if new location is valid, then get number
							// count of length (k-1) from that new digit and
							// add in count we found so far
							for (move = 0; move < 5; move++)
							{
								ro = i + row[move];
								co = j + col[move];
								if (ro >= 0 && ro <= 3 && co >= 0 && co <= 2 &&
								   keypad[ro, co] != '*' && keypad[ro, co] != '#')
								{
									nextNum = keypad[ro, co] - '0';
									count[num, k] += count[nextNum, k - 1];
								}
							}
						}
					}
				}
			}

			// Get count of all possible numbers of length "n" starting
			// with digit 0, 1, 2, ..., 9
			totalCount = 0;
			for (i = 0; i <= 9; i++)
				totalCount += count[i, n];
			return totalCount;
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