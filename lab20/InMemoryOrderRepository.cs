using System.Collections.Generic;

public class InMemoryOrderRepository : IOrderRepository
{
    private readonly List<Order> _orders = new();

    public void Save(Order order)
    {
        _orders.Add(order);
        Console.WriteLine("Order saved in memory.");
    }

    public Order? GetById(int id)
    {
        return _orders.Find(o => o.Id == id);
    }
}
