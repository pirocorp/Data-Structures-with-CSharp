namespace _03._CircularQueue
{
    using System;

    public static class CircularQueueProgram
    {
        public static void Main()
        {
            var queue = new CircularQueue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Enqueue(5);

            queue.Dequeue();
            queue.Dequeue();

            Console.WriteLine(string.Join(", ", queue.ToArray()));
            foreach (var i in queue)
            {
                Console.WriteLine(i);
            }
        }
    }
}