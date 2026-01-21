using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("IndependentWork12 — PLINQ performance & safety demo\n");
        var sizes = new[] { 1_000_000, 3_000_000 }; 
        int workFactor = 40; 
        int runs = 2; 

        foreach (var size in sizes)
        {
            Console.WriteLine($"\n===== Test for collection size: {size:N0} =====");
            var data = GenerateData(size);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            TimeSpan avgLinq = TimeSpan.Zero, avgPlinq = TimeSpan.Zero;
            for (int run = 1; run <= runs; run++)
            {
                Console.WriteLine($"\nRun #{run} (workFactor = {workFactor})");
                var sw = Stopwatch.StartNew();
                var linqResult = data
                    .Where(x => x % 2 >= 0) 
                    .Select(x => HeavyOperation(x, workFactor))
                    .ToList();
                sw.Stop();
                Console.WriteLine($"LINQ time: {sw.Elapsed.TotalSeconds:F3} s (items processed: {linqResult.Count})");
                avgLinq += sw.Elapsed;

                Thread.Sleep(500);

                sw.Restart();
                var plinqResult = data
                    .AsParallel()
                    .WithDegreeOfParallelism(Environment.ProcessorCount) 
                    .Where(x => x % 2 >= 0)
                    .Select(x => HeavyOperation(x, workFactor))
                    .ToList();
                sw.Stop();
                Console.WriteLine($"PLINQ time: {sw.Elapsed.TotalSeconds:F3} s (items processed: {plinqResult.Count})");
                avgPlinq += sw.Elapsed;

                if (linqResult.Count != plinqResult.Count)
                    Console.WriteLine("Warning: counts differ between LINQ and PLINQ results!");
                Thread.Sleep(500);
            }

            Console.WriteLine($"\nAverage LINQ: {avgLinq.TotalSeconds / runs:F3}s; Average PLINQ: {avgPlinq.TotalSeconds / runs:F3}s");
            Console.WriteLine($"Speedup (LINQ / PLINQ): {(avgLinq.TotalSeconds / avgPlinq.TotalSeconds):F2}x");
        }

        Console.WriteLine("\n===== Side-effects demonstration =====");

        var smallData = GenerateData(200_000);
        int unsafeCounter = 0;
        try
        {

            Stopwatch sw = Stopwatch.StartNew();
            foreach (var x in smallData)
            {
                if (IsHeavyPredicate(x))
                    unsafeCounter++; 
            }
            sw.Stop();
            Console.WriteLine($"Sequential count (correct): {unsafeCounter}  (time {sw.Elapsed.TotalMilliseconds} ms)");

            unsafeCounter = 0;
            sw.Restart();

            smallData.AsParallel().ForAll(x =>
            {
                if (IsHeavyPredicate(x))
                {
                    
                    unsafeCounter++;
                }
            });
            sw.Stop();
            Console.WriteLine($"Parallel unsafe count (likely incorrect): {unsafeCounter}  (time {sw.Elapsed.TotalMilliseconds} ms)");
            Console.WriteLine("Порівняй значення — вони мають різнитися через race condition (якщо CPU багатоядерний).");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error during unsafe demo: " + ex.Message);
        }
        int lockedCounter = 0;
        object locker = new object();
        Stopwatch swLock = Stopwatch.StartNew();
        smallData.AsParallel().ForAll(x =>
        {
            if (IsHeavyPredicate(x))
            {
                lock (locker)
                {
                    lockedCounter++;
                }
            }
        });
        swLock.Stop();
        Console.WriteLine($"Parallel count with lock (correct): {lockedCounter}  (time {swLock.ElapsedMilliseconds} ms)");
        int interlockedCounter = 0;
        Stopwatch swInter = Stopwatch.StartNew();
        smallData.AsParallel().ForAll(x =>
        {
            if (IsHeavyPredicate(x))
            {
                Interlocked.Increment(ref interlockedCounter);
            }
        });
        swInter.Stop();
        Console.WriteLine($"Parallel count with Interlocked (correct): {interlockedCounter}  (time {swInter.ElapsedMilliseconds} ms)");
        Stopwatch swSafe = Stopwatch.StartNew();
        var properCount = smallData.AsParallel().Count(x => IsHeavyPredicate(x));
        swSafe.Stop();
        Console.WriteLine($"Proper PLINQ Count (no side-effects): {properCount}  (time {swSafe.ElapsedMilliseconds} ms)");

        Console.WriteLine("\n===== Demo finished =====");
    }
    static List<double> GenerateData(int size)
    {
        Console.WriteLine($"Generating data: {size:N0} elements...");
        var rnd = new Random(12345);
        var list = new List<double>(size);
        for (int i = 0; i < size; i++)
        {
            list.Add(rnd.NextDouble() * 1000.0 + 1.0);
        }
        Console.WriteLine("Data generation finished.");
        return list;
    }
    static double HeavyOperation(double x, int workFactor)
    {
        double acc = x;
        for (int i = 0; i < workFactor; i++)
        {
            acc = Math.Sqrt(acc) + Math.Pow(acc, 0.75) * Math.Sin(acc + i);
            if (double.IsNaN(acc) || double.IsInfinity(acc) || Math.Abs(acc) > 1e12)
                acc = x;
        }
        return acc;
    }
    static bool IsHeavyPredicate(double x)
    {

        var val = HeavyOperation(x, 8); 
        return (val % 2) > 0.5;
    }
}
