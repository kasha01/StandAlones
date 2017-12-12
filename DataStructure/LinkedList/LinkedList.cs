using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure.LinkedList
{
	public class LinkedList
	{
		#region Private Helper members
		static bool flag = false;
		#endregion

		public Node head;
		public Node OriginalHead;

		public LinkedList(List<int> values)
		{
			this.head = new Node(values[0]);
			this.OriginalHead = head;
			Node tempHead = this.head;
			for (int v = 1; v < values.Count; v++)
			{
				var n = new Node(values[v]);
				tempHead.next = n;
				tempHead = n;
			}
		}

		public void reArrangeLinkedList(Node h)
		{
			if (h == null)
				return;

			reArrangeLinkedList(h.next);
			if (flag) { return; }

			Node temp;
			if (h == head) { head.next = null; flag = true; return; }
			else if (head.next == h)
			{
				flag = true;
				temp = null;
			}
			else
			{
				temp = head.next;
			}
			head.next = h;
			h.next = temp;
			head = temp;
		}
	}



	public class Node : IDisposable
	{
		public int data;
		public Node next;

		public Node(int v)
		{
			this.data = v;
			this.next = null;
		}

		public Node(Node l)
		{
			this.data = l.data;
		}

		public void Dispose()
		{
			this.next = null;
		}
	}
}
