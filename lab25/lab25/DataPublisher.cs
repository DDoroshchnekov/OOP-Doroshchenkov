namespace lab25;

public sealed class DataPublisher
{
    // processedData, strategyName
    public event Action<string, string>? DataProcessed;

    public void PublishDataProcessed(string processedData, string strategyName)
    {
        DataProcessed?.Invoke(processedData, strategyName);
    }
}