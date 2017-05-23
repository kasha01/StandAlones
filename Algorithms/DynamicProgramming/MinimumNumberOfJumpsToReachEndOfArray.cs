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

                while(myjump > 0)
                {
                    if(myjump + i < n)
                    {
                        jumps[myjump + i] = Math.Min(jumps[myjump + i], jumps[i] + 1);
                        myjump--;
                    }
                    else
                    {
                        myjump--;
                    }
                }
            }

            Console.WriteLine("Min number of jumps to reach end of array is " + jumps[n - 1]);
        }
    }
}
