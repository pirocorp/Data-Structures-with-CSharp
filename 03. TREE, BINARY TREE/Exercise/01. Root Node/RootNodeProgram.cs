﻿using System;
using System.Collections.Generic;
using System.Linq;

public static class RootNodeProgram
{
    static readonly Dictionary<int, Tree<int>> Nodes = new Dictionary<int, Tree<int>>();

    public static void Main()
    {
        ReadTree();
        var root = GetRoot();
        Console.WriteLine($"Root node: {root.Value}");
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