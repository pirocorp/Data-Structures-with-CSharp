using System;

public static class Heap<T> where T : IComparable<T>
{
    public static void Sort(T[] arr)
    {
        var n = arr.Length;

        for (var i = n / 2; i >= 0; i--)
        {
            Down(arr, i, n);
        }

        for (var i = n - 1; i > 0; i--)
        {
            var temp = arr[i];
            arr[i] = arr[0];
            arr[0] = temp;
            Down(arr, 0, i);
        }
    }

    public static void Sort2(T[] arr)
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

            //child < element
            if (child.CompareTo(element) < 0)
            {
                break;
            }

            arr[index] = child;
            arr[childIndex] = element;

            index = childIndex;
        }
    }

    private static void Down(T[] arr, int current, int border)
    {
        while (current < border / 2)
        {
            var element = arr[current];
            var childIndex = 2 * current + 1;

            if (childIndex + 1 < border &&
                arr[childIndex].CompareTo(arr[childIndex + 1]) < 0)
            {
                childIndex++;
            }

            var child = arr[childIndex];

            //child < element
            if (child.CompareTo(element) < 0)
            {
                break;
            }

            arr[current] = child;
            arr[childIndex] = element;

            current = childIndex;
        }
    }
}
