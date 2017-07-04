using System;
using System.Collections.Generic;

// http://www.geeksforgeeks.org/largest-independent-set-problem/
namespace Algorithms.DynamicProgramming
{
    public class LargestIndependentSet
    {
        class Node
        {
            public Node left;
            public Node right;
            public Node parent;
            public int data;
            public int Included = 1;
            public int NotIncluded = 0;
            public List<int> includes;
            public List<int> notincludes;

            public Node(int d)
            {
                this.data = d;
                this.left = null;
                this.right = null;
                includes = new List<int>();
                notincludes = new List<int>();
            }
        }

        Node root;

        public LargestIndependentSet()
        {
            // Driver
            this.root = new Node(10);
            var n2 = new Node(20);
            var n3 = new Node(30);
            var n4 = new Node(40);
            var n5 = new Node(50);
            var n6 = new Node(60);
            var n7 = new Node(70);
            var n8 = new Node(80);

            root.left = n2;
            root.right = n3;

            n2.left = n4;
            n2.right = n5;

            n5.left = n7;
            n5.right = n8;

            n3.right = n6;
        }

        public void solvemedumb()
        {
            postorder(this.root);
            Console.WriteLine(this.root.Included + " " + this.root.NotIncluded);
            Console.WriteLine("Largest Independent Set: " + Math.Max(this.root.Included, this.root.NotIncluded));
            Console.WriteLine("Largest Independent Set is: ");

            List<int> list = root.Included > root.NotIncluded ? root.includes : root.notincludes;

            foreach (int item in list)
                Console.Write(item + " ");
        }

        void postorder(Node n)
        {
            if (n == null)
            {
                return;
            }

            postorder(n.left);
            postorder(n.right);

            int lx = n.left != null ? n.left.NotIncluded : 0;
            int rx = n.right != null ? n.right.NotIncluded : 0;
            int li = n.left != null ? n.left.Included : 0;
            int ri = n.right != null ? n.right.Included : 0;

            var llx = n.left != null ? n.left.notincludes : new List<int>();
            var rrx = n.right != null ? n.right.notincludes : new List<int>();
            var lli = n.left != null ? n.left.includes : new List<int>();
            var rri = n.right != null ? n.right.includes : new List<int>();

            n.Included = lx + rx + 1;
            n.NotIncluded = Math.Max(li, lx) + Math.Max(ri, rx);

            n.includes.Add(n.data);
            n.includes.AddRange(llx);
            n.includes.AddRange(rrx);

            if (lli.Count > llx.Count)
            {
                n.notincludes.AddRange(lli);
            }
            else
            {
                n.notincludes.AddRange(llx);
            }

            if (rri.Count > rrx.Count)
            {
                n.notincludes.AddRange(rri);
            }
            else
            {
                n.notincludes.AddRange(rrx);
            }
        }
    }
}