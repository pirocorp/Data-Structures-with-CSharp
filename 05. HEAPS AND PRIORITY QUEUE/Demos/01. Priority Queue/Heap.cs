namespace _01._Priority_Queue
{
    using System;

    public static class Heap<T> where T : IComparable<T>
    {
        public static void Sort(T[] arr)
        {
            for (var i = arr.Length / 2; i >= 0; i--)
            {
                HeapifyDown(arr, i, arr.Length);
            }

            for (var i = arr.Length - 1; i > 0; i--)
            {
                var temp = arr[i];
                arr[i] = arr[0];
                arr[0] = temp;

                HeapifyDown(arr, 0, i);
            }
        }

        private static void HeapifyDown(T[] arr, int index, int length)
        {
            while (index < length / 2)
            {
                var element = arr[index];
                var childIndex = 2 * index + 1;

                //this.heap[childIndex] < this.heap[childIndex + 1]
                if (childIndex + 1 < length &&
                    arr[childIndex].CompareTo(arr[childIndex + 1]) < 0)
                {
                    childIndex++;
                }

                var child = arr[childIndex];

                //child > element
                if (child.CompareTo(element) < 0)
                {
                    break;
                }

                arr[index] = child;
                arr[childIndex] = element;

                index = childIndex;
            }
        }
    }
}