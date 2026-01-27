public class OrderProcessor
{
    public void ProcessOrder(Order order)
    {
        Console.WriteLine("Processing order...");

        // Validation
        if (order.TotalAmount <= 0)
        {
            Console.WriteLine("Order validation failed!");
            return;
        }

        // Save to DB (mock)
        Console.WriteLine("Order saved to database.");

        // Send Email (mock)
        Console.WriteLine($"Email sent to {order.CustomerName}");

        // Update status
        order.Status = OrderStatus.Processed;

        Console.WriteLine("Order processed successfully!");
    }
}
