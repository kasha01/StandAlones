namespace Algorithms.Sorting
{
    class Sorting
    {

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
            int temp2 = ar[i];
            ar[i] = pivot;
            pivot = temp2;
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

    }
}
