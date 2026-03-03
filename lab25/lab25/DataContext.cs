namespace lab25;

public sealed class DataContext
{
    private IDataProcessorStrategy _strategy;

    public DataContext(IDataProcessorStrategy strategy)
    {
        _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
    }

    public void SetStrategy(IDataProcessorStrategy strategy)
    {
        _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
    }

    public string CurrentStrategyName => _strategy.Name;

    public string ProcessData(string input) => _strategy.Process(input);
}