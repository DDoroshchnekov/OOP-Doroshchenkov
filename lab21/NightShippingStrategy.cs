public class NightShippingStrategy : IShippingStrategy
{
    public decimal CalculateCost(decimal distance, decimal weight)
    {
        // базовий тариф як Standard + націнка 20
        return distance * 1.5m + weight * 0.5m + 20m;
    }
}
