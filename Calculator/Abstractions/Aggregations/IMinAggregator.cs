using System.Collections.Generic;

namespace Calculator.Abstractions.Aggregations
{
    public interface IMinAggregator<T>
    {
        T Min(IEnumerable<T> values);

        T Min(T valueX, T valueY);
    }
}
