using System;
using System.Collections.Generic;
using System.Linq;

public static class SubtreesWithGivenSumProgram
{
    static readonly Dictionary<int, Tree<int>> Nodes = new Dictionary<int, Tree<int>>();

    public static void Main()
    {
        ReadTree();
        var root = GetRoot();

        var sum = int.Parse(Console.ReadLine());
        var subTrees = GetSubtreesWithGivenSum(root, sum);

        Console.WriteLine($"Subtrees of sum {sum}:");

        foreach (var tree in subTrees)
        {
            tree.Each(x => Console.Write($"{x} "));
            Console.WriteLine();
        }
    }

    private static List<Tree<int>> GetSubtreesWithGivenSum(Tree<int> root, int sum)
    {
        var result = new List<Tree<int>>();

        var rootSum = GetSubtrees(root, sum, 0, result);

        return result;
    }

    private static int GetSubtrees(Tree<int> node, int target, int currentSum, List<Tree<int>> result)
    {
        var childrenSum = 0;

        foreach (var child in node.Children)
        {
            childrenSum += GetSubtrees(child, target, currentSum, result);
        }

        currentSum += node.Value;
        currentSum += childrenSum;

        if (currentSum == target)
        {
            result.Add(node);
        }

        return currentSum;
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