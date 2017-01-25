using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure.Trees
{
    class AVL_Tree
    {
        public Node root = null;

        // Insert a node into AVL
        public void insert_AVL(int d)
        {
            this.root = insertAVLNode(this.root, d);
        }

        // To insert a node in AVL, I must return the parent/root of the node inserted so I can balance it
        // We need to balance ALL THE ANCESTORS of the newly inserted node
        private Node insertAVLNode(Node node, int d)
        {
            if (node != null && d > node.data)
            {
                node.right = insertAVLNode(node.right, d); // node is the parent, node.right will have the newly created node returned to it
                node = BalanceTree(node);
            }
            else if (node != null && d < node.data)
            {
                node.left = insertAVLNode(node.left, d);
                node = BalanceTree(node);
            }
            else
            {
                node = new Node(d);
            }
            return node;
        }

        private int diff(Node n)
        {
            int l = getLevel(n.left);
            int r = getLevel(n.right);
            return l - r;
        }

        // check https://www.cpp.edu/~ftang/courses/CS241/notes/self%20balance%20bst.htm
        public Node BalanceTree(Node n)
        {
            if (n == null)
                return null;

            int dif = diff(n);

            Node node = n;

            if (dif > 1)
            {
                //Left - we check on the left child to determine the second rotation:
                if (diff(n.left) > 0)   //Left Side > Right Side
                {
                    //Left - Left
                    node = rotate_ll(n);
                }
                else
                {
                    //Left - Right
                    node = rotate_lr(n);
                }
            }
            else if (dif < -1)
            {
                //Right - we check on the right child to determine the second rotation:
                if (diff(n.right) < 0)  //Right side > Left Side
                {
                    // Right - Right
                    node = rotate_rr(n);
                }
                else
                {
                    // Right - Left
                    node = rotate_rl(n);
                }
            }
            return node;    // node is the new root returned by the rotation function -which is actually the pivot-
        }

        private Node rotate_ll(Node root)
        {
            // Right Rotation
            Node pivot = root.left;
            root.left = pivot.right;
            pivot.right = root;
            return pivot;   //pivot is now the new root to be returned, check https://www.cpp.edu/~ftang/courses/CS241/notes/self%20balance%20bst.htm
        }

        private Node rotate_rr(Node root)
        {
            //Left Rotation
            Node pivot = root.right;
            root.right = pivot.left;
            pivot.left = root;
            return pivot;
        }

        private Node rotate_lr(Node root)
        {
            //Left - Right Rotation
            root.left = rotate_rr(root.left);
            return rotate_ll(root);
        }

        private Node rotate_rl(Node root)
        {
            //Right - Left Rotation
            root.right = rotate_ll(root.right);
            return rotate_rr(root);
        }

        public bool NodeExist(int d)
        {
            if (root != null)
            {
                return NodeExist(root, d);
            }
            else { return false; }
        }

        private bool NodeExist(Node node, int d)
        {
            if (node != null && d > node.data)
            {
                // go right
                return NodeExist(node.right, d);
            }
            else if (node != null && d < node.data)
            {
                return NodeExist(node.left, d);
            }
            else if (node != null && node.data == d)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void delete(int d)
        {
            if (root != null)
            {

            }
        }

        public int getLevel(Node n)     // used in AVL, each node has a min level of 1
        {
            if (n == null) { return 0; }
            int lh = getLevel(n.left);
            int rh = getLevel(n.right);
            return Math.Max(lh, rh) + 1;
        }
    }
}
