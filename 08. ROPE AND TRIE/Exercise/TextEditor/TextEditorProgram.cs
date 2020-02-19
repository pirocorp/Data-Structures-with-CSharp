using System;

public static class TextEditorProgram
{
    public static void Main()
    {
        var textEditor = new StringEditor();
        while (true)
        {
            var input = Console.ReadLine();
            var text = input.Split('"');
            var commands = input.Split();
            if (commands[0] == "end")
            {
                break;
            }

            switch (commands[0])
            {
                case "login":
                    textEditor.Login(commands[1]);
                    break;
                case "logout":
                    textEditor.Logout(commands[1]);
                    break;
                case "users":
                    if (commands.Length > 1)
                    {
                        foreach (var user in textEditor.Users(commands[1]))
                        {
                            Console.WriteLine(user);
                        }
                    }
                    else
                    {
                        foreach (var user in textEditor.Users())
                        {
                            Console.WriteLine(user);
                        }
                    }
                    break;
                default:
                    var name = commands[0];
                    var command = commands[1];

                    switch (command)
                    {
                        case "insert":
                            var index = int.Parse(commands[2]);
                            textEditor.Insert(name, index, text[1]);
                            break;
                        case "prepend":
                            textEditor.Prepend(name, text[1]);
                            break;
                        case "substring":
                            var startIndex = int.Parse(commands[2]);
                            var length = int.Parse(commands[3]);
                            textEditor.Substring(name, startIndex, length);
                            break;
                        case "delete":
                            var startIndexD = int.Parse(commands[2]);
                            var lengthD = int.Parse(commands[3]);
                            textEditor.Delete(name, startIndexD, lengthD);
                            break;
                        case "clear":
                            textEditor.Clear(name);
                            break;
                        case "length":
                            Console.WriteLine(textEditor.Length(name));
                            break;
                        case "print":
                            try
                            {
                                Console.WriteLine(textEditor.Print(name));
                            }
                            catch (Exception)
                            {
                            }
                            break;
                        case "undo":
                            textEditor.Undo(name);
                            break;
                        default: throw new InvalidOperationException();
                    }
                    break;
            }
        }
    }

    private static void Test()
    {
        var stringEditor = new StringEditor();
        var currentUser = "Pesho";
        stringEditor.Login(currentUser);
        stringEditor.Login("Gosho");
        stringEditor.Login("Pavel");
        stringEditor.Login("Asen");
        stringEditor.Prepend(currentUser, "DEF");
        stringEditor.Prepend(currentUser, " ");
        stringEditor.Prepend(currentUser, "abc");
        stringEditor.Prepend(currentUser, " ");
        stringEditor.Prepend(currentUser, "ZDF");
        Console.WriteLine(stringEditor.Print(currentUser));

        stringEditor.Delete(currentUser, 1, 2);

        Console.WriteLine(stringEditor.Print(currentUser));

        stringEditor.Insert(currentUser, 1, "_NEW STRING_");

        Console.WriteLine(stringEditor.Print(currentUser));

        Console.WriteLine(stringEditor.Length(currentUser));

        stringEditor.Substring(currentUser, 1, 12);

        Console.WriteLine(stringEditor.Print(currentUser));

        stringEditor.Undo(currentUser);

        Console.WriteLine(stringEditor.Print(currentUser));

        stringEditor.Clear(currentUser);

        Console.WriteLine(stringEditor.Print(currentUser));

        foreach (var user in stringEditor.Users("P"))
        {
            Console.WriteLine(user);
        }
    }
}