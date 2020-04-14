namespace LoggingCalculator.AbstractionsAndModels.Aggregations
{
    public interface IAggregationCalculator<T> : IAverageAggregator<T>, IMinAggregator<T>, IMaxAggregator<T>
    {
    }
}
