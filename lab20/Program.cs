class Program
{
    static void Main()
    {
        IOrderValidator validator = new OrderValidator();
        IOrderRepository repository = new InMemoryOrderRepository();
        IEmailService emailService = new ConsoleEmailService();

        OrderService service = new OrderService(
            validator,
            repository,
            emailService
        );

        Order validOrder = new Order(1, "Denys", 100);
        Order invalidOrder = new Order(2, "Ivan", -50);

        Console.WriteLine("\n--- VALID ORDER ---");
        service.ProcessOrder(validOrder);

        Console.WriteLine("\n--- INVALID ORDER ---");
        service.ProcessOrder(invalidOrder);
    }
}
