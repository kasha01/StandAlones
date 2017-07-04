using System;
using System.Collections.Generic;

// http://www.geeksforgeeks.org/largest-independent-set-problem/

/*
 * The node is either included or excluded from the Set
 * if Node is included ==> its included count = 1 (the node itself) + ExcludedSet of Adjacent nodes
 * if Node is excluded ==> its excluded count = Max(left node Included count, left node Excluded count) + Max(right node Included count, right node Excluded count)
 * 
 */
namespace Algorithms.DynamicProgramming
{
    public class LargestIndependentSet
    {
        class Node
        {
            public Node left;
            public Node right;
            public int data;
            public int includedCount = 1;
            public int excludedCount = 0;
            public List<int> includedNodesList;
            public List<int> excludedNodesList;

            public Node(int d)
            {
                this.data = d;
                this.left = null;
                this.right = null;
                includedNodesList = new List<int>();
                excludedNodesList = new List<int>();
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
            Console.WriteLine(this.root.includedCount + " " + this.root.excludedCount);
            Console.WriteLine("Largest Independent Set: " + Math.Max(this.root.includedCount, this.root.excludedCount));
            Console.WriteLine("Largest Independent Set is: ");

            List<int> list = root.includedCount > root.excludedCount ? root.includedNodesList : root.excludedNodesList;

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

            int lx = n.left != null ? n.left.excludedCount : 0;
            int rx = n.right != null ? n.right.excludedCount : 0;
            int li = n.left != null ? n.left.includedCount : 0;
            int ri = n.right != null ? n.right.includedCount : 0;

            var llx = n.left != null ? n.left.excludedNodesList : new List<int>();
            var rrx = n.right != null ? n.right.excludedNodesList : new List<int>();
            var lli = n.left != null ? n.left.includedNodesList : new List<int>();
            var rri = n.right != null ? n.right.includedNodesList : new List<int>();

            n.includedCount = lx + rx + 1;
            n.excludedCount = Math.Max(li, lx) + Math.Max(ri, rx);

            n.includedNodesList.Add(n.data);
            n.includedNodesList.AddRange(llx);
            n.includedNodesList.AddRange(rrx);

            if (lli.Count > llx.Count)
            {
                n.excludedNodesList.AddRange(lli);
            }
            else
            {
                n.excludedNodesList.AddRange(llx);
            }

            if (rri.Count > rrx.Count)
            {
                n.excludedNodesList.AddRange(rri);
            }
            else
            {
                n.excludedNodesList.AddRange(rrx);
            }
        }
    }
}