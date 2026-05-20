using System;

// Strategy
public interface IDataProcessorStrategy
{
    string Process(string data);
}

public class GrayscaleFilterStrategy : IDataProcessorStrategy
{
    public string Process(string data)
    {
        return $"Grayscale: {data}";
    }
}

public class SepiaFilterStrategy : IDataProcessorStrategy
{
    public string Process(string data)
    {
        return $"Sepia: {data}";
    }
}

// Factory
public abstract class StrategyFactory
{
    public abstract IDataProcessorStrategy CreateStrategy();
}

public class GrayscaleFactory : StrategyFactory
{
    public override IDataProcessorStrategy CreateStrategy()
    {
        return new GrayscaleFilterStrategy();
    }
}

public class SepiaFactory : StrategyFactory
{
    public override IDataProcessorStrategy CreateStrategy()
    {
        return new SepiaFilterStrategy();
    }
}

// Singleton
public class ProcessingManager
{
    private static ProcessingManager _instance;

    public static ProcessingManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ProcessingManager();
            }

            return _instance;
        }
    }

    public IDataProcessorStrategy Strategy { get; set; }

    private ProcessingManager() { }

    public string Execute(string data)
    {
        if (Strategy == null)
        {
            return "Strategy not selected";
        }

        return Strategy.Process(data);
    }
}

// Observer
public class DataPublisher
{
    public event Action<string> DataProcessed;

    public void Publish(string data)
    {
        DataProcessed?.Invoke(data);
    }
}

public class ConsoleObserver
{
    public void OnProcessed(string data)
    {
        Console.WriteLine($"Observer received: {data}");
    }
}

class Program
{
    static void Main()
    {
        var manager = ProcessingManager.Instance;

        StrategyFactory factory = new GrayscaleFactory();
        manager.Strategy = factory.CreateStrategy();

        string result = manager.Execute("photo.jpg");

        Console.WriteLine(result);

        DataPublisher publisher = new DataPublisher();

        ConsoleObserver observer = new ConsoleObserver();

        publisher.DataProcessed += observer.OnProcessed;

        publisher.Publish(result);
    }
}