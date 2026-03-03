namespace lab25;

public sealed class ProcessingLoggerObserver
{
    public void Subscribe(DataPublisher publisher)
    {
        publisher.DataProcessed += OnDataProcessed;
    }

    public void Unsubscribe(DataPublisher publisher)
    {
        publisher.DataProcessed -= OnDataProcessed;
    }

    private void OnDataProcessed(string processedData, string strategyName)
    {
        LoggerManager.Instance.Log($"Observer: Data processed with '{strategyName}'. Result = {processedData}");
    }
}