using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace PitFortress
{
    using Classes;
    using Interfaces;

    public class PitFortressCollection : IPitFortress
    {
        private readonly Dictionary<string, Player> _players;
        private readonly SortedSet<Player> _playersScores;
        private readonly OrderedDictionary<int, SortedSet<Minion>> _minions;
        private readonly SortedSet<Mine> _mines;

        private int _mineId = 1;
        private int _minionId = 1;

        public PitFortressCollection()
        {
            this._players = new Dictionary<string, Player>();
            this._playersScores = new SortedSet<Player>();
            this._minions = new OrderedDictionary<int, SortedSet<Minion>>();
            this._mines = new SortedSet<Mine>();
        }

        public int PlayersCount => this._players.Count;

        public int MinionsCount => this._minions
            .Sum(x => x.Value.Count);

        public int MinesCount => this._mines.Count;

        public void AddPlayer(string name, int mineRadius)
        {
            if (this._players.ContainsKey(name))
            {
                throw new ArgumentException();
            }

            if (mineRadius < 0)
            {
                throw new ArgumentException();
            }

            var player = new Player(name, mineRadius);
            this._players.Add(name, player);
            this._playersScores.Add(player);
        }

        public void AddMinion(int xCoordinate)
        {
            if (xCoordinate < 0 || xCoordinate > 1_000_000)
            {
                throw new ArgumentException();
            }

            if (!this._minions.ContainsKey(xCoordinate))
            {
                this._minions[xCoordinate] = new SortedSet<Minion>();
            }

            var minion = new Minion(this._minionId++, xCoordinate);
            this._minions[xCoordinate].Add(minion);
        }

        public void SetMine(string playerName, int xCoordinate, int delay, int damage)
        {
            if (!this._players.ContainsKey(playerName))
            {
                throw new ArgumentException();
            }

            if (xCoordinate < 0 || xCoordinate > 1_000_000)
            {
                throw new ArgumentException();
            }

            if (delay < 1 || delay > 10_000)
            {
                throw new ArgumentException();
            }

            if (damage < 0 || damage > 100)
            {
                throw new ArgumentException();
            }

            var player = this._players[playerName];
            var mine = new Mine(this._mineId++, delay, damage, xCoordinate, player);
            this._mines.Add(mine);
        }

        public IEnumerable<Minion> ReportMinions()
        {
            return this._minions
                .SelectMany(x => x.Value);
        }

        public IEnumerable<Player> Top3PlayersByScore()
        {
            if (this.PlayersCount < 3)
            {
                throw new ArgumentException();
            }

            return this._playersScores.Reverse().Take(3);
        }

        public IEnumerable<Player> Min3PlayersByScore()
        {
            if (this.PlayersCount < 3)
            {
                throw new ArgumentException();
            }

            return this._playersScores.Take(3);
        }

        public IEnumerable<Mine> GetMines()
        {
            return this._mines;
        }

        public void PlayTurn()
        {
            var minesToDetonate = new List<Mine>();

            foreach (var mine in this._mines)
            {
                mine.Delay -= 1;

                if (mine.Delay <= 0)
                {
                    minesToDetonate.Add(mine);
                }
            }

            foreach (var mine in minesToDetonate)
            {
                var start = mine.XCoordinate - mine.Player.Radius;
                var end = mine.XCoordinate + mine.Player.Radius;

                var player = mine.Player;

                var minionsToUpdate = this._minions
                    .Range(start, true, end, true)
                    .SelectMany(x => x.Value)
                    .ToList();

                foreach (var minion in minionsToUpdate)
                {
                    minion.Health -= mine.Damage;

                    if (minion.Health <= 0)
                    {
                        this._playersScores.Remove(player);
                        mine.Player.Score++;
                        this._playersScores.Add(player);

                        this._minions[minion.XCoordinate].Remove(minion);
                    }
                }
            }

            foreach (var mine in minesToDetonate)
            {
                this._mines.Remove(mine);
            }
        }
    }
}
