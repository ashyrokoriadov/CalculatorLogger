using System.Collections.Generic;

namespace Calculator.Abstractions.Aggregations
{
    public interface IAverageAggregator<T>
    {
        T Average(IEnumerable<T> values);

        T Average(T valueX, T valueY);
    }
}
