using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure.Trees
{
    class BinarySearchTree
    {
        public Node root = null;

        // Insert Node into BST - my way
        public void insert(int d)
        {
            if (root != null)
            {
                insert_rc(ref root, d);
            }
            else
            {
                root = new Node(d);
            }
        }

        private void insert_rc(ref Node node, int d)
        {
            if (node != null && d >= node.data)
            {
                insert_rc(ref node.right, d);
            }
            else if (node != null && d < node.data)
            {
                insert_rc(ref node.left, d);
            }
            else
            {
                node = new Node(d);
            }
        }

        //Add a node without a recurssion
        public Node addNode(int d)
        {
            Node n = this.root;

            if (n == null) { this.root = new Node(d, null, 1); return n; }
            int l = 1;
            while (true)
            {
                l++;
                if (d > n.data && n.right == null)
                {
                    n.right = new Node(d, n, l);
                    break;
                }
                else if (d > n.data)
                {
                    n = n.right;
                }
                else if (d < n.data && n.left == null)
                {
                    n.left = new Node(d, n, l);
                    break;
                }
                else if (d < n.data)
                {
                    n = n.left;
                }
            }
            return n;
        }

        public void inorder(Node n)
        {
            if (n == null) { return; }

            inorder(n.left);
            Console.Write(n.data + " ");
            inorder(n.right);
        }

        public void preorder(Node n)
        {
            if (n == null) { return; }
            Console.Write(n.data + " ");
            preorder(n.left);
            preorder(n.right);
        }

        public void postorder(Node n)
        {
            if (n == null) { return; }

            postorder(n.left);
            postorder(n.right);

            Console.Write(n.data + " ");
        }

        public int getHeight(Node n)
        {
            int h = 0;
            if (n == null) { return 0; }
            if (n.left != null || n.right != null) { h++; }
            int lh = getHeight(n.left);
            int rh = getHeight(n.right);
            return Math.Max(lh, rh) + h;
        }
        
        public int getDepth(Node n)
        {
            if (n == null || n.parent == null || n.parent == n)
            {
                return 0;
            }
            return getDepth(n.parent) + 1;
        }
    }
}
