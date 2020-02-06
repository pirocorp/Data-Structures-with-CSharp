using System;
using System.Collections.Generic;
using System.Linq;

public static class PathsWithGivenSumProgram
{
    static readonly Dictionary<int, Tree<int>> Nodes = new Dictionary<int, Tree<int>>();

    public static void Main()
    {
        ReadTree();
        var root = GetRoot();

        var sum = int.Parse(Console.ReadLine());

        var resultPaths = GetPathsWithGivenSum(root, sum);

        Console.WriteLine($"Paths of sum {sum}:");
        foreach (var path in resultPaths)
        {
            Console.WriteLine(string.Join(" ", path.Select(x => x.Value).ToArray()));
        }
    }

    private static List<List<Tree<int>>> GetPathsWithGivenSum(Tree<int> root, int sum)
    {
        var leafs = new List<Tree<int>>();
        GetLeafs(leafs, root, sum, 0);

        var paths = new List<List<Tree<int>>>();

        foreach (var leaf in leafs)
        {
            var currentPath = GetPath(leaf);
            paths.Add(currentPath);
        }

        return paths;
    }

    private static List<Tree<int>> GetPath(Tree<int> leaf)
    {
        var path = new List<Tree<int>>();

        while (leaf != null)
        {
            path.Add(leaf);
            leaf = leaf.Parent;
        }

        path.Reverse();

        return path;
    }

    private static void GetLeafs(List<Tree<int>>leafs, Tree<int> node, int target, int currentSum)
    {
        currentSum += node.Value;

        if (node.Children.Count == 0 && currentSum == target)
        {
            leafs.Add(node);
        }

        foreach (var child in node.Children)
        {
            GetLeafs(leafs, child, target, currentSum);
        }
    }

    private static Tree<int> GetRoot()
    {
        return Nodes.Single(x => x.Value.Parent == null).Value;
    }

    private static void ReadTree()
    {
        var n = int.Parse(Console.ReadLine());

        for (var i = 1; i < n; i++)
        {
            var tokens = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var parentToken = tokens[0];
            var childToken = tokens[1];

            var parentNode = GetNode(parentToken);
            var childNode = GetNode(childToken);

            parentNode.Children.Add(childNode);
            childNode.Parent = parentNode;
        }
    }

    private static Tree<int> GetNode(int node)
    {
        if (!Nodes.ContainsKey(node))
        {
            Nodes[node] = new Tree<int>(node);
        }

        return Nodes[node];
    }
}