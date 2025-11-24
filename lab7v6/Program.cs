using System;

namespace Lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Lab 7: Retry Pattern & Exception Handling ===\n");

            var fileProcessor = new FileProcessor();
            var networkClient = new NetworkClient();

            // Використовуємо RetryHelper для FileProcessor
            string fileResult = RetryHelper.ExecuteWithRetry(
                () => fileProcessor.ReadFile("data.txt"),
                retryCount: 5,
                initialDelay: TimeSpan.FromSeconds(1),
                shouldRetry: ex => ex is System.IO.FileNotFoundException
            );

            Console.WriteLine($"Результат FileProcessor: {fileResult}\n");

            // Використовуємо RetryHelper для NetworkClient
            string networkResult = RetryHelper.ExecuteWithRetry(
                () => networkClient.DownloadData("https://example.com"),
                retryCount: 5,
                initialDelay: TimeSpan.FromSeconds(1),
                shouldRetry: ex => ex is System.Net.Http.HttpRequestException
            );

            Console.WriteLine($"Результат NetworkClient: {networkResult}");
        }
    }
}
