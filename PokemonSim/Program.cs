// Program.cs
class Program
{
    static void Main(string[] args)
    {
        try
        {
            var ui = new ConsoleUI();
            ui.Run();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"An error occurred: {ex.Message}");
            Console.ResetColor();
        }
    }
}
