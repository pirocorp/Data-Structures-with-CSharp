namespace _02._LinkedStack
{
    using System;

    public static class LinkedStackProgram
    {
        public static void Main()
        {
            var list = new LinkedStack<int>();
            list.Push(1);
            list.Push(2);
            list.Push(3);
            list.Push(4);
            list.Push(5);

            var result = list.ToArray();
            Console.WriteLine(string.Join(", ", result));

            foreach (var x in list)
            {
                Console.WriteLine(x);
            }
        }
    }
}