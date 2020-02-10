namespace _01._Priority_Queue
{
    using System;
    using System.Collections.Generic;

    public static class PriorityQueueProgram
    {
        public static void Main()
        {
            //Test1();
            //Test2();
            //Test3();
            //Test4();
            //Test5();
        }

        private static void Test5()
        {
            var arr = new int[] {12, 20, 30, 55, 5, 3, 6, 15, 8, 22, 17};

            var bh = new AnotherBinaryHeap<int>();
            for (var i = 0; i < arr.Length; i++)
            {
                bh.Add(arr[i]);
            }

            var result = new Stack<int>();

            while (bh.Count > 0)
            {
                result.Push(bh.Remove());
            }

            Console.WriteLine(string.Join(", ", result.ToArray()));

            Heap<int>.Sort(arr);
            Console.WriteLine(string.Join(", ", arr));
        }

        private static void Test4()
        {
            var bh = new AnotherBinaryHeap<int>();
            bh.Add(1);
            bh.Add(2);
            bh.Add(4);
            bh.Add(6);
            bh.Add(7);
            bh.Add(8);
            bh.Add(12);
            bh.Add(33);
            bh.Add(32);
            bh.Add(24);
            bh.Add(20);
            bh.Add(35);
            bh.Add(25);
            bh.Add(44);
            bh.Add(65);
            bh.Add(66);
            bh.Add(17);
            bh.Add(19);
            bh.Add(37);
            bh.Add(47);
            bh.Add(112);
            bh.Add(106);
            bh.Add(93);
            bh.Add(98);
            bh.Add(9);
            bh.Add(50);

            Console.WriteLine(bh.Peak());
            Console.WriteLine(bh.Remove());
            Console.WriteLine(bh.Remove());
            Console.WriteLine(bh.Remove());
            Console.WriteLine(bh.Remove());
            Console.WriteLine(bh.Peak());
        }

        private static void Test3()
        {
            var arr = new int[] {12, 20, 30, 55, 5, 3, 6, 15, 8, 22, 17};
            Heap<int>.Sort(arr);

            Console.WriteLine(string.Join(", ", arr));
        }

        private static void Test2()
        {
            var maxHeap = new BinaryHeap<int>();

            maxHeap.Insert(5);
            maxHeap.Insert(3);
            maxHeap.Insert(1);

            Console.WriteLine(maxHeap.Pull());
            Console.WriteLine(maxHeap.Pull());
            Console.WriteLine(maxHeap.Pull());
            Console.WriteLine(maxHeap.Count);
        }

        private static void Test1()
        {
            var maxHeap = new BinaryHeap<int>();

            maxHeap.Insert(3);
            Console.WriteLine(maxHeap.Peek());

            maxHeap.Insert(5);
            Console.WriteLine(maxHeap.Peek());

            maxHeap.Insert(2);
            Console.WriteLine(maxHeap.Peek());

            maxHeap.Insert(1);
            Console.WriteLine(maxHeap.Peek());

            maxHeap.Insert(7);
            Console.WriteLine(maxHeap.Peek());

            maxHeap.Insert(8);
            Console.WriteLine(maxHeap.Peek());

            maxHeap.Pull();
            Console.WriteLine(maxHeap.Peek());
            Console.WriteLine(new string('-', 50));
        }
    }
}