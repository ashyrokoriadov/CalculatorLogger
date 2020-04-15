using System.Collections.Generic;
using LoggingCalculator.AbstractionsAndModels.Aggregations;

namespace Calculator.Aggregations
{
    public class AggregationCalculator<T> : IAggregationCalculator<T>
    {
        private readonly IAverageAggregator<T> _average;
        private readonly IMinAggregator<T> _min;
        private readonly IMaxAggregator<T> _max;

        public AggregationCalculator(IAverageAggregator<T> average, IMinAggregator<T> min, IMaxAggregator<T> max)
        {
            _average = average;
            _min = min;
            _max = max;
        }

        public T Average(IEnumerable<T> values)
        {
            return _average.Average(values);
        }

        public T Average(T valueX, T valueY)
        {
            return _average.Average(valueX, valueY);
        }

        public T Max(IEnumerable<T> values)
        {
            return _max.Max(values);
        }

        public T Max(T valueX, T valueY)
        {
            return _max.Max(valueX, valueY);
        }

        public T Min(IEnumerable<T> values)
        {
            return _min.Min(values);
        }

        public T Min(T valueX, T valueY)
        {
            return _min.Min(valueX, valueY);
        }
    }
}
