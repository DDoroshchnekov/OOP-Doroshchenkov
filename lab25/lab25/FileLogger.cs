namespace lab25;

public sealed class FileLogger : ILogger
{
    private readonly string _path;

    public FileLogger(string path)
    {
        _path = path ?? throw new ArgumentNullException(nameof(path));
    }

    public void Log(string message)
    {
        var line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [FileLogger] {message}";
        File.AppendAllText(_path, line + Environment.NewLine);
    }
}