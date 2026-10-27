namespace ConsoleApp;

public static class Input
{
    public static string ReadText(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine()?.Trim() ?? "";
    }

    public static int? ReadInt(string prompt)
    {
        Console.Write(prompt);

        if (int.TryParse(Console.ReadLine(), out var value))
            return value;

        Console.WriteLine("Нужно ввести целое число.");
        return null;
    }
}