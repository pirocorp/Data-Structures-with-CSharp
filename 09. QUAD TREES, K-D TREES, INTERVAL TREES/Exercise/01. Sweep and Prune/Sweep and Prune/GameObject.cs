namespace Sweep_and_Prune
{
    public class GameObject
    {
        public string Id { get; private set; }

        public int X1 { get; set; }

        public int Y1 { get; set; }

        public int X2 => this.X1 + 10;

        public int Y2 => this.Y1 + 10;

        public GameObject(string id, int x, int y)
        {
            this.Id = id;
            this.X1 = x;
            this.Y1 = y;
        }
    }
}