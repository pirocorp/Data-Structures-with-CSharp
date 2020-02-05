namespace _01._Find_the_Root
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class FindTheRootProgram
    {
        private static Node<int>[] nodes;

        public static void Main()
        {
            ReadGraph();
            var rootElements = GetRootElement();

            if (rootElements.Count == 0)
            {
                Console.WriteLine("No root!");
                return;
            }

            if (rootElements.Count > 1)
            {
                Console.WriteLine("Multiple root nodes!");
                return;
            }

            Console.WriteLine(rootElements[0].Value);
        }

        private static List<Node<int>> GetRootElement()
        {
            var result = new List<Node<int>>();

            foreach (var node in nodes)
            {
                if (node.Parent == null)
                {
                    result.Add(node);
                }
            }

            return result;
        }

        private static void ReadGraph()
        {
            var nodesCount = int.Parse(Console.ReadLine());
            var edgesCount = int.Parse(Console.ReadLine());

            nodes = new Node<int>[nodesCount];

            for (var i = 0; i < nodesCount; i++)
            {
                nodes[i] = new Node<int>(i);
            }

            for (var i = 0; i < edgesCount; i++)
            {
                var edge = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var parentNode = edge[0];
                var childNode = edge[1];

                nodes[parentNode].Edges.Add(nodes[childNode]);
                nodes[childNode].Parent = nodes[parentNode];
            }
        }

        private class Node<T>
        {
            public T Value { get; set; }

            public Node<T> Parent { get; set; }

            public List<Node<T>> Edges { get; set; }

            public Node(T value)
            {
                this.Value = value;
                this.Edges = new List<Node<T>>();
            }
        }
    }
}