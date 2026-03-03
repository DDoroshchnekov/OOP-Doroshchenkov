namespace lab25;

public sealed class FileLoggerFactory : LoggerFactory
{
    private readonly string _path;

    public FileLoggerFactory(string path)
    {
        _path = path ?? throw new ArgumentNullException(nameof(path));
    }

    public override ILogger CreateLogger() => new FileLogger(_path);
}