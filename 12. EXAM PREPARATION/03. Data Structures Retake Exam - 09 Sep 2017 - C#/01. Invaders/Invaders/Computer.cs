using System;
using System.Collections.Generic;
using System.Linq;

public class Computer : IComputer
{
    private int _energy;
    private Dictionary<int, List<LinkedListNode<Invader>>> _byDistance;
    private readonly LinkedList<Invader> _byInsertion;
    private int _steps;

    public Computer(int energy)
    {
        if (energy < 0)
        {
            throw new ArgumentException();
        }

        this._energy = energy;
        this._byDistance = new Dictionary<int, List<LinkedListNode<Invader>>>();
        this._byInsertion = new LinkedList<Invader>();
    }

    public int Energy => this._energy;

    public void Skip(int turns)
    {
        this._steps += turns;

        this._byDistance = this._byDistance
            .SelectMany(distance => distance.Value)
            .Where(node =>
            {
                if (node.Value.Distance <= turns)
                {
                    this._energy -= node.Value.Damage;
                    this._byInsertion.Remove(node);
                }

                return node.Value.Distance > turns;
            })
            .Select(node =>
            {
                node.Value.Distance -= turns;
                return node;
            })
            .GroupBy(node => node.Value.Distance)
            .ToDictionary(group => group.Key, group => group.ToList());

        if (this._energy < 0)
        {
            this._energy = 0;
        }
    }

    public void AddInvader(Invader invader)
    {
        if (!this._byDistance.ContainsKey(invader.Distance))
        {
            this._byDistance[invader.Distance] = new List<LinkedListNode<Invader>>();
        }

        var node = new LinkedListNode<Invader>(invader);
        this._byDistance[invader.Distance].Add(node);
        this._byInsertion.AddLast(node);
    }

    public void DestroyHighestPriorityTargets(int count)
    {
        var enumerator = this._byDistance
            .SelectMany(distance => distance.Value)
            .OrderBy(node => node.Value);

        var toBeRemoved = enumerator
            .Take(count);

        foreach (var node in toBeRemoved)
        {
            this._byInsertion.Remove(node);
        }

        this._byDistance =  enumerator.Skip(count)
            .GroupBy(node => node.Value.Distance)
            .ToDictionary(group => group.Key, group => group.ToList());
    }

    public void DestroyTargetsInRadius(int radius)
    {
        var toBeRemoved = this._byDistance
            .Where(distance => distance.Key <= radius)
            .ToList();

        toBeRemoved
            .ForEach(distance => distance.Value.ForEach(node => this._byInsertion.Remove(node)));

        this._byDistance = this._byDistance
            .Where(distance => distance.Key > radius)
            .ToDictionary(distance => distance.Key, distance => distance.Value);
    }

    public IEnumerable<Invader> Invaders()
    {
        return this._byInsertion;
    }
}
