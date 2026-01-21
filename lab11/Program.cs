using System;
using System.Threading;
using Polly;
using Polly.CircuitBreaker;
using Polly.Timeout;

internal class Program
{
    private static int _apiAttempts = 0;
    private static int _dbAttempts = 0;

    static void Main(string[] args)
    {
        Console.WriteLine("===== IndependentWork11 — Polly Demo =====\n");

        Scenario1_RetryApi();
        Scenario2_CircuitBreakerDb();
        Scenario3_TimeoutOperation();

        Console.WriteLine("\n===== END =====");
    }
    static void Scenario1_RetryApi()
    {
        Console.WriteLine("\n--- Scenario 1: External API + Retry ---");
        string FakeApiCall()
        {
            _apiAttempts++;
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] API Attempt {_apiAttempts}");
            if (_apiAttempts <= 2)
                throw new Exception("API тимчасово недоступний (імітація).");
            return "API Response OK";
        }
        var retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetry(
                retryCount: 3,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(1),
                onRetry: (exception, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Retry {retryCount} after {timeSpan.TotalSeconds}s. Reason: {exception.Message}");
                });

        try
        {
            var result = retryPolicy.Execute(() => FakeApiCall());
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Final result: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Operation failed after retries: {ex.Message}");
        }
    }
    static void Scenario2_CircuitBreakerDb()
    {
        Console.WriteLine("\n--- Scenario 2: Database + Circuit Breaker ---");
        string FakeDbQuery()
        {
            _dbAttempts++;
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] DB Attempt {_dbAttempts}");
            throw new Exception("Помилка з'єднання з БД (імітація).");
        }
        var breakerPolicy = Policy
            .Handle<Exception>()
            .CircuitBreaker(
                exceptionsAllowedBeforeBreaking: 2,
                durationOfBreak: TimeSpan.FromSeconds(5),
                onBreak: (ex, breakDelay) =>
                {
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Circuit OPEN. Reason: {ex.Message}. Break for {breakDelay.TotalSeconds}s");
                },
                onReset: () =>
                {
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Circuit CLOSED. Operations allowed again.");
                },
                onHalfOpen: () =>
                {
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Circuit HALF-OPEN. Testing...");
                });
        for (int i = 0; i < 4; i++)
        {
            try
            {
                breakerPolicy.Execute(() => FakeDbQuery());
            }
            catch (BrokenCircuitException bce)
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Call prevented by circuit: {bce.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] DB call failed: {ex.Message}");
            }
            Thread.Sleep(1000);
        }
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Waiting 6 seconds to allow circuit to reset...");
        Thread.Sleep(6000);
        try
        {
            breakerPolicy.Execute(() => FakeDbQuery());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] After wait: {ex.Message}");
        }
    }
    static void Scenario3_TimeoutOperation()
    {
        Console.WriteLine("\n--- Scenario 3: Long Operation + Timeout ---");
        void LongOperation()
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Start long operation...");
            Thread.Sleep(4000); // 4 секунди — більше, ніж таймаут політики
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Long operation finished (this line may not appear if timeout triggers).");
        }
        var timeoutPolicy = Policy.Timeout(
            seconds: 2,
            timeoutStrategy: TimeoutStrategy.Pessimistic,
            onTimeout: (context, timespan, task) =>
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Timeout triggered after {timespan.TotalSeconds}s.");
            });

        try
        {
            timeoutPolicy.Execute(() => LongOperation());
        }
        catch (TimeoutRejectedException)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Operation was cancelled due to timeout.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Unexpected error: {ex.Message}");
        }
    }
}
