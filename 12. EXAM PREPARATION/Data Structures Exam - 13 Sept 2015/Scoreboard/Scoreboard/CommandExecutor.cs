﻿using System;
using System.Linq;
using System.Text;

namespace Scoreboard
{
    public class CommandExecutor
    {
        private readonly Scoreboard _scoreboard = new Scoreboard();

        public string ProcessCommand(string commandLine)
        {
            var tokens = commandLine.Split(new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);
            var command = tokens[0];
            switch (command)
            {
                case "RegisterUser":
                    return this.RegisterUser(tokens[1], tokens[2]);
                case "RegisterGame":
                    return this.RegisterGame(tokens[1], tokens[2]);
                case "AddScore":
                    return this.AddScore(tokens[1], tokens[2], tokens[3], tokens[4], int.Parse(tokens[5]));
                case "ShowScoreboard":
                    return this.ShowScoreboard(tokens[1]);
                case "DeleteGame":
                    return this.DeleteGame(tokens[1], tokens[2]);
                case "ListGamesByPrefix":
                    return this.ListGamesByPrefix(tokens[1]);
                default:
                    return "Incorrect command";
            }
        }

        private string RegisterUser(string username, string userPassword)
        {
            if (this._scoreboard.RegisterUser(username, userPassword))
            {
                return "User registered";
            }

            return "Duplicated user";
        }

        private string RegisterGame(string gameName, string gamePassword)
        {
            if (this._scoreboard.RegisterGame(gameName, gamePassword))
            {
                return "Game registered";
            }

            return "Duplicated game";
        }

        private string AddScore(string username, string userPassword,
            string gameName, string gamePassword, int score)
        {
            if (this._scoreboard.AddScore(username, userPassword, gameName, gamePassword, score))
            {
                return "Score added";
            }

            return "Cannot add score";
        }

        private string ShowScoreboard(string gameName)
        {
            var scoreboardEntries = this._scoreboard.ShowScoreboard(gameName);
            if (scoreboardEntries == null)
            {
                return "Game not found";
            }

            if (scoreboardEntries.Any())
            {
                var result = new StringBuilder();
                int counter = 0;
                foreach (var entry in scoreboardEntries)
                {
                    counter++;
                    result.AppendFormat("#{0} {1} {2}", counter, entry.Username, entry.Score);
                    result.AppendLine();
                }
                result.Length -= Environment.NewLine.Length;
                return result.ToString();
            }

            return "No score";
        }

        private string DeleteGame(string gameName, string gamePassword)
        {
            if (this._scoreboard.DeleteGame(gameName, gamePassword))
            {
                return "Game deleted";
            }

            return "Cannot delete game";
        }

        private string ListGamesByPrefix(string namePrefix)
        {
            var matchedGames = this._scoreboard.ListGamesByPrefix(namePrefix);
            if (matchedGames.Any())
            {
                return string.Join(", ", matchedGames);
            }

            return "No matches";
        }
    }
}