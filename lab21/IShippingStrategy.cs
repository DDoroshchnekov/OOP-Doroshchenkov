public interface IShippingStrategy
{
    decimal CalculateCost(decimal distance, decimal weight);
}
