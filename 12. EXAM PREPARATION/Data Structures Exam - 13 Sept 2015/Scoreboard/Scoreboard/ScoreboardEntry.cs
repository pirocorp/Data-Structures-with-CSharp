namespace Scoreboard
{
    using System;

    public class ScoreboardEntry : IComparable<ScoreboardEntry>
    {
        public ScoreboardEntry(string username, int score)
        {
            this.Score = score;
            this.Username = username;
        }

        public int CompareTo(ScoreboardEntry other)
        {
            var cmp = other.Score.CompareTo(this.Score);

            if (cmp == 0)
            {
                cmp = string.Compare(this.Username, other.Username, StringComparison.InvariantCulture);
            }

            return cmp;
        }

        public int Score { get; private set; }

        public string Username { get; private set; }

        public override string ToString()
        {
            return $"{this.Username} {this.Score}";
        }
    }
}