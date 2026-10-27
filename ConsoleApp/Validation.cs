namespace ConsoleApp;

public static class Validation
{
    public static bool HasErrors(List<string> errors)
    {
        foreach (var message in errors)
            Console.WriteLine(message);

        return errors.Count > 0;
    }
}