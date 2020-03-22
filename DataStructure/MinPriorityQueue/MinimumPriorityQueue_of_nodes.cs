using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure.MinPriorityQueue
{
	class Node<T>
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

	public class MinPriorityQueue_of_nodes<T>
	{
		//Priority Zero is the highest Priority...Priority 100 is the lowest
		//this is best fit for Dijksra, as lowest/shortest distance path will have top priority and will be at the top of the heap
		public int heapSize { get; set; }
		Node<T>[] arr;

		public MinPriorityQueue_of_nodes(int size)
		{
			arr = new Node<T>[size];
		}

		private int getParent(int i) { return (i - 1) / 2; }
		private int getLeft(int i) { return (i * 2) + 1; }
		private int getRight(int i) { return (i * 2) + 2; }

		public void add_with_priority(T d, int p)
		{
			if (heapSize == arr.Length) { throw new Exception("Reached Max Size"); }

			// add new node to the tail of the heap and sift up
			Node<T> n = new Node<T>(d, p);
			arr[heapSize] = n;
			heapSize++;
			siftUp(heapSize - 1);
		}

		private void siftUp(int index)
		{
			if (index == 0) { return; }

			int p = getParent(index);

			if (p < heapSize && arr[p] > arr[index])
			{
				swap(p, index);
			}
			siftUp(p);
		}

		public void decrease_priority(T value, int newP)
		{
			//this is actually increasing priority, since we have MinPriorityQueue, items with priority (1) has "higher" priority than item with priority (2)
			//find value
			for (int i = 0; i < heapSize; i++)
			{
				if (arr[i].data.Equals(value))
				{
					// try to decrease priority
					if (arr[i].priority > newP)
					{
						arr[i].priority = newP;
						siftUp(i);    //priority "decreased", push node up the heap
					}
					else
					{
						throw new Exception("New Priority is larget than current");
					}
				}
			}
		}

		public T extract_min()
		{
			if (heapSize == 0) { throw new Exception("Priority Queue is empty"); }

			//get the top
			Node<T> n = arr[0];            //extract the minimum node (top of heap)
			arr[0] = arr[heapSize - 1]; //put the top at the tail of the p-queue (prepare for deleting it)
			arr[heapSize - 1] = null;   //set the tail of the p-queue (which is the extracted node as null)
			heapSize--;                 //delete the tail by chopping the heap size
			if (heapSize > 0) //if heap size is zero or less, there is nothing to sift down into
			{
				siftDown(0);
			}

			return n.data;
		}

		private void siftDown(int index)
		{
			int l = getLeft(index);
			int r = getRight(index);
			int smallest = index;

			if (l < heapSize && arr[l] < arr[smallest])
			{
				smallest = l;
			}
			if (r < heapSize && arr[r] < arr[smallest])
			{
				smallest = r;
			}
			if (index != smallest)
			{
				swap(index, smallest);
				siftDown(smallest);
			}
		}

		public void emptifyQueue() { heapSize = 0; /*simply set size to 0*/ }

		private void swap(int p, int index)
		{
			Node<T> temp = arr[p];
			arr[p] = arr[index];
			arr[index] = temp;
		}
	}
}