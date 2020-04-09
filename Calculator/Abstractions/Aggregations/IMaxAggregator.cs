using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Abstractions.Aggregations
{
    public interface IMaxAggregator<T>
    {
        T Max(IEnumerable<T> values);

        T Max(T valueX, T valueY);
    }
}
