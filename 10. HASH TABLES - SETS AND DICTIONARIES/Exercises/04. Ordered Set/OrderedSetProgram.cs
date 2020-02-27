namespace _04.Ordered_Set
{
    using System;

    public static class OrderedSetProgram
    {
        public static void Main()
        {
            var set = new OrderedSet<int> {17, 17};

            Console.WriteLine(set.Contains(17));
            Console.WriteLine(set.Contains(18));

            set.Add(9);
            Console.WriteLine(set.Contains(9));
            set.Remove(9);
            Console.WriteLine(set.Contains(9));
            
            set.Add(9);
            set.Add(12);
            set.Add(19);
            set.Add(6);
            set.Add(25);
            Console.WriteLine(set.Count);
            Console.WriteLine();

            foreach (var item in set)
            {
                Console.WriteLine(item);
            }
        }
    }
}