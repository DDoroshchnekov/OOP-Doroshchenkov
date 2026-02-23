using System;

namespace Lab24.Observers
{
    public class ConsoleLoggerObserver
    {
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
            Console.WriteLine($"[ConsoleLogger] Operation: {operationName}, Result: {result}");
        }
    }
}