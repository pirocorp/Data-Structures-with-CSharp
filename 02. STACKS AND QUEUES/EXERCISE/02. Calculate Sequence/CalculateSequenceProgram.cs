namespace _02._Calculate_Sequence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class CalculateSequenceProgram
    {
        public static void Main()
        {
            var queue = new Queue<int>();

            var input = int.Parse(Console.ReadLine());
            var items = new List<int>();

            queue.Enqueue(input);
            items.Add(input);

            AddElementsToQueue(queue, items);
            items = items.Take(50).ToList();

            Console.WriteLine(string.Join(", ", items));
        }

        private static void AddElementsToQueue(Queue<int> queue, List<int> items)
        {
            var current = queue.Dequeue();

            queue.Enqueue(current + 1);
            items.Add(current + 1);
            queue.Enqueue(2 * current + 1);
            items.Add(2 * current + 1);
            queue.Enqueue(current + 2);
            items.Add(current + 2);


            if (items.Count < 50)
            {
                AddElementsToQueue(queue, items);
            }
        }
    }
}