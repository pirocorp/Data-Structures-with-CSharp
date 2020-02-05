namespace _02._Round_Dance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class RoundDanceProgram
    {
        private static int start;

        private static Dictionary<int, List<int>> graph;

        private static Dictionary<int, bool> visited;

        public static void Main()
        {
            ReadInput();
            TestDFS();
        }

        private static void TestDFS()
        {
            visited = new Dictionary<int, bool>();

            foreach (var key in graph.Keys)
            {
                visited[key] = false;
            }

            DFS(start);
        }

        private static void DFS(int node)
        {
            var nodes = new Stack<int>();
            var maxCount = 0;

            var count = 0;

            nodes.Push(node);

            while (nodes.Count > 0)
            {
                var currentNode = nodes.Pop();

                if (visited[currentNode])
                {
                    continue;
                }
                else
                {
                    visited[currentNode] = true;
                    count++;
                }

                foreach (var childNode in graph[currentNode])
                {
                    nodes.Push(childNode);
                }

                if (graph[currentNode].All(x => visited[x]))
                {
                    if (count > maxCount)
                    {
                        maxCount = count;
                    }

                    count = 0;
                }
            }

            Console.WriteLine(maxCount);
        }

        private static void ReadInput()
        {
            var n = int.Parse(Console.ReadLine());
            start = int.Parse(Console.ReadLine());

            graph = new Dictionary<int, List<int>>();
            graph[start] = new List<int>();

            for (var i = 0; i < n; i++)
            {
                var nums = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var fr1 = nums[0];
                var fr2 = nums[1];

                if (!graph.ContainsKey(fr1))
                {
                    graph[fr1] = new List<int>();
                }

                if (!graph.ContainsKey(fr2))
                {
                    graph[fr2] = new List<int>();
                }

                graph[fr1].Add(fr2);
                graph[fr2].Add(fr1);
            }
        }
    }
}
