namespace PitFortress.Classes
{
    using Interfaces;

    public class Mine : IMine
    {
        //private static readonly IEnumerator<int> IdGenerator;

        ////Static constructor to initialize static field
        ////Static constructors are called maximum 1 time just before 
        ////first call to instance constructor (Lazy loading) 
        //static Mine() 
        //{
        //    IdGenerator = Helpers.MakeId().GetEnumerator();
        //}

        public Mine(int id, int delay, int damage, int x, Player player)
        {
            this.Id = id;//IdGenerator.NextValue();
            this.Delay = delay;
            this.Damage = damage;
            this.XCoordinate = x;
            this.Player = player;
        }

        public int CompareTo(Mine other)
        {
            var cmp = this.Delay.CompareTo(other.Delay);

            if (cmp == 0)
            {
                cmp = this.Id.CompareTo(other.Id);
            }

            return cmp;
        }

        public int Id { get; private set; }

        public int Delay { get; set; }

        public int Damage { get; private set; }

        public int XCoordinate { get; private set; }

        public Player Player { get; private set; }

        public override string ToString()
        {
            return $"X:{this.XCoordinate} Player:{this.Player.Name} Radius: {this.Player.Radius}";
        }
    }
}
