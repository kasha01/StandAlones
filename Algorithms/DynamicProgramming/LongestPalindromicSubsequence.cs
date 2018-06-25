using System;

/*
 * The whole idea is calculating the Longest Palindromic of a sequence starting from i --> to j: L[i][j]
 * There are 3 main cases:
 * 1) if there is only 1 character. i.e. i=j => length of subsequence is 1. L[1][1], L[2][2] ...etc ==> then L[i][i] always equals 1
 * 2) if X[i] == X[j] ==> L[i][j] = L[i+1][j-1]+2 => length of my subsequence = 2 (since two chars are equal) + length of subsequence before me
 * 3) if X[i] != X[j] ==> L[i][j] = Max{L[i][j-1], L[i+1][j]} => length of my subsequence = Max of the length of subsequences before me, that is a 
 * subsequence of Length -1...so it either starts at i+1->j OR starts at i->but ends at j-1. This is mainly to carry out the Max Length I have so
 * far, to carry it out for further calculations in the bottom up process
 * Notice if the letters are equal as in step 2...The sequence before me is i+1,j-1...as I want the length of the previous Palindrome which HAS to be
 * at an even length less than mine....so hypothetically, if I did L[i+1][j]...this will leave my subsequence at an odd count and I cannot have +2 to it
 * making the whole step invalid...Just saying. 
 * Notice also in step 3, I don't have a +2 b/c the letters are NOT equal, and I merely want to carry the Max solution I have so far
 * NOTE: This is longest Palindromic SUB-SEQUENCE, which is different from Sub-String. in Sub-sequence, elements don't have
 * to be contiguous/in order, that is why in case 2, I don't have to check if the "substring" behind me is also truely
 * palindormic.
 * EX: "abada" -> on i=0,j=4 X[0]==X[4] which will give me L[0][4] = L[i+1][j-1]+2 = L[1][3] + 2 = 3
 * EX: "abadaa" -> on i=0,j=5 X[0]==X[5] which will give me L[0][5] = L[i+1][j-1]+2 = L[1][4] + 2 = 3+2=5. notice L[1][4] which
 * represents the sequence "bada" has a palindrome of length 3, but "abadaa" substring has NO palindrome of length 5, as
 * a + "bada" + a, dont form a true palindrome, but I don't care about the order since I am talking about subsequence,
 * and surely abadaa has a palindromic subsequence of 5 that is "aabaa" or "aadaa". the length is correct.
*/

namespace Algorithms.DynamicProgramming
{
	public class LongestPalindromicSubsequence
	{
		private int[,] map;
		private readonly string s;
		public LongestPalindromicSubsequence(string s)
		{
			this.s = s;
			map = new int[s.Length, s.Length];

			// init map
			for (int i = 0; i < s.Length; i++)
			{
				for (int j = 0; j < s.Length; j++)
				{
					if (i == j)
						map[i, j] = 1;
					else
						map[i, j] = int.MinValue;
				}
			}
		}

		public int getLPS_naive(int i, int j, string s)
		{
			if (i == j)
				return 1;

			if (i > j)
				return 0;

			if (s[i].Equals(s[j]))
				return getLPS_naive(i + 1, j - 1, s) + 2;

			return Math.Max(getLPS_naive(i + 1, j, s), getLPS_naive(i, j - 1, s));
		}

		public int getLPS_TopDown(int i, int j, string s)
		{
			if (i == j)
				return 1;

			if (i > j)
				return 0;

			if (map[i, j] >= 0)
				return map[i, j];

			int res = 0;
			if (s[i].Equals(s[j]))
			{
				res = getLPS_TopDown(i + 1, j - 1, s) + 2;
			}
			else
			{
				res = Math.Max(getLPS_TopDown(i + 1, j, s), getLPS_TopDown(i, j - 1, s));
			}

			map[i, j] = res;
			return res;
		}

		// longest increasing SUBSTRING
		int longestPalSubstr(string str)
		{
			int n = str.Length; // get length of input string

			// table[i][j] will be false if substring str[i..j]
			// is not palindrome.
			// Else table[i][j] will be true
			var table = new bool[n, n];

			// All substrings of length 1 are palindromes
			int maxLength = 1;
			for (int i = 0; i < n; ++i)
				table[i, i] = true;

			// check for sub-string of length 2.
			int start = 0;
			for (int i = 0; i < n - 1; ++i)
			{
				if (str[i] == str[i + 1])
				{
					table[i, i + 1] = true;
					start = i;
					maxLength = 2;
				}
			}

			// Check for lengths greater than 2. k is length
			// of substring
			for (int k = 3; k <= n; ++k)
			{
				// Fix the starting index
				for (int i = 0; i < n - k + 1; ++i)
				{
					// Get the ending index of substring from
					// starting index i and length k
					int j = i + k - 1;

					// checking for sub-string from ith index to
					// jth index iff str[i+1] to str[j-1] is a
					// palindrome
					if (table[i + 1, j - 1] && str[i] == str[j])
					{
						table[i, j] = true;

						if (k > maxLength)
						{
							start = i;
							maxLength = k;
						}
					}
				}
			}

			Console.WriteLine(str.Substring(start, start + maxLength - 1));

			return maxLength; // return length of LPS
		}
	}
}
