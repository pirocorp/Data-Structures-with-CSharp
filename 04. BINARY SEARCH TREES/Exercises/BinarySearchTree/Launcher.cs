using System;

public class Launcher
{
    public static void Main()
    {
        SimpleTest1();
    }

    private static void SimpleTest1()
    {
        var bst = new BinarySearchTree<int>();

        Console.WriteLine(bst.Count());
        Console.WriteLine(new string('-', 100));

        bst.Insert(60);

        PrintTree(bst);

        bst.DeleteMax();

        PrintTree(bst);

        bst.Insert(30);
        bst.Insert(20);
        bst.Insert(45);
        bst.Insert(5);
        bst.Insert(25);
        bst.Insert(35);
        bst.Insert(50);
        bst.Insert(3);
        bst.Insert(4);
        bst.Insert(5);
        bst.Insert(23);
        bst.Insert(28);
        bst.Insert(33);
        bst.Insert(50);
        bst.Insert(22);
        bst.Insert(24);
        bst.Insert(27);
        bst.Insert(29);
        bst.Insert(26);
        bst.Insert(65);
        bst.Insert(70);
        bst.Insert(63);
        bst.Insert(40);
        bst.Insert(38);
        bst.Insert(42);
        bst.Insert(55);
        bst.Insert(53);
        bst.Insert(57);

        var r = bst.Rank(27);
        Console.WriteLine(r);
        Console.WriteLine(new string('-', 100));

        Console.WriteLine(bst.MinElement());
        Console.WriteLine(new string('-', 100));

        Console.WriteLine(bst.MaxElement());
        Console.WriteLine(new string('-', 100));

        Console.WriteLine(bst.Select(9));
        Console.WriteLine(new string('-', 100));

        Console.WriteLine(bst.Floor(4));
        Console.WriteLine(new string('-', 100));

        Console.WriteLine(bst.Ceiling(25));
        Console.WriteLine(new string('-', 100));

        PrintTree(bst);
        bst.Delete(25);
        PrintTree(bst);

        Console.WriteLine(bst.Count());
        Console.WriteLine(new string('-', 100));

        Console.WriteLine(bst.Rank(5));
        Console.WriteLine(new string('-', 100));

        Console.WriteLine(bst.Select(2));
        Console.WriteLine(new string('-', 100));

        PrintTree(bst);
        bst.Delete(30);
        PrintTree(bst);
    }

    private static void PrintTree(BinarySearchTree<int> bst)
    {
        foreach (var i in bst)
        {
            Console.Write($"{i} ");
        }

        Console.WriteLine();
        Console.WriteLine(new string('-', 100));
    }
}