namespace _01._Binary_Search_Tree
{
    using System;

    public static class Startup
    {
        public static void Main()
        {
            var bst = new BinarySearchTree<int>();
            bst.Insert(17);
            bst.Insert(10);
            bst.Insert(25);
            bst.Insert(5);
            bst.Insert(15);
            bst.Insert(19);
            bst.Insert(34);
            bst.Insert(7);
            bst.Insert(3);
            bst.Insert(13);
            bst.Insert(16);
            bst.Insert(22);

            foreach (var i in bst.Range(10, 22))
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("-----------------");
            var iter = bst.RangeEnumerator(10, 22);

            foreach (var node in bst.RangeEnumerator(10, 22))
            {
                Console.WriteLine(node);
            }
        }
    }
}