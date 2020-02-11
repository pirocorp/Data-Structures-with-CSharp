using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class DecreaseKeyProgram
{
    public static void Main()
    {
        MinHeapExample();
    }

    private static void MinHeapExample()
    {
        Console.WriteLine("Created an empty heap.");
        var heap = new PriorityQueue<int>();
        heap.Insert(5);
        heap.Insert(8);
        heap.Insert(1);
        heap.Insert(3);
        heap.Insert(12);
        heap.Insert(-4);

        Console.WriteLine("Heap elements (min to max):");
        while (heap.Count > 0)
        {
            var max = heap.Pull();
            Console.WriteLine(max);
        }
    }
}