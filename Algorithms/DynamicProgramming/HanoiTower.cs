using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.DynamicProgramming
{
    // https://en.wikipedia.org/wiki/Tower_of_Hanoi

    public class HanoiTower
    {
        Stack<int> disks;
        Stack<int> aux;
        Stack<int> target;

        public HanoiTower(Stack<int> ds)
        {
            this.disks = ds;
            this.aux = new Stack<int>();
            this.target = new Stack<int>();
        }

        public void buildTower(int n)
        {
            move(n, this.disks, this.target, this.aux);

            // print target Tower
            while (target.Count > 0)
            {
                Console.WriteLine(target.Pop());
            }
        }

        /*
         * Take a simple case of 2 disks (n=2), What needs to be done is --> move n-1(top disk) to aux, move n disk (bottom disk) to target, move n-1 disk to target
         * Now we can do that recurssively for n number of disks. when n reaches zero, there is no more disks to move and we return.
         * n-1 --> is to keep track which disk we are moving, so we know when to terminate
         * It is hard to track it recurssively but since we solved the sub problem (2 disks), n disks will naturally follow
         */
        private void move(int n, Stack<int> source, Stack<int> target, Stack<int> aux)
        {
            if (n > 0)
            {
                move(n - 1, source, aux, target);       // move source to aux (notice how aux became a target in the method call)
                target.Push(source.Pop());              // move/put source to target
                move(n - 1, aux, target, source);       // move aux to target (notice how aux became the source in the method call)
            }
        }
    }
}
