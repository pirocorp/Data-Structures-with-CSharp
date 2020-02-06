using System;
using System.Collections.Generic;
using System.Linq;

public class LongestPathProgram
{
    static readonly Dictionary<int, Tree<int>> Nodes = new Dictionary<int, Tree<int>>();

    static void Main()
    {
        ReadTree();
        var root = GetRoot();
        var longestPath = GetLongestPath(root);

        var result = longestPath.Select(x => x.Value).ToArray();

        Console.WriteLine($"Longest path: {string.Join(" ", result)}");
    }

    private static List<Tree<int>> GetLongestPath(Tree<int> root)
    {
        var result = new List<Tree<int>>();

        LongestBFS(root, new List<Tree<int>>(), ref result);

        return result;
    }

    private static void LongestBFS(Tree<int> node, List<Tree<int>> currentPath, ref List<Tree<int>> longestPath)
    {
        currentPath.Add(node);

        if (node.Children.Count == 0 && longestPath.Count < currentPath.Count)
        {
            longestPath = currentPath;
        }

        foreach (var child in node.Children)
        {
            LongestBFS(child, new List<Tree<int>>(currentPath), ref longestPath);
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