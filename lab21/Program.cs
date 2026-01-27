using System;

class Program
{
    static void Main()
    {
        DeliveryService service = new DeliveryService();

        Console.WriteLine("Enter delivery type (Standard, Express, International, Night):");
        string deliveryType = Console.ReadLine() ?? "Standard"; // null coalescing

        decimal distance = ReadDecimal("Enter distance (km):", 0);
        decimal weight = ReadDecimal("Enter weight (kg):", 0);

        try
        {
            IShippingStrategy strategy = ShippingStrategyFactory.CreateStrategy(deliveryType);
            decimal cost = service.CalculateDeliveryCost(distance, weight, strategy);
            Console.WriteLine($"Delivery cost ({deliveryType}) = {cost:C}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        // Демонстрація OCP: нова стратегія Night
        IShippingStrategy nightStrategy = new NightShippingStrategy();
        decimal nightCost = service.CalculateDeliveryCost(distance, weight, nightStrategy);
        Console.WriteLine($"Night delivery cost = {nightCost:C}");
    }

    static decimal ReadDecimal(string prompt, decimal defaultValue)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                return defaultValue;

            if (decimal.TryParse(input, out decimal value))
                return value;

            Console.WriteLine("Invalid number, please try again.");
        }
    }
}
