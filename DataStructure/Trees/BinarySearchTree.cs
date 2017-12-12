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

		// convert BST to a sorted array in ascending order.
		public void inorder(Node n)
		{
			if (n == null) { return; }

			inorder(n.left);
			Console.Write(n.data + " ");
			inorder(n.right);
		}

		public void inorder_iterative(Node root)
		{
			Node current = root;
			Stack<Node> st = new Stack<Node>();
			st.Push(root);
			current = root.left;

			while (st.Count != 0 || current != null)
			{
				while (current != null)
				{
					st.Push(current);
					current = current.left;
				}
				var n = st.Pop();
				Console.Write(n.data + " ");
				current = n.right;
			}
		}

		public Node ll_head = null; // linked list head
		private Node prev = null;   // the previous node traversed in the inorder traversal.
									// which essentially is the left node (if I am the root), or the root if I'm at the right node
		public void bst_to_ll(Node n)
		{
			if (n == null) { return; }

			bst_to_ll(n.left);

			if (ll_head == null)
			{
				ll_head = n;
			}
			else
			{
				n.left = prev;
				prev.right = n;
			}
			prev = n;

			bst_to_ll(n.right);
		}

		// copy tree, prefix expression/polish notation
		/* you can copy tree by traversing in preorder and duplicate every node and move
		 for example, head-create head, go left-go left on the dup, node-dup node..etc. */
		public void preorder(Node n)
		{
			if (n == null) { return; }
			Console.Write(n.data + " ");
			preorder(n.left);
			preorder(n.right);
		}

		// Delete Tree. Postfix expression
		/* you can delete the tree by following postorder and delete every node you hit. this way the tree will be deleted
		 without leaving orphan nodes*/
		public void postorder(Node n)
		{
			if (n == null) { return; }

			postorder(n.left);
			postorder(n.right);

			Console.Write(n.data + " ");
		}

		// find least common ancestor
		public bool LCA(Node n, int nv1, int nv2, ref Node common_ancestor)
		{
			if (common_ancestor != null) { return false; }
			if (n == null) { return false; }

			bool n1Found = LCA(n.left, nv1, nv2, ref common_ancestor);
			bool n2Found = LCA(n.right, nv1, nv2, ref common_ancestor);


			if ((n1Found && n2Found)
				|| ((n1Found || n2Found) && (n.data == nv1 || n.data == nv2))
				|| ((nv1 == n.data) && (nv2 == n.data))
				)
			{
				common_ancestor = n;
				return false;
			}
			Console.Write(n.data + " ");
			return n1Found || n2Found || n.data == nv1 || n.data == nv2;
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

		// Level = Depth = number of edges from root to node. root is level 0, 1st child is level 1 and so on
		// height is index 1 number and it is number of levels. H = level of deepest node + 1.
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
			foreach (var list in dic.Values)
			{
				for (int i = 0; i < list.Count - 1; i++)
				{
					list[i].nextRight = list[i + 1];
				}
			}

			// print by next Right
			foreach (int l in dic.Keys)
			{
				Node firstNode = dic[l][0];
				while (firstNode != null)
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
				mp.Add(level, list);
			}
			else
			{
				mp[level].Add(n);
			}
		}

		#region Morris Traversal
		// http://www.geeksforgeeks.org/?p=6358

		void MorrisTraversal_Inorder(Node root)
		{
			Node current, pre;

			if (root == null)
				return;

			current = root;
			while (current != null)
			{
				if (current.left == null)
				{
					// if current has no left child, Print node and go right
					// right will either take me to the right subtree of current or up via threaded link in the next while tick
					Console.Write(current.data + " "); // print node before I go right(which is the case for both inorder 
													   // and preorder)
					current = current.right;
				}
				else
				{
					/* Find the inorder predecessor of current, so current becomes the right child of it */
					// the inorder predecessor of current is the right most node of the current's left sub tree
					// Go left(left subtree) - keep loop to right until I reach the right most (pre.right == null)
					// Condition pre.right != current => pre.right will equal current when I have finished my left subtree
					pre = current.left;
					while (pre.right != null && pre.right != current)
						pre = pre.right;

					// Hit when pre.right = null -> I have reached right most node.
					// Make current as right child of its inorder predecessor
					if (pre.right == null)
					{
						pre.right = current;    // right most child (pred), right child is current
						current = current.left; // go left
					}

					// Hit when pre.right != null i.e. pre.right=current => I have finished left subtree of current
					// Revert the changes made in if part to restore the original tree i.e., fix the right child of predecssor
					else
					{
						pre.right = null;       // remove the virtual link I added

						Console.Write(current.data + " "); // print node which in this case root of left subtree since this
														   // else-if hits when left subtree finishes. In case cur=4, pre=5, will print 4 then jumpt to 2.
														   // inorder => print root after finish left subtree and before going to right subtree.

						current = current.right; //go right in case of cur=4 I will go up to 2, in case cur=1, I will go right to 8
					}
				}
			}
		}

		void MorrisTraversal_Preorder(Node root)
		{
			Node current, pre;

			if (root == null)
				return;

			current = root;
			while (current != null)
			{
				if (current.left == null)
				{
					// if current has no left child, Print node and go right
					// right will either take me to the right subtree of current or up via threaded link in the next while tick
					Console.Write(current.data + " "); // print current node before I go right
					current = current.right;    // go right
				}
				else
				{
					pre = current.left;
					while (pre.right != null && pre.right != current)
						pre = pre.right;

					// Hit when pre.right = null -> I have reached right most node.
					if (pre.right == null)
					{
						pre.right = current;    // right most child (pred), right child is current
						Console.Write(current.data + " "); // print current node before I go left
						current = current.left; // go left
					}

					// Hit when pre.right != null i.e. pre.right=current => I have finished left subtree of current
					// Revert the changes made in if part to restore the original tree i.e., fix the right child of predecssor
					else
					{
						pre.right = null;       // remove the virtual link I added						
						current = current.right; //go right in case of cur=4 I will go up to 2, in case cur=1, I will go right to 8
					}
				}
			}
		}

		// TODO UNDERSTAND CODE CONCEPT. THANKS TO Kartik Saraswat
		//This is Post Order :children before node( L ,R , N)
		public void morrisPostorderTraversal(Node root)
		{

			// Making our tree left subtree of a dummy Node
			Node dummyRoot = new Node(0);
			dummyRoot.left = root;

			//Think of P as the current node 
			Node p = dummyRoot, pred, first, middle, last;
			while (p != null)
			{

				if (p.left == null)
				{
					p = p.right;
				}
				else
				{
					/* p has a left child => it also has a predeccessor
					   make p as right child predeccessor of p	
					*/
					pred = p.left;
					while (pred.right != null && pred.right != p)
					{
						pred = pred.right;
					}

					if (pred.right == null)
					{

						// predeccessor found for first time
						// modify the tree

						pred.right = p;
						p = p.left;

					}
					else
					{

						// predeccessor found second time
						// reverse the right references in chain from pred to p
						first = p;
						middle = p.left;
						while (middle != p)
						{
							last = middle.right;
							middle.right = first;
							first = middle;
							middle = last;
						}

						// visit the nodes from pred to p
						// again reverse the right references from pred to p	
						first = p;
						middle = pred;
						while (middle != p)
						{

							Console.Write(middle.data + " ");
							last = middle.right;
							middle.right = first;
							first = middle;
							middle = last;
						}

						// remove the pred to node reference to restore the tree structure
						pred.right = null;
						p = p.right;
					}
				}
			}
		}

		#endregion

		#region GeeksForGeeks Imp.
		// http://www.geeksforgeeks.org/level-order-tree-traversal/
		public int getTreeHeight(Node n)
		{
			if (n == null) { return 0; }

			int hl = getTreeHeight(n.left);
			int hr = getTreeHeight(n.right);

			return Math.Max(hl, hr) + 1;
		}

		// Prints all levels
		public void traverseLevels(Node n, int l)
		{
			if (n == null) { return; }

			// I am at the target level
			if (l == 1) { Console.Write(n.data + " "); }
			else if (l > 1)
			{
				traverseLevels(n.left, l - 1);
				traverseLevels(n.right, l - 1);
			}
		}

		public void printAllLevel(Node root)
		{
			int h = getTreeHeight(root);
			for (int i = 1; i <= h; i++)
			{
				traverseLevels(root, i);
				Console.WriteLine();
			}
		}
		#endregion
	}
}
