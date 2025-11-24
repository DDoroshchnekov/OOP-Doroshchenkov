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
            if (shouldRetry == null) shouldRetry = ex => true;

            int attempt = 0;
            TimeSpan delay = initialDelay.Value;

            while (true)
            {
                try
                {
                    attempt++;
                    return operation();
                }
                catch (Exception ex)
                {
                    if (attempt >= retryCount || !shouldRetry(ex))
                        throw;

                    Console.WriteLine($"Спроба {attempt} неуспішна: {ex.Message}. Повтор через {delay.TotalSeconds} сек.");
                    Thread.Sleep(delay);
                    delay = TimeSpan.FromSeconds(delay.TotalSeconds * 2); // експоненційна затримка
                }
            }
        }
    }
}
