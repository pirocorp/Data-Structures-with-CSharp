using System;

class MassEffectGalaxyMapProgram
{
    static void Main(string[] args)
    {
        var starClusters = int.Parse(Console.ReadLine());
        var reportsMake = int.Parse(Console.ReadLine());
        var galaxySize = int.Parse(Console.ReadLine());

        var galaxy = new Rectangle(0, 0, galaxySize, galaxySize);
        var universe = new KdTree(galaxy);

        for (var i = 0; i < starClusters; i++)
        {
            var star = Console.ReadLine().Split();
            var name = star[0];
            var x = double.Parse(star[1]);
            var y = double.Parse(star[2]);

            var coordinates = new Point2D(x, y);
            universe.Insert(coordinates);
        }

        for (var i = 0; i < reportsMake; i++)
        {
            var report = Console.ReadLine().Split();
            var x1 = double.Parse(report[1]);
            var y1 = double.Parse(report[2]);
            var x2 = double.Parse(report[3]);
            var y2 = double.Parse(report[4]);

            var searchedPart = new Rectangle(x1, y1, x2, y2);

            var starsInSearchedPart = universe.RangeSearch(searchedPart);
            Console.WriteLine(starsInSearchedPart);
        }
    }
}