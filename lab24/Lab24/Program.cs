using System;
using Lab24.Observers;
using Lab24.Strategies;

namespace Lab24
{
    class Program
    {
        static void Main()
        {
            var publisher = new ResultPublisher();

            var consoleObserver = new ConsoleLoggerObserver();
            var historyObserver = new HistoryLoggerObserver();
            var thresholdObserver = new ThresholdNotifierObserver(threshold: 50);

            consoleObserver.Subscribe(publisher);
            historyObserver.Subscribe(publisher);
            thresholdObserver.Subscribe(publisher);

            var processor = new NumericProcessor(new SquareOperationStrategy());

            Run(processor, publisher, 5);

            processor.SetStrategy(new CubeOperationStrategy());
            Run(processor, publisher, 4);

            processor.SetStrategy(new SquareRootOperationStrategy());
            Run(processor, publisher, 81);

            Console.WriteLine("\n--- History ---");
            foreach (var item in historyObserver.History)
                Console.WriteLine(item);

            Console.WriteLine("\nDone.");
        }

        private static void Run(NumericProcessor processor, ResultPublisher publisher, double input)
        {
            var result = processor.Process(input);
            publisher.PublishResult(result, processor.CurrentOperationName);
        }
    }
}