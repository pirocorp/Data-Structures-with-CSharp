namespace _04._Longest_Path_in_a_Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class LongestPathInTreeProgram
    {
        private static int totalBestSum = int.MinValue;

        private class Node
        {
            public Node(int value)
            {
                this.Value = value;
                this.Children = new List<Node>();
            }

            public int Value { get; set; }

            public List<Node> Children { get; set; }

            public int BestSum { get; set; }

            public int SecondBestSum { get; set; }

            public bool HasParent { get; set; }

            public override string ToString()
            {
                return this.Value.ToString();
            }
        }

        public static void Main()
        {
            var nodesCount = int.Parse(Console.ReadLine());
            var edgesCount = int.Parse(Console.ReadLine());
            var tree = new Dictionary<int, Node>();

            for (var i = 0; i < edgesCount; i++)
            {
                var data = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                var node = data[0];
                var child = data[1];

                if (!tree.ContainsKey(node))
                {
                    tree[node] = new Node(node);
                }

                if (!tree.ContainsKey(child))
                {
                    tree[child] = new Node(child);
                }

                tree[child].HasParent = true;
                tree[node].Children.Add(tree[child]);
            }

            var root = tree.Single(x => !x.Value.HasParent).Value;
            CalculateBestSumsDfs(root);

            Console.WriteLine(totalBestSum);
        }

        private static void CalculateBestSumsDfs(Node node)
        {
            if (node.Children.Count == 0)
            {
                return;
            }

            var bestSum = int.MinValue;
            var secondBestSum = int.MinValue;

            foreach (var child in node.Children)
            {
                CalculateBestSumsDfs(child);

                var childSum = child.BestSum + child.Value;

                if (childSum > bestSum)
                {
                    secondBestSum = bestSum;
                    bestSum = childSum;
                }
                else if(childSum > secondBestSum)
                {
                    secondBestSum = childSum;
                }
            }

            node.BestSum = bestSum;
            node.SecondBestSum = secondBestSum;

            if (node.BestSum != int.MinValue && 
                node.SecondBestSum != int.MinValue)
            {
                var nodeTotalSum = node.BestSum + node.SecondBestSum + node.Value;

                if (nodeTotalSum > totalBestSum)
                {
                    totalBestSum = nodeTotalSum;
                }
            }
        }
    }
}