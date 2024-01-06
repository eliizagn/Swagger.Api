using App.Metrics;
using App.Metrics.Counter;

namespace CosmeticsShop.API
{
    public static class MetricsRegister
    {
        public static CounterOptions CreateCosmeticsProduct => new CounterOptions
        {
            Name = "Created Product",
            Context = "CosmeticsApi",
            MeasurementUnit = Unit.Calls
        };
        public static CounterOptions FindCosmeticProduct => new CounterOptions
        {
            Name = "Search Product",
            Context = "CosmeticsApi",
            MeasurementUnit = Unit.Calls
        };
    }

}
