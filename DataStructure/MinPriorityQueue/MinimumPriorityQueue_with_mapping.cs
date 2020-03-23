using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure.MinPriorityQueue
{
	public class MinimumPriorityQueue_with_mapping<T>
	{
		private Node<T>[] arr;
		private Dictionary<T, int> map;	// maps items to its index in the heap

		public int heapSize { get; set; }

		public MinimumPriorityQueue_with_mapping(int size)
		{
			arr = new Node<T>[size];
			map = new Dictionary<T, int> ();
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
			map.Add (d, heapSize);

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
			int itemIndex = map [value];
			if (arr[itemIndex].priority > newP)
			{
				arr[itemIndex].priority = newP;
				siftUp(itemIndex);    //priority "decreased", push node up the heap
			}
			else
			{
				throw new Exception("New Priority is larget than current");
			}
		}

		public T extract_min()
		{
			if (heapSize == 0) { throw new Exception("Priority Queue is empty"); }

			// update map
			T minData = arr [0].data;
			T lastItemInHeap = arr [0].data;
			map.Remove (minData);		// to be extracted
			map[lastItemInHeap] = 0;	// last item in heap is brought into index 0

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

		public void emptifyQueue() { map.Clear(); heapSize = 0; /*simply set size to 0*/ }

		private void swap(int p, int index)
		{
			Node<T> parentNode = arr[p];
			Node<T> indexNode = arr [index];

			Node<T> temp = arr[p];
			arr[p] = arr[index];
			arr[index] = temp;

			map[parentNode.data] = index;
			map[indexNode.data] = p;
		}
	}
}