using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// http://www.geeksforgeeks.org/dynamic-programming-set-20-maximum-length-chain-of-pairs/
namespace Algorithms.DynamicProgramming
{
    /* Note: Unlike in LIS or LCS. In this question, the sequence can be rearranged to give the max length
     * consider the chain {a,b} {a',b'} {a'',b''} {a''',b'''} ...etc  
     * First we sort the sequence so we guarantee an increasing pattern
     * Then we loop thru all pairs and we basically exclude the pairs that don't meet the condition of a'>b
     * Notice that we are sorting by (b) item/second item which gives:
     * Since a<b & a'<b'. and by sorting b<b'<b''... and if b<a' Then ==> a<b<a'<b' (Chain Pair)
     * same thing can be obtained if I sorted by (a) item/first item...but sorting by 1st item (a) will NOT work because:
     * sorting by a can bring a pair with a huge b (a<<b), the huge b value will exclude lots of subsequent pairs with a's bigger than a
     * but smaller than the huge b. Thus this won't give me the max chain length. whereas sorting by (b) will ensure the smallest b will
     * be in the first pair and thus including max number of pairs
     * Consider: {{1,7},{2,3},{4,5},{6,7},{8,9}} this is sorted by (a) item, notice 1st pair b=7 is too big (b>>a) and this is 
     * excluding the 3 pairs infront of it{2,3},{4,5},{6,7}, giving a wrong answer of 2.
     * Whereas sorting by last item ==> {{2,3},{4,5},{1,7},{6,7},{8,9}} wil give the right answer of 4 as, b=3 is the smallest b available
     * and this allows more increasing pairs to be part of the answer.
     * The sort ensures an increasing pattern and the b/item sort ensures max number of pairs is included in the answer.
     */
    public class MaxLenghtOfChainOfPairs
    {
        public void getMaxLength()
        {
            List<Tuple<int, int>> list = new List<Tuple<int, int>>()
            {
                new Tuple<int,int>(5,24),
                new Tuple<int,int>(39,60),
                new Tuple<int,int>(15,28),
                new Tuple<int,int>(27,40),
                new Tuple<int,int>(50,90),
            };

            int[] lip = new int[list.Count];

            // sort the list ascn
            list.Sort(getComparer());

            int j = 0; int lichain = 1;
            for(int i=1; i< list.Count; i++)
            {
                if(list[j].Item2 < list[i].Item1)
                {
                    lichain++;
                    j = i;      // we set j to mark the last item in the chain so far.
                }
            }

            Console.WriteLine(lichain);
        }

        private IComparer<Tuple<int, int>> getComparer()
        {
            return new mycomparer();
        }
    }

    class mycomparer : IComparer<Tuple<int, int>>
    {
        public int Compare(Tuple<int, int> x, Tuple<int, int> y)
        {
            // sort ascn by Item2
            if (x.Item2 < y.Item2)
                return -1;
            else if (x.Item2 > y.Item2)
                return 1;
            else
                return 0;
        }
    }
}
