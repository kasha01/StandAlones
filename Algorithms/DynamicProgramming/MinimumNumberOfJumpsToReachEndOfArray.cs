using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.DynamicProgramming
{
    public class MinimumNumberOfJumpsToReachEndOfArray
    {
        private int[] arr;
        private int[] jumps;
        public MinimumNumberOfJumpsToReachEndOfArray(int[] arr)
        {
            this.arr = arr;
            jumps = new int[arr.Length];
        }

        public void getMinNumberOfJumps()
        {
            int n = arr.Length;

            // init jumps
            jumps[0] = 0;
            for(int i=1; i< n; i++) { jumps[i] = int.MaxValue; }

            int myjump = 0;
            for(int i=0; i < n; i++)
            {
                myjump = arr[i];
				// myjump. what is my max reach from the index (i) I am currently standing at
				// if myjump is zero- ignore as I cannot move.
                while(myjump > 0)
                {
                    if(myjump + i < n)
                    {
						// jumps array value at my targeted destination (myjump capacity + my current standing index) = 
						// min(currently value jumps[myjump+i] , if I am doing a jump from my current stand that is jump[current stand] + 1 since I am jumping)
						// don't confuse, the arr values tells you the max reach you can get, but you do need to jump that is what +1 for
						// so if I am standing at index=2, and arr[2]=5, which means I can reach index 2+5=7 with a single jump
						// that is jump[7]=jump[2]+1 as I did jump. min compares that with the current value of jump[7] if exist
                        jumps[myjump + i] = Math.Min(jumps[myjump + i], jumps[i] + 1);
                        myjump--;
						// decrease my jumping capacity to count for other spots
                    }
                    else
                    {
                        myjump--;
                    }
                }
            }

            Console.WriteLine("Min number of jumps to reach end of array is " + jumps[n - 1]);
        }

		// GeeksforGeeks impl. essentially same as above except the jumps (j) are counted from 0 -> to destination
		// here I am doing extra inner for loops, I will have wasted for loops if (the if statement) not met. which is
		// something I am avoiding in my solution
		int minJumps(int[] arr, int n)
		{
			int[] jumps = new int[n];  // jumps[n-1] will hold the result
			int i, j;

			if (n == 0 || arr[0] == 0)
				return int.MaxValue;

			jumps[0] = 0;

			// Find the minimum number of jumps to reach arr[i]
			// from arr[0], and assign this value to jumps[i]
			for (i = 1; i < n; i++)
			{
				jumps[i] = int.MaxValue;
				for (j = 0; j < i; j++)
				{
					if (i <= j + arr[j] && jumps[j] != int.MaxValue)
					{
						jumps[i] = Math.Min(jumps[i], jumps[j] + 1);
						break;
					}
				}
			}
			return jumps[n - 1];
		}


		// Returns minimum number of jumps to reach arr[n-1] from arr[0]
		int minJumps_o_n(int[] arr, int n)
		{

			// The number of jumps needed to reach the starting index is 0
			if (n <= 1)
				return 0;

			// Return -1 if not possible to jump
			if (arr[0] == 0)
				return -1;

			// initialization
			int maxReach = arr[0];  // stores all time the maximal reachable index in the array.
			int step = arr[0];      // stores the amount of steps we can still take
			int jump = 1;//stores the amount of jumps necessary to reach that maximal reachable position.

			// Start traversing array
			int i = 1;
			for (i = 1; i < n; i++)
			{
				// Check if we have reached the end of the array
				if (i == n - 1)
					return jump;

				// updating maxReach
				maxReach = Math.Max(maxReach, i + arr[i]);

				// we use a step to get to the current index
				step--;

				// If no further steps left
				if (step == 0)
				{
					// we must have used a jump
					// since i reached steps=0, and i am at index (i) then i must have had jumped from the index
					// where steps were originally calculated into the current index I am at (i).
					jump++;

					// Check if the current index/position or lesser index
					// is the maximum reach point from the previous indexes
					if (i >= maxReach)
						return -1;

					// re-initialize the steps to the amount
					// of steps to reach maxReach from position i.
					step = maxReach - i;
				}
			}

			return -1;
		}

	}
}
