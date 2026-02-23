using System;

namespace Lab24.Strategies
{
    public class SquareRootOperationStrategy : INumericOperationStrategy
    {
        public string Name => "SquareRoot";

        public double Execute(double value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Cannot take square root of a negative number.");

            return Math.Sqrt(value);
        }
    }
}