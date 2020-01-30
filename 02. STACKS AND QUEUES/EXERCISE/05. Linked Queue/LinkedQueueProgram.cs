namespace _05.Linked_Queue
{
    using System;

    public static class LinkedQueueProgram
    {
        public static void Main()
        {
            var queue = new LinkedQueue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Enqueue(5);

            Console.WriteLine(string.Join(", ", queue.ToArray()));

            var r = queue.Dequeue();
            Console.WriteLine(r);
            Console.WriteLine(string.Join(", ", queue.ToArray()));


            r = queue.Dequeue();
            Console.WriteLine(r);
            Console.WriteLine(string.Join(", ", queue.ToArray()));

        }
    }
}