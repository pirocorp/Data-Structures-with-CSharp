using System;
using System.Collections.Generic;

public class AStar
{
    private readonly char[,] map;
    private readonly PriorityQueue<Node> open;
    private readonly Dictionary<Node, Node> parent;
    private readonly Dictionary<Node, int> cost;

    public AStar(char[,] map)
    {
        this.open = new PriorityQueue<Node>();
        this.parent = new Dictionary<Node, Node>();
        this.cost = new Dictionary<Node, int>();
        this.map = map;
    }

    public static int GetH(Node current, Node goal)
    {
        var deltaX = Math.Abs(current.Col - goal.Col);
        var deltaY = Math.Abs(current.Row - goal.Row);

        return deltaX + deltaY;
    }

    public IEnumerable<Node> GetPath(Node start, Node goal)
    {
        this.parent[start] = null;
        this.cost[start] = 0;

        this.open.Enqueue(start);

        while (this.open.Count > 0)
        {
            var current = this.open.Dequeue();

            if (current.Equals(goal))
            {
                break;
            }

            var neighbours = this.GetNeighbours(current);

            foreach (var neighbour in neighbours)
            {
                var newCost = this.cost[current] + 1;

                if (!this.cost.ContainsKey(neighbour) ||
                    newCost < this.cost[neighbour])
                {
                    this.cost[neighbour] = newCost;
                    neighbour.F = newCost + GetH(neighbour, goal);
                    this.open.Enqueue(neighbour);
                    this.parent[neighbour] = current;
                }
            }
        }

        var currentStep = goal;

        var path = new Stack<Node>();

        while (currentStep != null && this.parent.ContainsKey(currentStep))
        {
            path.Push(currentStep);
            currentStep = this.parent[currentStep];
        }

        if (path.Count == 0)
        {
            path.Push(start);
        }

        return path.ToArray();
    }

    private List<Node> GetNeighbours(Node current)
    {
        var neighbours = new List<Node>();

        if (current.Row > 0 && this.map[current.Row - 1, current.Col] != 'W')
        {
            neighbours.Add(new Node(current.Row - 1, current.Col));
        }

        if (current.Col > 0 && this.map[current.Row, current.Col - 1] != 'W')
        {
            neighbours.Add(new Node(current.Row, current.Col - 1));
        }

        if (current.Row < this.map.GetLength(0) - 1 && this.map[current.Row + 1, current.Col] != 'W')
        {
            neighbours.Add(new Node(current.Row + 1, current.Col));
        }

        if (current.Col < this.map.GetLength(1) - 1 && this.map[current.Row, current.Col + 1] != 'W')
        {
            neighbours.Add(new Node(current.Row, current.Col + 1));
        }

        return neighbours;
    }
}