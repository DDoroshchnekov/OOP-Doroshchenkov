using System.Collections.Generic;

namespace Lab24.Observers
{
    public class HistoryLoggerObserver
    {
        public List<string> History { get; } = new();

        public void Subscribe(ResultPublisher publisher)
        {
            publisher.ResultCalculated += OnResultCalculated;
        }

        public void Unsubscribe(ResultPublisher publisher)
        {
            publisher.ResultCalculated -= OnResultCalculated;
        }

        private void OnResultCalculated(double result, string operationName)
        {
            History.Add($"Operation: {operationName}, Result: {result}");
        }
    }
}