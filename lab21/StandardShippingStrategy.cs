public class StandardShippingStrategy : IShippingStrategy
{
    public decimal CalculateCost(decimal distance, decimal weight)
    {
        return distance * 1.5m + weight * 0.5m;
    }
}
