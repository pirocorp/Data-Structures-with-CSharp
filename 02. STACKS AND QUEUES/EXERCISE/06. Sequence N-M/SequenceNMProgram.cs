namespace _06._Sequence_N_M
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class SequenceNmProgram
    {
        public static void Main()
        {
            var inputNumbers = Console.ReadLine()
                .Split(' ')
                .Where(x => x != "")
                .Select(int.Parse)
                .ToArray();

            var n = inputNumbers[0];
            var m = inputNumbers[1];

            var queue = new Queue<Item<int>>();
            var nItem = new Item<int>(n, null);
            queue.Enqueue(nItem);

            while (queue.Count != 0)
            {
                var item = queue.Dequeue();

                if (item.Value < m)
                {
                    queue.Enqueue(new Item<int>(item.Value + 1, item));
                    queue.Enqueue(new Item<int>(item.Value + 2, item));
                    queue.Enqueue(new Item<int>(item.Value * 2, item));
                }

                if (item.Value == m)
                {
                    PrintSolution(item);
                    return;
                }
            }
        }

        private static void PrintSolution(Item<int> item)
        {
            var result = new List<int>();

            while (item != null)
            {
                result.Add(item.Value);
                item = item.PrevItem;
            }

            result.Reverse();

            Console.WriteLine(string.Join(" -> ", result));
        }
    }
}