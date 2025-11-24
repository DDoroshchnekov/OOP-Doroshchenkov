using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab6
{
    // 🔷 Власний делегат для обробки суми замовлення
    public delegate double OrderProcessor(double amount);

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Lab 6: Delegates, Lambdas, LINQ ===\n");

            // 🔹 Створюємо колекцію замовлень
            List<Order> orders = new()
            {
                new Order(1, 1500, "Completed"),
                new Order(2, 520, "Pending"),
                new Order(3, 3200, "Completed"),
                new Order(4, 800, "Pending"),
                new Order(5, 2100, "Completed"),
            };

            // -------------------------------------------------------------
            // 1) Власний делегат + анонімний метод
            // -------------------------------------------------------------
            OrderProcessor addTax = delegate (double amount)
            {
                return amount * 1.05; // 5% податку
            };

            Console.WriteLine("Власний делегат (анонімний метод):");
            Console.WriteLine($"Сума з податком: {addTax(1000)}\n");

            // -------------------------------------------------------------
            // 2) Лямбда-вираз на власному делегаті
            // -------------------------------------------------------------
            OrderProcessor discount = amount => amount * 0.9;

            Console.WriteLine("Власний делегат (лямбда):");
            Console.WriteLine($"Сума зі знижкою: {discount(1000)}\n");

            // -------------------------------------------------------------
            // 3) Використання вбудованих делегатів
            // -------------------------------------------------------------

            // Predicate<Order> — перевірка статусу
            Predicate<Order> isPending = o => o.Status == "Pending";

            // Func<Order, string> — форматований вивід
            Func<Order, string> orderInfo = o => $"{o.Id}: {o.Amount} UAH ({o.Status})";

            // Action<Order> — друк
            Action<Order> printOrder = o => Console.WriteLine(orderInfo(o));

            Console.WriteLine("Pending orders:");
            orders.Where(o => isPending(o)).ToList().ForEach(printOrder);
            Console.WriteLine();

            // -------------------------------------------------------------
            // 4) LINQ + Лямбди
            // -------------------------------------------------------------

            // 🔹 Загальна сума виконаних
            double completedSum = orders
                .Where(o => o.Status == "Completed")
                .Select(o => o.Amount)
                .Sum();

            Console.WriteLine($"Загальна сума Completed: {completedSum}");

            // 🔹 Кількість Pending
            int pendingCount = orders.Count(o => o.Status == "Pending");
            Console.WriteLine($"Кількість Pending: {pendingCount}");

            // 🔹 Сортування за сумою
            var sorted = orders.OrderBy(o => o.Amount);

            Console.WriteLine("\nСортування за сумою:");
            foreach (var o in sorted)
                Console.WriteLine(o);

            // 🔹 Aggregate — приклад
            double totalAmount = orders.Select(o => o.Amount)
                                       .Aggregate((x, y) => x + y);

            Console.WriteLine($"\nСума всіх замовлень (Aggregate): {totalAmount}");
        }
    }
}
