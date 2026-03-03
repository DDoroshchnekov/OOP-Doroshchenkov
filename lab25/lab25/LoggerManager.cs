namespace lab25;

public sealed class LoggerManager
{
    private static readonly Lazy<LoggerManager> _instance =
        new(() => new LoggerManager());

    public static LoggerManager Instance => _instance.Value;

    private LoggerFactory? _factory;
    private ILogger? _logger;

    private LoggerManager() { }

    public void Initialize(LoggerFactory factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        _logger = _factory.CreateLogger();
    }

    public void SetFactory(LoggerFactory factory)
    {
        // динамічна зміна фабрики (і логера)
        Initialize(factory);
    }

    public void Log(string message)
    {
        if (_logger is null)
            throw new InvalidOperationException("LoggerManager is not initialized. Call Initialize(factory) first.");

        _logger.Log(message);
    }
}