using System;

namespace Lab24.Observers
{
    public class ThresholdNotifierObserver
    {
        public double Threshold { get; }
        public bool WasNotified { get; private set; }
        public string? LastMessage { get; private set; }

        public ThresholdNotifierObserver(double threshold)
        {
            Threshold = threshold;
        }

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
            if (result > Threshold)
            {
                WasNotified = true;
                LastMessage = $"Result {result} exceeded threshold {Threshold} (Operation: {operationName})";
                Console.WriteLine($"[ThresholdNotifier] {LastMessage}");
            }
        }
    }
}