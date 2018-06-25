using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix
{
	class Program
	{
		// not mine

		static void invalidate_validate_matrix(int x, int y, int[,] matrix, int n, bool b)
		{
			int z = 1;
			if (b)
			{
				// make it valid
				z = -1;
			}
			else
			{
				// make in valid. +1
			}

			int tempx = x; int tempy = y;

			tempx = tempx + 1;
			tempy = tempy + 1;
			while (tempx >= 0 && tempy >= 0 && tempx < n && tempy < n)
			{
				matrix[tempx, tempy] = matrix[tempx, tempy] + z;
				tempx = tempx + 1;
				tempy = tempy + 1;
			}

			tempx = x; tempy = y;
			tempx = tempx + 1;
			tempy = tempy - 1;
			while (tempx >= 0 && tempy >= 0 && tempx < n && tempy < n)
			{
				matrix[tempx, tempy] = matrix[tempx, tempy] + z;
				tempx = tempx + 1;
				tempy = tempy - 1;
			}

			tempx = x; tempy = y;
			tempx = tempx - 1;
			tempy = tempy + 1;
			while (tempx >= 0 && tempy >= 0 && tempx < n && tempy < n)
			{
				matrix[tempx, tempy] = matrix[tempx, tempy] + z;
				tempx = tempx - 1;
				tempy = tempy + 1;
			}

			tempx = x; tempy = y;
			tempx = tempx - 1;
			tempy = tempy - 1;
			while (tempx >= 0 && tempy >= 0 && tempx < n && tempy < n)
			{
				matrix[tempx, tempy] = matrix[tempx, tempy] + z;
				tempx = tempx - 1;
				tempy = tempy - 1;
			}
		}

		static bool validate_matrix(int x, int y, int[,] matrix, int n)
		{
			int tempx = x; int tempy = y;

			tempx = tempx + 1;
			tempy = tempy + 1;
			while (tempx >= 0 && tempy >= 0 && tempx < n && tempy < n)
			{
				if (matrix[tempx, tempy] != 0) { return false; }
				tempx = tempx + 1;
				tempy = tempy + 1;
			}

			tempx = x; tempy = y;
			tempx = tempx + 1;
			tempy = tempy - 1;
			while (tempx >= 0 && tempy >= 0 && tempx < n && tempy < n)
			{
				if (matrix[tempx, tempy] != 0) { return false; }
				tempx = tempx + 1;
				tempy = tempy - 1;
			}

			tempx = x; tempy = y;
			tempx = tempx - 1;
			tempy = tempy + 1;
			while (tempx >= 0 && tempy >= 0 && tempx < n && tempy < n)
			{
				if (matrix[tempx, tempy] != 0) { return false; }
				tempx = tempx - 1;
				tempy = tempy + 1;
			}

			tempx = x; tempy = y;
			tempx = tempx - 1;
			tempy = tempy - 1;
			while (tempx >= 0 && tempy >= 0 && tempx < n && tempy < n)
			{
				if (matrix[tempx, tempy] != 0) { return false; }
				tempx = tempx - 1;
				tempy = tempy - 1;
			}

			return true;
		}

		static bool validate(int r, int c, int n, int[,] matrix, List<bool> rows, List<bool> columns)
		{
			if (rows[r] || columns[c] || matrix[r, c] != 0)
			{
				return false;
			}
			return true;
			// validation matrix
			//return validate_matrix(r, c, matrix, n);
		}

		static void bt(int row, int n, List<int> vec, int[,] matrix, List<bool> rows, List<bool> columns)
		{

			if (row >= n)
			{
				// print result
				for (int i = 0; i < n; i++)
				{
					Console.Write(vec[i] + " ");
				}
				return;
			}

			for (int c = 0; c < n; c++)
			{
				// validate my Queen location, on row, column=c
				if (!validate(row, c, n, matrix, rows, columns))
				{
					continue;
				}

				// it is valid. invalidate other squares, put my queen here
				rows[row] = true;   // invalidate my row - occupied
				columns[c] = true;// invalidate my column
				bool b = false;
				matrix[row, c] = matrix[row, c] + 1;
				invalidate_validate_matrix(row, c, matrix, n, b); // invalidate matrix

				vec.Add(c);

				bt(row + 1, n, vec, matrix, rows, columns);

				// back track
				rows[row] = false;   // invalidate my row
				columns[c] = false;// invalidate my column
				b = true;
				invalidate_validate_matrix(row, c, matrix, n, b); // invalidate matrix
				matrix[row, c] = matrix[row, c] - 1;
				vec.RemoveAt(vec.Count - 1);
			}
		}

		static void Main(string[] args)
		{
			Console.ReadKey();
		}

		#region Gray Code
		// http://www.drdobbs.com/architecture-and-design/logic-101-gray-codes/196604129
		// https://en.wikipedia.org/wiki/Gray_code

		// append reverse of order n gray code to prefix string, and print
		public static void yarg(String prefix, int n)
		{
			if (n == 0) Console.WriteLine(prefix);
			else
			{
				gray(prefix + "1", n - 1);
				yarg(prefix + "0", n - 1);
			}
		}

		// append order n gray code to end of prefix string, and print (TO BE CALLED FIRST FROM MAIN) --> gray("",n);
		public static void gray(String prefix, int n)
		{
			if (n == 0) Console.WriteLine(prefix);
			else
			{
				gray(prefix + "0", n - 1);
				yarg(prefix + "1", n - 1);
			}
		}

		// each bit is xored with the bit before it
		public static void intToGray(int n)
		{
			int x = n >> 1;
			Console.WriteLine(n ^ x);
		}

		/* Each bit will be xored with the more significant bits (ie the bits that come before him)
         * 1 1 1
         * - 1 1 
         * 1 0 1 ==> b3 goes down as is, b2 = b3^b2, b1=b2^b1 (notice how each bit is xored with the all the ones before him)
         * - - 1
         * 1 0 0 ==> b3 goes as it, b2 goes down as is (b3^b2), b1 = b2^b1^b3 (all bits before b1) END
         */
		public static void GrayToint(int num)
		{
			for (int mask = num >> 1; mask != 0; mask = mask >> 1)
			{
				num = num ^ mask;
			}
			Console.WriteLine(num);
		}

		#endregion

		#region PartitionProblem
		// http://www.geeksforgeeks.org/dynamic-programming-set-18-partition-problem/ 
		public static void partition(int[] arr)
		{
			int sum = 0;
			for (int i = 0; i < arr.Length; i++)
				sum = sum + arr[i];

			if (sum % 2 != 0)
				Console.WriteLine("Array cannot be divided into two equal sums");
			else
			{
				int callCount = 0;
				var res = subSetSum(arr, arr.Length, sum / 2, ref callCount);
				string result = res
					? "Array can be divided into two equal sums"
					: "Array cannot be divided into two equal sums";
				Console.WriteLine(result);
			}
		}

		private static bool subSetSum(int[] arr, int n, int sum, ref int c)
		{
			c++;
			if (sum == 0)           // we have achieved a subset with sum/2
				return true;

			if (sum < 0)            // sum is less than zero, arr[n-1] is too big -happens only when arr[n-1] is included.
				return false;

			if (n == 0)             // Lenght of the subset is zero
				return false;

			/*
             * a) item arr[n-1] is excluded
             * b) item arr[n-1] is included       
             * since this is an OR, once a true is returned, this statement will always conclude to true so it will
             * auto return without any subsequent recursive calls.      
             */
			return subSetSum(arr, n - 1, sum, ref c) || subSetSum(arr, n - 1, sum - arr[n - 1], ref c);
		}


		#endregion

		#region Boolean Parenthesization

		static int booleanParenthesization(char[] symb, char[] oper, int n)
		{
			int[,] F = new int[n, n];
			int[,] T = new int[n, n];

			// Fill diaginal entries first
			// All diagonal entries in T[i,i] are 1 if symbol[i]
			// is T (true).  Similarly, all F[i,i] entries are 1 if
			// symbol[i] is F (False)
			for (int i = 0; i < n; i++)
			{
				F[i, i] = (symb[i] == 'F') ? 1 : 0;
				T[i, i] = (symb[i] == 'T') ? 1 : 0;
			}

			// Now fill T[i,i+1], T[i,i+2], T[i,i+3]... in order
			// And F[i,i+1], F[i,i+2], F[i,i+3]... in order
			for (int gap = 1; gap < n; ++gap)
			{
				for (int i = 0, j = gap; j < n; ++i, ++j)
				{
					T[i, j] = F[i, j] = 0;
					for (int g = 0; g < gap; g++)
					{
						// Find place of parenthesization using current value
						// of gap
						int k = i + g;

						// Store Total[i,k] and Total[k+1,j]
						int tik = T[i, k] + F[i, k];
						int tkj = T[k + 1, j] + F[k + 1, j];

						// Follow the recursive formulas according to the current
						// operator
						if (oper[k] == '&')
						{
							T[i, j] += T[i, k] * T[k + 1, j];
							F[i, j] += (tik * tkj - T[i, k] * T[k + 1, j]);
						}
						else if (oper[k] == '|')
						{
							F[i, j] += F[i, k] * F[k + 1, j];
							T[i, j] += (tik * tkj - F[i, k] * F[k + 1, j]);
						}
						else if (oper[k] == '^')
						{
							T[i, j] += F[i, k] * T[k + 1, j] + T[i, k] * F[k + 1, j];
							F[i, j] += T[i, k] * T[k + 1, j] + F[i, k] * F[k + 1, j];
						}
					}
				}
			}
			return T[0, n - 1];
		}

		#endregion

		#region Combination of coins {10,15,30} upto a 1000
		// since all numbers are multiples of 5, so they can be formed by 5 multiple (largest common factor)...so the combination is 10, 15, 20,25,30,35..etc
		static void mt()
		{
			SortedSet<int> set = new SortedSet<int>();
			bool flag = false;
			int[] a = { 10, 15, 55 };
			double k = Math.Pow(2, 15);
			for (long i = 1; i < k; i++)
			{
				if (flag)
					break;
				int sum = 0;
				for (int j = 0; j < 15; j++)
				{
					int x = 1 << j;
					if ((i & x) != 0)
					{
						int c1 = a[(j) % 3];
						int c2 = a[(j) % 3] * ((j) / 3);
						sum = sum + a[(j) % 3] + a[(j) % 3] * ((j) / 3);
					}
				}
				if (sum > 1000) { flag = true; break; }
				if (!set.Contains(sum))
					set.Add(sum);
			}

			foreach (var item in set)
			{
				Console.Write(item + " ");
			}
		}
		#endregion

		#region Misc

		private static List<List<int>> getSubSets(int[] set)
		{
			// in short the key is generate all binary numbers from (0) to (n-1) (the number if n digits),
			// then loop through the bits of each number to see what character repsentation it
			// contains e.g. 100 - represent a subset that has the 3rd character in the set,
			// 011, represetn a subset that has the 1st and 2nd character in the set and so on
			int n = set.Length;
			List<List<int>> result = new List<List<int>>();
			for (int i = 0; i < (1 << n); i++)
			{
				var l = new List<int>();

				for (int j = 0; j < n; j++)
					if ((i & (1 << j)) > 0)
						l.Add(set[j]);

				result.Add(l);
			}
			return result;
		}

		// https://leetcode.com/articles/repeated-string-match/
		// how many times short string A, needs to be repeated so it will contain string B.
		private static int repeatedStringMatch(String A, String B)
		{
			int q = 1;
			StringBuilder S = new StringBuilder(A);
			for (; S.Length < B.Length; q++) S.Append(A);
			if (S.ToString().IndexOf(B) >= 0) return q;
			if (S.Append(A).ToString().IndexOf(B) >= 0) return q + 1;
			return -1;
		}


		#endregion
	}
}