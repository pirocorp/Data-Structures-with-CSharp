namespace _01._AVL_Tree
{
    using System;

    public static class AvlTreeProgram
    {
        public static void Main()
        {
            var tree = new AvlTree<int>();

            var input = Console.ReadLine();

            var isNumber = int.TryParse(input, out var result);

            while (isNumber)
            {
                tree.Add(result);
                tree.Print("Final tree");
                input = Console.ReadLine();
                isNumber = int.TryParse(input, out result);
            }

            if (input?.ToLower() == "auto")
            {
                RandomTreeGenerator(tree);
            }
        }

        private static void RandomTreeGenerator(AvlTree<int> tree)
        {
            var rnd = new Random();

            var input = "";

            while (input.ToLower() != "quit")
            {
                Console.Clear();

                var current = rnd.Next(0, 100);

                Console.WriteLine($"Adding {current}");
                tree.Add(current);
                tree.Print("Final tree");

                Console.WriteLine("Press Enter to continue...");
                input = Console.ReadLine();
            }
        }
    }
}