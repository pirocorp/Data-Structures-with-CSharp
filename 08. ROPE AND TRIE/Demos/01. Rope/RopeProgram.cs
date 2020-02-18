namespace _01._Rope
{
    using System;

    public static class RopeProgram
    {
        public static void Main()
        {
            var rope = new Rope();
            Console.WriteLine("Rope Test");
            char ch;

            do
            {
                Console.WriteLine("\nRope Operations\n");
                Console.WriteLine("1. concat ");
                Console.WriteLine("2. get character at index");
                Console.WriteLine("3. substring");
                Console.WriteLine("4. clear");

                var choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter string to concat");
                        rope.Concat(Console.ReadLine());
                        break;
                    case 2:
                        Console.WriteLine("Enter index");
                        Console.WriteLine("Character at index = " + rope.CharacterAt(int.Parse(Console.ReadLine())));
                        break;
                    case 3:
                        Console.WriteLine("Enter integer start and end limit");
                        Console.WriteLine("Substring : " + rope.Substring(int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine())));
                        break;
                    case 4:
                        Console.WriteLine("\nRope Cleared\n");
                        rope.Clear();
                        break;
                    default:
                        Console.WriteLine("Wrong Entry \n ");
                        break;
                }
                /**  Display rope  **/
                Console.WriteLine("\nRope : ");
                rope.Print();

                Console.WriteLine("\nDo you want to continue (Type y or n) \n");
                ch = Console.ReadLine()[0];
            } while (ch == 'Y' || ch == 'y');
        }
    }
}