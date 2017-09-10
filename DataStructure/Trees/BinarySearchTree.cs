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

        // convert BST to ascending sorted array
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

        public void getCountofLeaves()
        {
            traverse_in_postorder(this.root);
        }
        
        public int traverse_in_postorder(Node n)
        {
            if (n == null) { return 0; }

            int l = traverse_in_postorder(n.left);
            int r = traverse_in_postorder(n.right);
            if (l == 0 && r == 0)
            {
                n.leaves = 1;
                return 1;
            }
            else
            {
                n.leaves = l + r;
                return l + r;
            }
            //Console.Write(n.data + " ");
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

        public void printTreeByLevels(Node root)
        {
            // <level, list of nodes in level>
            Dictionary<int, List<Node>> dic = new Dictionary<int, List<Node>>();
            
            //Fill dictionary with nodes by level
            postorder_2(root, 0, dic);

            //Construct Node.nextRight
            foreach(var list in dic.Values)
            {
                for(int i=0; i<list.Count-1; i++)
                {
                    list[i].nextRight = list[i + 1];
                }
            }

            // print by next Right
            foreach(int l in dic.Keys)
            {
                Node firstNode = dic[l][0];
                while(firstNode != null)
                {
                    Console.Write(firstNode.data + " ");
                    firstNode = firstNode.nextRight;
                }
                Console.WriteLine();
            }
        }

        void postorder_2(Node n, int l, Dictionary<int, List<Node>> mp)
        {
            if (n == null) { return; }
            int level = l + 1;
            postorder_2(n.left, level, mp);
            postorder_2(n.right, level, mp);

            if (!mp.ContainsKey(level))
            {
                // not found
                List<Node> list = new List<Node>(); list.Add(n);
                mp.Add(level,list);
            }
            else
            {
                mp[level].Add(n);
            }
        }

    }
}
