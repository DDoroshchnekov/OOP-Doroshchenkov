namespace lab25;

public sealed class ConsoleLogger : ILogger
{
    public void Log(string message) => Console.WriteLine($"[ConsoleLogger] {message}");
}