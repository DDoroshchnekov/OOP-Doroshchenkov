using System;

// Strategy
public interface IDataProcessorStrategy
{
    void Process(string data);
}

// Strategies
public class GrayscaleFilterStrategy : IDataProcessorStrategy
{
    public void Process(string data)
    {
        Console.WriteLine($"[Grayscale Filter] Processing image: {data}");
    }
}

public class SepiaFilterStrategy : IDataProcessorStrategy
{
    public void Process(string data)
    {
        Console.WriteLine($"[Sepia Filter] Processing image: {data}");
    }
}

public class BlurFilterStrategy : IDataProcessorStrategy
{
    public void Process(string data)
    {
        Console.WriteLine($"[Blur Filter] Processing image: {data}");
    }
}

// Context
public class DataContext
{
    private IDataProcessorStrategy _strategy;

    public DataContext(IDataProcessorStrategy strategy)
    {
        _strategy = strategy;
    }

    public void SetStrategy(IDataProcessorStrategy strategy)
    {
        _strategy = strategy;
    }

    public void ExecuteProcessing(string data)
    {
        _strategy.Process(data);
    }
}

// Observer Publisher
public class DataPublisher
{
    public event Action<string> DataProcessed;

    public void PublishDataProcessed(string data)
    {
        DataProcessed?.Invoke(data);
    }
}

// Observers
public class ConsoleOutputObserver
{
    public void OnDataProcessed(string data)
    {
        Console.WriteLine($"[Console Output] Image displayed: {data}");
    }
}

public class ImageSaverObserver
{
    public void OnDataProcessed(string data)
    {
        Console.WriteLine($"[Image Saver] Image saved: {data}");
    }
}

// Main
class Program
{
    static void Main(string[] args)
    {
        // Context
        DataContext context =
            new DataContext(new GrayscaleFilterStrategy());

        // Publisher
        DataPublisher publisher = new DataPublisher();

        // Observers
        ConsoleOutputObserver consoleObserver =
            new ConsoleOutputObserver();

        ImageSaverObserver saverObserver =
            new ImageSaverObserver();

        // Subscribe observers
        publisher.DataProcessed += consoleObserver.OnDataProcessed;
        publisher.DataProcessed += saverObserver.OnDataProcessed;

        // Grayscale
        context.ExecuteProcessing("photo1.jpg");
        publisher.PublishDataProcessed("photo1.jpg");

        Console.WriteLine();

        // Sepia
        context.SetStrategy(new SepiaFilterStrategy());

        context.ExecuteProcessing("photo2.jpg");
        publisher.PublishDataProcessed("photo2.jpg");

        Console.WriteLine();

        // Blur
        context.SetStrategy(new BlurFilterStrategy());

        context.ExecuteProcessing("photo3.jpg");
        publisher.PublishDataProcessed("photo3.jpg");
    }
}