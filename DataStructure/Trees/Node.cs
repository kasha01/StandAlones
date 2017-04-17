using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure.Trees
{
    public class Node
    {
        public Node left;
        public Node right;
        public Node parent;
        public int data;
        public int height;
        public int leaves;  // count of leaves the node has

        public Node(int d)
        {
            this.data = d;
            this.left = null;
            this.right = null;
        }

        public Node(int d, Node p, int l)
        {
            this.parent = p;
            this.data = d;
            this.left = null;
            this.right = null;
            this.height = l;
        }
    }
}
