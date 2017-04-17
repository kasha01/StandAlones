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

    }
}
