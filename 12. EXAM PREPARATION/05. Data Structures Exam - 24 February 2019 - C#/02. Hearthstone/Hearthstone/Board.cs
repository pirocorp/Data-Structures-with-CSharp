using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hearthstone;

public class Board : IBoard
{
    private Dictionary<string, Card> _byName;
    private readonly CardComparer _cardComparer;

    public Board()
    {
        this._byName = new Dictionary<string, Card>();
        this._cardComparer = new CardComparer();
    }

    public bool Contains(string name)
    {
        return this._byName.ContainsKey(name);
    }

    public int Count()
    {
        return this._byName.Count;
    }

    public void Draw(Card card)
    {
        if (this.Contains(card.Name))
        {
            throw new ArgumentException();
        }

        this._byName.Add(card.Name, card);
    }

    public void Play(string attackerCardName, string attackedCardName)
    {
        if (!this.Contains(attackerCardName) ||
            !this.Contains(attackedCardName))
        {
            throw new ArgumentException();
        }

        var attackCard = this._byName[attackerCardName];
        var targetCard = this._byName[attackedCardName];

        if (attackCard.Level != targetCard.Level ||
            attackCard.Health <= 0)
        {
            throw new ArgumentException();
        }

        if (targetCard.Health <= 0)
        {
            return;
        }

        targetCard.Health -= attackCard.Damage;

        if (targetCard.Health <= 0)
        {
            attackCard.Score += targetCard.Level;
        }
    }

    public void Remove(string name)
    {
        if (!this.Contains(name))
        {
            throw new ArgumentException();
        }

        this._byName.Remove(name);
    }

    public void RemoveDeath()
    {
        this._byName = this._byName.Values
            .Where(x => x.Health > 0)
            .ToDictionary(x => x.Name, x => x);
    }

    public IEnumerable<Card> GetBestInRange(int start, int end)
    {
        return this._byName.Values
            .Where(x => x.Score >= start && x.Score <= end)
            .OrderByDescending(x => x.Level);
    }

    public IEnumerable<Card> ListCardsByPrefix(string prefix)
    {
        return this._byName.Values
            .Where(x => x.Name.StartsWith(prefix))
            .OrderBy(x => x, this._cardComparer);
    }

    public IEnumerable<Card> SearchByLevel(int level)
    {
        return this._byName.Values
            .Where(x => x.Level == level)
            .OrderByDescending(x => x.Score);
    }

    public void Heal(int health)
    {
        var smallestHealth = this._byName.Values
            .OrderBy(x => x.Health)
            .First();

        smallestHealth.Health += health;
    }
}