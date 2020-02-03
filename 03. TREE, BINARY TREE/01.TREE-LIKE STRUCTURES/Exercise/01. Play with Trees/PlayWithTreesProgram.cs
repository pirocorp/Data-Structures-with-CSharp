namespace _01._Play_with_Trees
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class PlayWithTreesProgram
    {
        private static readonly Dictionary<int, Tree<int>> nodeByValue = new Dictionary<int, Tree<int>>();
        private static Tree<int> deepestElement;

        public static void Main()
        {
            var nodesCount = int.Parse(Console.ReadLine());

            Tree<int> tree = null;

            for (var i = 0; i < nodesCount; i++)
            {
                var edge = Console.ReadLine().Split(' ');

                var parentValue = int.Parse(edge[0]);
                var parentNode = GetTreeNodeByValue(parentValue);

                var childValue = int.Parse(edge[1]);
                var childNode = GetTreeNodeByValue(childValue);
                childNode.Parent = parentNode;

                parentNode.Children.Add(childNode);

                if (tree == null)
                {
                    tree = parentNode;
                }
            }

            var pathSum = int.Parse(Console.ReadLine());
            var subtreeSum = int.Parse(Console.ReadLine());

            PrintTreeRootNode(tree);
            Console.WriteLine();

            PrintLeafNodes(tree);
            Console.WriteLine();

            PrintMiddleNodes(tree);
            Console.WriteLine();

            PrintLongestPath(tree);
            Console.WriteLine();

            PrintPathsOfSum(tree, pathSum);
            Console.WriteLine();

            PrintSubTreesOfSum(subtreeSum);
            Console.WriteLine();
        }

        private static void PrintSubTreesOfSum(int subtreeSum)
        {
            var allSubtrees = nodeByValue.Select(x => x.Value).ToArray();

            var result = allSubtrees
                .Where(x => GetSubtreeSum(x) == subtreeSum)
                .ToArray();

            Console.WriteLine($"Subtrees of sum {subtreeSum}:");
            foreach (var subTree in result)
            {
                PrintSubtree(subTree);
                Console.WriteLine();
            }
        }

        private static void PrintSubtree(Tree<int> subTree)
        {
            Console.Write(subTree.Value);

            foreach (var child in subTree.Children)
            {
                Console.Write($" + ");
                PrintSubtree(child);
            }
        }

        private static int GetSubtreeSum(Tree<int> tree)
        {
            return tree.Value + tree.Children.Select(GetSubtreeSum).Sum();
        }

        private static void PrintPathsOfSum(Tree<int> tree, int pathSum)
        {
            var paths = GetPaths(tree);
            var result = paths
                .Where(p => p.Sum() == pathSum)
                .ToArray();

            Console.WriteLine($"Paths of sum {pathSum}:");
            foreach (var path in result)
            {
                path.Reverse();
                Console.WriteLine(string.Join(" -> ", path));
            }
        }

        private static List<List<int>> GetPaths(Tree<int> tree)
        {
            var leafs = new List<Tree<int>>();
            GetLeafNodes(tree, leafs);
            var paths = new List<List<int>>();

            foreach (var leaf in leafs)
            {
                var currentPath = new List<int>();
                var currentLeaf = leaf;

                while (currentLeaf != null)
                {
                    currentPath.Add(currentLeaf.Value);
                    currentLeaf = currentLeaf.Parent;
                }

                paths.Add(currentPath);
            }

            return paths;
        }

        private static void PrintLongestPath(Tree<int> tree)
        {
            var maxDepth = 0;
            GetDeepestElement(tree, ref maxDepth);

            var currentDeepest = deepestElement;
            var nodes = new List<int>();

            while (currentDeepest != null)
            {
                nodes.Add(currentDeepest.Value);
                currentDeepest = currentDeepest.Parent;
            }

            nodes.Reverse();

            Console.WriteLine($"Longest path: {string.Join(" -> ", nodes)} (length = {nodes.Count})");
        }

        public static void GetDeepestElement(Tree<int> currentTree, ref int maxDepth)
        {
            if (currentTree.Children.Count > 0)
            {
                maxDepth++;

                for (var i = 0; i < currentTree.Children.Count; i++)
                {
                    GetDeepestElement(currentTree.Children[i], ref maxDepth);
                }
            }

            var currentDepth = GetDepth(currentTree);

            if (maxDepth < currentDepth)
            {
                maxDepth = currentDepth;
                deepestElement = currentTree;
            }
        }

        private static int GetDepth(Tree<int> child)
        {
            var depth = 1;

            while (child.Parent != null)
            {
                depth++;
                child = child.Parent;
            }

            return depth;
        }

        private static void PrintMiddleNodes(Tree<int> tree)
        {
            var middleNodes = new List<Tree<int>>();

            GetMiddleNodes(tree, middleNodes);

            var result = middleNodes
                .Select(x => x.Value)
                .OrderBy(x => x)
                .ToArray();

            Console.WriteLine($"Middle nodes: {string.Join(", ", result)}");
        }

        private static void GetMiddleNodes(Tree<int> tree, List<Tree<int>> middleNodes)
        {
            if (tree.Parent != null && tree.Children.Count > 0)
            {
                middleNodes.Add(tree);
            }

            if(tree.Children.Count > 0)
            {
                foreach (var child in tree.Children)
                {
                    GetMiddleNodes(child, middleNodes);
                }
            }
        }

        private static void PrintLeafNodes(Tree<int> tree)
        {
            var leafs = new List<Tree<int>>();

            GetLeafNodes(tree, leafs);

            var result = leafs
                .Select(x => x.Value)
                .OrderBy(x => x)
                .ToArray();

            Console.WriteLine($"Leaf nodes: {string.Join(", ", result)}");
        }

        private static void GetLeafNodes(Tree<int> tree, List<Tree<int>> leafs)
        {
            if (tree.Children.Count == 0)
            {
                leafs.Add(tree);
            }

            foreach (var child in tree.Children)
            {
                GetLeafNodes(child, leafs);
            }
        }

        private static void PrintTreeRootNode(Tree<int> tree)
        {
            Console.WriteLine($"Root node: {tree.Value}");
        }

        public static Tree<int> GetTreeNodeByValue(int value)
        {
            if (!nodeByValue.ContainsKey(value))
            {
                nodeByValue[value] = new Tree<int>(value);
            }

            return nodeByValue[value];
        }
    }
}