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
            .SelectMany(x => x.Value)
            .Where(x =>
            {
                if (x.Value.Distance <= turns)
                {
                    this._energy -= x.Value.Damage;
                    this._byInsertion.Remove(x);
                }

                return x.Value.Distance > turns;
            })
            .Select(x =>
            {
                x.Value.Distance -= turns;
                return x;
            })
            .GroupBy(x => x.Value.Distance)
            .ToDictionary(x => x.Key, x => x.ToList());

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
            .SelectMany(x => x.Value)
            .OrderBy(x => x.Value);

        var toBeRemoved = enumerator
            .Take(count)
            .ToArray();

        foreach (var node in toBeRemoved)
        {
            this._byInsertion.Remove(node);
        }

        this._byDistance =  enumerator.Skip(count)
            .GroupBy(x => x.Value.Distance)
            .ToDictionary(x => x.Key, x => x.ToList());
    }

    public void DestroyTargetsInRadius(int radius)
    {
        var toBeRemoved = this._byDistance
            .Where(x => x.Key <= radius)
            .ToArray();

        foreach (var distance in toBeRemoved)
        {
            foreach (var node in distance.Value)
            {
                this._byInsertion.Remove(node);
            }
        }

        this._byDistance = this._byDistance
            .Where(x => x.Key > radius)
            .ToDictionary(x => x.Key, y => y.Value);
    }

    public IEnumerable<Invader> Invaders()
    {
        return this._byInsertion;
    }
}
