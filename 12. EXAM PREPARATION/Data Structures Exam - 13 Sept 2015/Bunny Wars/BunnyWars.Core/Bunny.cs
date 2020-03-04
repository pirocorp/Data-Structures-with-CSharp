namespace BunnyWars.Core
{
    using System;

    public class Bunny : IComparable<Bunny>
    {
        public Bunny(string name, int team, int roomId)
        {
            this.Name = name;
            this.Team = team;
            this.RoomId = roomId;
            this.Health = 100;
            this.Score = 0;
        }

        public int RoomId { get; set; }

        public string Name { get; }

        public int Health { get; set; }

        public int Score { get; set; }

        public int Team { get; private set; }

        public virtual int CompareTo(Bunny other)
        {
            if (this == other)
            {
                return 0;
            }

            var cmp = - string.Compare(this.Name, other.Name, 
                StringComparison.InvariantCulture);

            return cmp;
        }

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            var other = (Bunny) obj;
            return this.Name != other.Name;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        public override string ToString()
        {
            return $"Name: {this.Name}, Room: {this.RoomId}, Team: {this.Team}, Score: {this.Score}";
        }
    }
}
