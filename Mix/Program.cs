using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 1, 8, 5, 10 };
            partition(arr);
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

        #region Misc

        private List<List<int>> getSubSets(int[] set)
        {
            // in short the key is generate all binary numbers from (0) to (n-1), then loop through the bits of each number to see what character repsentation it
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

        #endregion
    }
}
