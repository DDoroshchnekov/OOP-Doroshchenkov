public class InternationalShippingStrategy : IShippingStrategy
{
    public decimal CalculateCost(decimal distance, decimal weight)
    {
        decimal baseCost = distance * 5.0m + weight * 2.0m;
        return baseCost * 1.15m; // + 15% податок
    }
}
