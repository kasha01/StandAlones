using System;

namespace DataStructure
{
	public class Node<T>
	{
		// I needed class node because now the heap "nodes" has both data and value and needs to overrie operators
		// in regular heap, all what i have is data, so array of integer is very sufficient
		public T data; public int priority;
		public Node(T d, int p)
		{
			this.data = d; this.priority = p;
		}
		public static bool operator <(Node<T> n1, Node<T> n2) { return n1.priority < n2.priority; }
		public static bool operator >(Node<T> n1, Node<T> n2) { return n1.priority > n2.priority; }
		public static bool operator <=(Node<T> n1, Node<T> n2) { return n1.priority <= n2.priority; }
		public static bool operator >=(Node<T> n1, Node<T> n2) { return n1.priority >= n2.priority; }
	}
}

