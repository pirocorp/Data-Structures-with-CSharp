namespace _01._RedBlackTree
{
    using System;

    public static class RedBlackTreeProgram
    {
        public static void Main()
        {
            var tree = new RedBlackTree<int, int>();

            var input = Console.ReadLine();

            var isNumber = int.TryParse(input, out var result);

            while (isNumber)
            {
                tree.Add(result, result);
                tree.Print("Final tree");
                input = Console.ReadLine();
                isNumber = int.TryParse(input, out result);
            }

            if (input?.ToLower() == "auto")
            {
                RandomTreeGenerator(tree);
            }
        }

        private static void RandomTreeGenerator(RedBlackTree<int, int> tree)
        {
            var rnd = new Random();

            var input = "";

            while (input.ToLower() != "quit")
            {
                Console.Clear();

                var current = rnd.Next(0, 100);

                Console.WriteLine($"Adding {current}");
                tree.Add(current, current);
                tree.Print("Final tree");

                Console.WriteLine("Press Enter to continue...");
                input = Console.ReadLine();
            }
        }
    }
}