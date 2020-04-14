using System.Collections.Generic;

namespace LoggingCalculator.AbstractionsAndModels.Aggregations
{
    public interface IMaxAggregator<T>
    {
        T Max(IEnumerable<T> values);

        T Max(T valueX, T valueY);
    }
}
