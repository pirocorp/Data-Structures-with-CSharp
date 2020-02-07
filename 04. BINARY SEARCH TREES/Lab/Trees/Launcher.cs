using System;
using System.Collections.Generic;
using System.Linq;

public class Launcher
{
    public static void Main()
    {
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(8);
        bst.Insert(9);
        bst.Insert(37);
        bst.Insert(39);
        bst.Insert(45);

        IEnumerable<int> result = bst.Range(4, 37);

        //4, 5, 8, 9, 10, 37
        Console.WriteLine(string.Join(" ", result.ToArray()));
    }
}