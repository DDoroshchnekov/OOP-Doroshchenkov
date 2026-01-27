using System;
using System.Collections.Generic;

public static class ShippingStrategyFactory
{
    private static readonly Dictionary<string, IShippingStrategy> strategies = new()
    {
        { "Standard", new StandardShippingStrategy() },
        { "Express", new ExpressShippingStrategy() },
        { "International", new InternationalShippingStrategy() },
        { "Night", new NightShippingStrategy() } // нова стратегія
    };

    public static IShippingStrategy CreateStrategy(string deliveryType)
    {
        if (strategies.ContainsKey(deliveryType))
            return strategies[deliveryType];

        throw new ArgumentException("Unknown delivery type");
    }
}
