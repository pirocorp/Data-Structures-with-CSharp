namespace Sweep_and_Prune
{
    using System;
    using System.Collections.Generic;

    public static class SweepAndPruneProgram
    {
        public static void Main()
        {
            var items = new List<GameObject>();
            var byId = new Dictionary<string, GameObject>();

            var input = Console.ReadLine();

            while (input != "end")
            {
                var cmdArgs = input
                    .Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);

                switch (cmdArgs[0])
                {
                    case "add":
                        AddItem(cmdArgs, items, byId);
                        break;
                    case "start":
                        while (cmdArgs[0] != "end")
                        {
                            if (cmdArgs[0] == "move")
                            {
                                var id = cmdArgs[1];
                                var x = int.Parse(cmdArgs[2]);
                                var y = int.Parse(cmdArgs[3]);

                                if (byId.ContainsKey(id))
                                {
                                    byId[id].X1 = x;
                                    byId[id].Y1 = y;
                                }

                                Sweep();
                            }

                            if (cmdArgs[0] == "tick")
                            {
                                Sweep();
                            }

                            cmdArgs = Console.ReadLine()
                                .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                        }
                        break;
                    default:
                        break;
                }

                input = Console.ReadLine();
            }
        }

        private static void Sweep()
        {
            throw new NotImplementedException();
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