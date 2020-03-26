﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
	public class GraphWithAdjacentList{

		// maps node to index/name of node
		public Dictionary<int, Node> nodes = new Dictionary<int, Node>();

		// Add Node to Graph
		public void addNode(int index, Node n)
		{
			nodes.Add(index,n);
		}

		public void ConnectNode(int a, int b){
			Node na = nodes [a];
			Node nb = nodes [b];

			na.adjNodes.Add (b);
			nb.adjNodes.Add (a);
		}

		public void ResetVisited(){
			foreach (KeyValuePair<int,Node> n in nodes) {
				n.Value.visited = false;
			}
		}

		public int DFS(Node root, Node disconnected)
		{
			int count = 0;	// count of nodes traversed
			Stack<Node> s = new Stack<Node>();
			root.visited = true;
			s.Push(root);

			while (s.Count > 0)
			{
				Node child = getAdjacentNode(s.Peek());
				if (child != null)
				{
					child.visited = true;
					s.Push(child);
				}
				else
				{
					count++;
					s.Pop();
				}
			}

			return count;
		}

		private Node getAdjacentNode(Node n)
		{
			foreach(int rf in n.adjNodes) {
				Node adj = nodes[rf];
				if (!adj.visited) {
					return adj;
				}
			}
			return null;
		}
	}

	public class Node{
		public int index;
		public ISet<int> adjNodes;
		public bool visited;

		public Node(int i){
			this.index = i;
			this.visited = false;
			adjNodes = new HashSet<int>();
		}
	}
}