namespace Client.Common;

public class ConsoleColors
{
    public static void Green(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(msg);
    }

    public static void Red(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(msg);
    }

    public static void White(string msg)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(msg);
    }

    public static void Yellow(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(msg);
    }

    internal static void Cyan(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write(msg);
    }

    internal static void DarkYellow(string msg)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write(msg);
    }
}
