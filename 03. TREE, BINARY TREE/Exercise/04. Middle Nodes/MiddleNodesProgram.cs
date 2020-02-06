using System;
using System.Collections.Generic;
using System.Linq;

public static class MiddleNodesProgram
{
    static readonly Dictionary<int, Tree<int>> Nodes = new Dictionary<int, Tree<int>>();

    public static void Main()
    {
        ReadTree();
        var root = GetRoot();
        var middleNodes = new List<Tree<int>>();
        GetMiddleNodes(root, middleNodes);

        var result = middleNodes
            .Select(x => x.Value)
            .OrderBy(x => x)
            .ToArray();

        Console.WriteLine($"Middle nodes: {string.Join(" ", result)}");
    }

    private static void GetMiddleNodes(Tree<int> node, List<Tree<int>> middleNodes)
    {
        if (node.Children.Count > 0 && node.Parent != null)
        {
            middleNodes.Add(node);
        }

        foreach (var child in node.Children)
        {
            GetMiddleNodes(child, middleNodes);
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