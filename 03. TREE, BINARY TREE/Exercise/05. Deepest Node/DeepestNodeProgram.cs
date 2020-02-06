using System;
using System.Collections.Generic;
using System.Linq;

public static class DeepestNodeProgram
{
    static readonly Dictionary<int, Tree<int>> Nodes = new Dictionary<int, Tree<int>>();

    public static void Main()
    {
        ReadTree();
        var root = GetRoot();
        var deepestNode = GetDeepestNode(root);
        Console.WriteLine($"Deepest node: {deepestNode.Value}");
    }

    private static Tree<int> GetDeepestNode(Tree<int> root)
    {
        Tree<int> result = null;
        var maxLevel = 0;

        FindDeepestWithDFS(root, ref result, ref maxLevel, 1);

        return result;
    }

    private static void FindDeepestWithDFS(Tree<int> node, ref Tree<int> result, ref int maxLevel, int currentLevel)
    {
        if (currentLevel > maxLevel)
        {
            result = node;
            maxLevel = currentLevel;
        }

        currentLevel++;

        foreach (var child in node.Children)
        {
            FindDeepestWithDFS(child, ref result, ref maxLevel, currentLevel);
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