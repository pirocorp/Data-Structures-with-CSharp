namespace Sweep_and_Prune
{
    using System;
    using System.Collections.Generic;

    public static class SweepAndPruneProgram
    {
        private static readonly List<GameObject> Items = new List<GameObject>();
        private static readonly Dictionary<string, GameObject> ById = new Dictionary<string, GameObject>();
        private static int Ticks = 0;

        private static bool kill = false;

        public static void Main()
        {
            var input = Console.ReadLine();

            while (input != "end")
            {
                var cmdArgs = input
                    .Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);

                switch (cmdArgs[0])
                {
                    case "add":
                        AddItem(cmdArgs, Items, ById);
                        break;
                    case "start":
                        while (cmdArgs[0] != "end")
                        {
                            if (cmdArgs[0] == "move")
                            {
                                var id = cmdArgs[1];
                                var x = int.Parse(cmdArgs[2]);
                                var y = int.Parse(cmdArgs[3]);

                                if (ById.ContainsKey(id))
                                {
                                    ById[id].X1 = x;
                                    ById[id].Y1 = y;
                                }

                                Sweep();
                            }

                            if (cmdArgs[0] == "tick")
                            {
                                Sweep();
                            }

                            Ticks++;

                            cmdArgs = Console.ReadLine()
                                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                        }

                        kill = true;
                        break;
                    default:
                        break;
                }

                if (kill)
                {
                    break;
                }

                input = Console.ReadLine();
            }
        }

        private static void Sweep()
        {
            InsertionSort();

            for (var i = 0; i < Items.Count; i++)
            {
                var current = Items[i];
                for (var j = i + 1; j < Items.Count; j++)
                {
                    var candidate = Items[j];
                    if (current.Intersect(candidate))
                    {
                        Console.WriteLine($"({Ticks}) {current.Id} collides with {candidate.Id}");
                    }
                }
            }
        }

        private static void InsertionSort()
        {
            for (var i = 1; i < Items.Count; i++)
            {
                var currentIndex = i;

                while (currentIndex - 1 >= 0 && Items[currentIndex - 1].X1 > Items[currentIndex].X1)
                {
                    Swap(currentIndex - 1, currentIndex);
                    currentIndex--;
                }
            }
        }

        private static void Swap(int indexA, int indexB)
        {
            var temp = Items[indexA];
            Items[indexA] = Items[indexB];
            Items[indexB] = temp;
        }

        private static void AddItem(string[] cmdArgs, List<GameObject> items, Dictionary<string, GameObject> byId)
        {
            var id = cmdArgs[1];
            var x = int.Parse(cmdArgs[2]);
            var y = int.Parse(cmdArgs[3]);

            var item = new GameObject(id, x, y);

            items.Add(item);
            byId.Add(item.Id, item);
        }
    }
}