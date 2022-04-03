namespace Algorithms.Sorting
{
	class Sorting
	{
		// average O(log n)
		// worst O(log n)
		public static int findByBinarySearch(int i, int j, int[] arr, int n)
		{
			// -1 is not found
			int mid = (i + j) / 2;
			int armid = arr[mid];
			if (i > j) { return -1; }

			if (armid == n) { return mid; }
			else if (n > armid) { return findByBinarySearch(mid + 1, j, arr, n); }
			else if (n < armid) { return findByBinarySearch(i, mid - 1, arr, n); }
			else { return -1; }
		}

		/*
		 * average/worst/best O(n log n)
		 * space: O(n)
		 * This first approach has a bad space complexity. I think it is O(n logn)
		 * the second approach "better" has space O(n)
		 */
		public static int MergeSort(int[] ar)
		{
			if (ar.Length < 2)
			{
				return 0;
			}

			int mid = ar.Length / 2;
			int[] left = new int[mid];
			int[] right = new int[ar.Length - mid];
			for (int ii = 0; ii < mid; ii++)
			{
				left[ii] = ar[ii];
			}
			for (int jj = mid; jj < ar.Length; jj++)
			{
				right[jj - mid] = ar[jj];
			}
			int counter = 0;
			counter = counter + MergeSort(left);
			counter = counter + MergeSort(right);

			int k = 0;
			int i = 0; int j = 0;
			while (i < left.Length && j < right.Length)
			{
				if (left[i] <= right[j])
				{
					ar[k] = left[i]; i++;
				}
				else
				{
					counter++;
					ar[k] = right[j]; j++;
				}
				k++;
			}

			while (i < left.Length)
			{
				ar[k] = left[i]; i++; k++;
			}
			while (j < right.Length)
			{
				ar[k] = right[j]; j++; k++;
			}
			return counter;
		}
		#region merge sort better apporach
		public static void mergesort2(int[] ar, int l, int r)
		{
			// the split into two arrays is impossible if l>=r
			if (l >= r)
				return;

			// same as (l+r)/2 but this avids overflow of large l,r
			int m = l + ((r - l) / 2);
			mergesort2(ar, l, m);
			mergesort2(ar, m + 1, r);

			merge(ar, l, r, m);
		}

		public static void merge(int[] ar, int l, int r, int mid)
		{
			// by splitting the temp arrays here out of the recurssive function. I decrease space complexity
			// space complexity is O(n)
			int ll = mid - l + 1;
			int rr = r - mid;
			int[] left = new int[ll];
			int[] right = new int[rr];

			for (int ii = 0; ii < ll; ii++)
			{
				left[ii] = ar[l + ii];
			}
			for (int jj = 0; jj < rr; jj++)
			{
				right[jj] = ar[mid + 1 + jj];
			}

			int k = l;
			int i = 0; int j = 0;
			while (i < left.Length && j < right.Length)
			{
				if (left[i] <= right[j])
				{
					ar[k] = left[i]; i++;
				}
				else
				{
					ar[k] = right[j]; j++;
				}
				k++;
			}

			while (i < left.Length)
			{
				ar[k] = left[i]; i++; k++;
			}
			while (j < right.Length)
			{
				ar[k] = right[j]; j++; k++;
			}
		}

		#endregion

		// Lomoto quick sort - can be optimized for finding the kth largest/smallest element
		public static int quicksort(int lo, int hi, int[] ar)
		{
			int pivot = ar[hi];
			int i = lo;
			if (lo < hi)
			{
				for (int j = lo; j < hi; j++)
				{
					if (ar[j] < pivot)
					{
						int temp = ar[j];
						ar[j] = ar[i];
						ar[i] = temp;
						i++;
					}
				}
			}

			ar[hi] = ar[i];
			ar[i] = pivot;
			return i;
		}

		public static void partition(int s, int e, int[] ar)
		{
			if (s < e)
			{
				int index = quicksort(s, e, ar);
				partition(index + 1, e, ar);
				partition(s, index - 1, ar);
			}
		}


		// Hoare: notice pivot location is not necessarily at the index that is returned. i.e. pivot can be swapped
		//  watch this https://www.youtube.com/watch?v=NuQYFXmLUrM
		private static int parition(int lo, int hi, int[] arr){
			int pivot = arr [(lo + hi) / 2];
			int i = lo - 1;
			int j = hi + 1;

			while (true) {
				do {
					i++;
				} while(arr [i] < pivot);

				do {
					j--;
				} while(arr [j] > pivot);

				if (i >= j)
					return j;

				// swap i with j
				int temp = arr[i];
				arr [i] = arr [j];
				arr [j] = temp;
			}
		}

		private static void sortHoare(int lo, int hi, int[] arr){
			if (lo < hi) {
				int pivot = parition (lo, hi, arr);
				sortHoare (lo, pivot, arr);
				sortHoare (pivot + 1, hi, arr);
			}
		}
	}
}
