namespace Algorithms.Search
{
	// https://www.youtube.com/watch?v=GTJr8OvyEVQ
	// https://github.com/mission-peace/interview/blob/master/src/com/interview/string/SubstringSearch.java
	public class KMP
	{
		//O(nk)
		public bool isPatternExist_naive(char[] text, char[] pattern)
		{
			int i = 0;
			int j = 0;
			int k = 0;
			while (i < text.Length && j < pattern.Length)
			{
				if (text[i] == pattern[j])
				{
					i++;
					j++;
				}
				else
				{
					j = 0;
					k++;
					i = k;
				}
			}
			if (j == pattern.Length)
			{
				return true;
			}
			return false;
		}

		public bool isPatternExist(string text, string pattern)
		{
			int[] lps = ComputeLps(pattern);
			int i = 0; // text pointer
			int j = 0; // pattern pointer

			while (i < text.Length && j < pattern.Length)
			{
				if (text[i] == pattern[j])
				{
					i++; j++;
				}
				else
				{
					if (j == 0)
					{
						i++;
					}
					else
					{
						j = lps[j - 1];
					}
				}
			}

			return j == pattern.Length;
		}

		private int[] ComputeLps(string pattern)
		{
			int[] lps = new int[pattern.Length];

			int j = 0;
			int i = 1;
			while (i < pattern.Length)
			{
				if (pattern[i] == pattern[j])
				{
					lps[i] = j + 1;
					i++; j++;
				}
				else
				{
					if (j == 0)
					{
						lps[i] = 0;
						i++;
					}
					else
					{
						// pushing prefix index j towards the start everytime there is a mismatch
						j = lps[j - 1];
					}
				}
			}
			return lps;
		}
	}
}