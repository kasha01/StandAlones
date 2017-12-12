using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Comparison of Heap sort:
 * Heap sort is in-place (O(1) auxilary space). Not a stable sort (equal elements initial sort is not preserved due to heapifying)
 * Worst-case (upper bound): O(n log n) 
 * Best-case: O(n)
 * space: O(1) auxilary
 * 
 * Heap sort compete with Quick sort: Heap sort has better upper bound of O(n log n), quick sort has worst case of O(n^2) which
 * makes it not suitable for big data.
 * Heap sort is good for embedded system or system with small memory
 * 
 * Heap sort competes with Merge sort: Merge sort space omega(n). but merge sort has some advantages over Heap:
 * it is suitable for parallel algorithm (running different parts of the algorithm on different machines then combine the result),
 * it supports external sorting (different parts of data can be on memory and other on the hardisk), for heap sort, locality of 
 * reference is a limitation 
 */

namespace DataStructure.Heap
{
    // Heap + Heap Sort
    public class Heap
    {
        int capacity;
        int[] arr;
        int size;

        public Heap(int[] arr, bool isMaxHeap)
        {
            this.capacity = arr.Length;
            this.size = this.capacity;
            this.arr = arr;
            if (isMaxHeap)
            {
                buildMaxHeap();
            }
            else
            {
                buildMinHeap();
            }
        }

        void swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        int parent(int i)
        {
            return (i - 1) / 2;
        }

        int left(int i)
        {
            return 2 * i + 1;
        }

        int right(int i)
        {
            return 2 * i + 2;
        }

        void maxHeapify(int i)
        {
            int l = left(i);
            int r = right(i);
            int largest = i;

            if (l < size && arr[l] > arr[largest])
                largest = l;
            if (r < size && arr[r] > arr[largest])
                largest = r;

            if (largest != i)
            {
                swap(arr, i, largest);
                maxHeapify(largest);    //this is a max heap, so small parent is replaced with large child, largest index is pointing at the large child, so we are going down, therefore sift down
            }
        }

        void minHeapify(int i)
        {
            int l = left(i);
            int r = right(i);
            int smallest = i;

            if (l < size && arr[l] < arr[smallest])
                smallest = l;
            if (r < size && arr[r] < arr[smallest])
                smallest = r;

            if (smallest != i)
            {
                swap(arr, i, smallest);
                minHeapify(smallest);    //this is a min heap, so large parent is replaced with the small child, smallest index is pointing at the small child, so we are going down, therefore sift down
                                         // the easy way to know if sifting up or down, is by noticing index i passed in the heapify method, if it is increasing on every recursion (moving down the tree) then it is sift down, if decreasing (moving up the tree) then it is sift up
            }
        }

		//Heapifying as it swifts down, it can go to one side of the tree but has to ignore the other, therefore, we do n/2 and
		// reheapify so all nodes are eventually in right place
        void buildMaxHeap()
        {
            int n = arr.Length;
            for (int i = ((int)Math.Floor(n / 2.0)) - 1; i >= 0; i--)   //I only need to Heapify the parents
                maxHeapify(i);
        }

        void buildMinHeap()
        {
            int n = arr.Length;
            for (int i = ((int)Math.Floor(n / 2.0)) - 1; i >= 0; i--)
                minHeapify(i);
        }

        public void heapsort(bool isMaxHeap)
        {
            for (int i = size - 1; i >= 0; i--)
            {
                //swap - heap allows O(1) access to max(max heap)/min(min heap). so we always swap to put the Max at the end. then we heapfiy to rebalance the heap and DECREASE THE SIZE OF THE HEAP, so we don't alter the max value and continue
                // Max Heap --> Ascending Sort (Max swapped to the end) . Min Heap --> Descending Sort
                int temp = arr[i];
                arr[i] = arr[0];
                arr[0] = temp;
                size = size - 1;
                if (isMaxHeap)
                {
                    maxHeapify(0);
                }
                else
                {
                    minHeapify(0);
                }
                printHeap();
            }
        }

        public void printHeap()
        {
            foreach (var i in arr)
                Console.Write(i + " ");

            Console.WriteLine();
        }

    }
}
