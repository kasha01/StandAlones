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
    }
}
