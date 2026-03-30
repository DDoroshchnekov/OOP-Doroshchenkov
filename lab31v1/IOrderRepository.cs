namespace lab31v1
{
    public interface IOrderRepository
    {
        Order? GetById(int id);
        void Save(Order order);
        void Delete(int id);
    }
}