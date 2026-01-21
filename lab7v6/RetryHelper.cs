using System;
using System.Threading;

namespace Lab7
{
    public static class RetryHelper
    {
        public static T ExecuteWithRetry<T>(
            Func<T> operation,
            int retryCount = 3,
            TimeSpan? initialDelay = null,
            Func<Exception, bool> shouldRetry = null)
        {
            if (initialDelay == null) initialDelay = TimeSpan.FromSeconds(1);

            for (int attempt = 1; attempt <= retryCount; attempt++)
            {
                try
                {
                    return operation();
                }
                catch (Exception ex)
                {
                    if (shouldRetry != null && !shouldRetry(ex))
                        throw;

                    Console.WriteLine($"Спроба {attempt} неуспішна: {ex.Message}. Повтор через {initialDelay.Value.TotalSeconds} сек.");
                    Thread.Sleep(initialDelay.Value);
                    initialDelay = TimeSpan.FromSeconds(initialDelay.Value.TotalSeconds * 2); // експоненційна затримка
                }
            }
            // Остання спроба
            return operation();
        }
    }
}
