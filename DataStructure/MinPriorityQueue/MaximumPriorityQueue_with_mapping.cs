using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure.MinPriorityQueue
{
	// Using Binary Heap
	public class MaximumPriorityQueue_with_mapping<T>
	{
		private Node<T>[] arr;
		private Dictionary<T, int> map;	// maps items to its index in the heap

		public int heapSize { get; set; }

		public MaximumPriorityQueue_with_mapping(int size)
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

		public void increase_priority(T value, int newP)
		{
			int itemIndex = map [value];
			if (arr[itemIndex].priority < newP)
			{
				arr[itemIndex].priority = newP;
				siftUp(itemIndex);    //priority "increased", push node up the heap
			}
			else
			{
				throw new Exception("New Priority is smaller than current");
			}
		}

		public void update_priority(T value, int newP)
		{
			int itemIndex = map [value];
			if (arr[itemIndex].priority < newP)
			{
				arr[itemIndex].priority = newP;
				siftUp(itemIndex);    //priority "increased", push node up the heap
			}
			else if(arr[itemIndex].priority > newP)
			{
				// priority "decreased", push node down the heap
				arr[itemIndex].priority = newP;
				siftDown(itemIndex);
			}
		}

		public T extract_max()
		{
			if (heapSize == 0) { throw new Exception("Priority Queue is empty"); }

			// update map
			T maxData = arr [0].data;
			T lastItemInHeap = arr [0].data;
			map.Remove (maxData);		// to be extracted
			map[lastItemInHeap] = 0;	// last item in heap is brought into index 0

			//get the top
			Node<T> n = arr[0];
			arr[0] = arr[heapSize - 1];
			arr [heapSize - 1] = null;
			heapSize--;
			if (heapSize > 0)
			{
				siftDown(0);
			}

			return n.data;
		}

		public void delete(T value){
			increase_priority (value, int.MinValue); // increase item priority
			extract_max();							 // extract item
		}

		private void siftUp(int index)
		{
			if (index == 0) { return; }

			int p = getParent(index);

			if (p < heapSize && arr[p] < arr[index])
			{
				swap(p, index);
			}
			siftUp(p);
		}

		private void siftDown(int index)
		{
			int l = getLeft(index);
			int r = getRight(index);
			int largest = index;

			if (l < heapSize && arr[l] > arr[largest])
			{
				largest = l;
			}
			if (r < heapSize && arr[r] > arr[largest])
			{
				largest = r;
			}
			if (index != largest)
			{
				swap(index, largest);
				siftDown(largest);
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