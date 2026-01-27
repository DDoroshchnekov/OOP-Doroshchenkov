public class DeliveryService
{
    public decimal CalculateDeliveryCost(decimal distance, decimal weight, IShippingStrategy strategy)
    {
        return strategy.CalculateCost(distance, weight);
    }
}
