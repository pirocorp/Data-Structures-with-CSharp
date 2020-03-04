namespace Scoreboard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class Scoreboard : IScoreboard
    {
        private readonly Dictionary<string, string> _users;
        private readonly Dictionary<string, string> _games;
        private readonly OrderedDictionary<string, OrderedBag<ScoreboardEntry>> _scores;
        private readonly int _maxEntriesToKeep;

        public Scoreboard(int maxEntriesToKeep = 10)
        {
            this._maxEntriesToKeep = maxEntriesToKeep;
            this._users = new Dictionary<string, string>();
            this._games = new Dictionary<string, string>();
            this._scores = new OrderedDictionary<string,
                OrderedBag<ScoreboardEntry>>(string.CompareOrdinal);
        }

        public bool RegisterUser(string username, string password)
        {
            if (this._users.ContainsKey(username))
            {
                return false;
            }

            this._users.Add(username, password);

            return true;
        }

        public bool RegisterGame(string game, string password)
        {
            if (this._games.ContainsKey(game))
            {
                return false;
            }

            this._games.Add(game, password);
            this._scores.Add(game, new OrderedBag<ScoreboardEntry>());

            return true;
        }

        public bool AddScore(string username, string userPassword,
            string game, string gamePassword, int score)
        {
            if (!this._users.ContainsKey(username) ||
                !this._games.ContainsKey(game) ||
                this._users[username] != (userPassword) || 
                this._games[game] != (gamePassword))
            {
                return false;
            }

            this._scores[game].Add(new ScoreboardEntry(username, score));

            return true;
        }

        public IEnumerable<ScoreboardEntry> ShowScoreboard(string game)
        {
            if (!this._games.ContainsKey(game))
            {
                return null;
            }

            return this._scores[game].Take(this._maxEntriesToKeep);
        }

        public bool DeleteGame(string game, string gamePassword)
        {
            if (!this._games.ContainsKey(game) || 
                this._games[game] != (gamePassword))
            {
                return false;
            }

            this._games.Remove(game);
            this._scores.Remove(game);

            return true;
        }

        public IEnumerable<string> ListGamesByPrefix(string gameNamePrefix)
        {
            var upperBound = gameNamePrefix + char.MaxValue;
            var gamesWithPrefix = this._scores.Range(gameNamePrefix, true, upperBound, false);

            return gamesWithPrefix.Keys.Take(this._maxEntriesToKeep);
        }
    }
}