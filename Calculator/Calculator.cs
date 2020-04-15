using System.Collections.Generic;
using LoggingCalculator.AbstractionsAndModels;
using LoggingCalculator.AbstractionsAndModels.Aggregations;
using LoggingCalculator.AbstractionsAndModels.Arithmetic;

namespace Calculator
{
    public class Calculator<T> : ICalculator<T>
    {
        private readonly IArithmeticCalculator<T> _arithmeticCalculator;
        private readonly IAggregationCalculator<T> _aggregationCalculator;

        public Calculator(
            IArithmeticCalculator<T> arithmeticCalculator, 
            IAggregationCalculator<T> aggregationCalculator)
        {
            _arithmeticCalculator = arithmeticCalculator;
            _aggregationCalculator = aggregationCalculator;
        }

        public T Add(T valueX, T valueY)
        {
            return _arithmeticCalculator.Add(valueX, valueY);
        }

        public T Add(IEnumerable<T> values)
        {
            return _arithmeticCalculator.Add(values);
        }

        public T Average(IEnumerable<T> values)
        {
            return _aggregationCalculator.Average(values);
        }

        public T Average(T valueX, T valueY)
        {
            return _aggregationCalculator.Average(valueX, valueY);
        }

        public T Divide(T valueX, T valueY)
        {
            return _arithmeticCalculator.Divide(valueX, valueY);
        }

        public T Divide(IEnumerable<T> values, T dividend)
        {
            return _arithmeticCalculator.Divide(values, dividend);
        }

        public T Max(IEnumerable<T> values)
        {
            return _aggregationCalculator.Max(values);
        }

        public T Max(T valueX, T valueY)
        {
            return _aggregationCalculator.Max(valueX, valueY);
        }

        public T Min(IEnumerable<T> values)
        {
            return _aggregationCalculator.Min(values);
        }

        public T Min(T valueX, T valueY)
        {
            return _aggregationCalculator.Min(valueX, valueY);
        }

        public T Multiply(T valueX, T valueY)
        {
            return _arithmeticCalculator.Multiply(valueX, valueY);
        }

        public T Multiply(IEnumerable<T> values)
        {
            return _arithmeticCalculator.Multiply(values);
        }

        public T Subtract(T valueX, T valueY)
        {
            return _arithmeticCalculator.Subtract(valueX, valueY);
        }

        public T Subtract(IEnumerable<T> values, T minuend)
        {
            return _arithmeticCalculator.Subtract(values, minuend);
        }
    }
}
