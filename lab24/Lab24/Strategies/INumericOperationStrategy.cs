namespace Lab24.Strategies
{
    public interface INumericOperationStrategy
    {
        double Execute(double value);
        string Name { get; }
    }
}