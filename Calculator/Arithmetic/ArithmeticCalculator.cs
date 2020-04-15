using System.Collections.Generic;
using LoggingCalculator.AbstractionsAndModels.Arithmetic;

namespace Calculator.Arithmetic
{
    public class ArithmeticCalculator<T> : IArithmeticCalculator<T>
    {
        private readonly IAdder<T> _adder;
        private readonly ISubtractor<T> _subtractor;
        private readonly IMultiplier<T> _multiplier;
        private readonly IDivider<T> _divider;

        public ArithmeticCalculator(IAdder<T> adder, ISubtractor<T> subtractor, IMultiplier<T> multiplier, IDivider<T> divider)
        {
            _adder = adder;
            _subtractor = subtractor;
            _multiplier = multiplier;
            _divider = divider;
        }

        public T Add(T valueX, T valueY)
        {
            return _adder.Add(valueX, valueY);
        }

        public T Add(IEnumerable<T> values)
        {
            return _adder.Add(values);
        }

        public T Divide(T valueX, T valueY)
        {
            return _divider.Divide(valueX, valueY);
        }

        public T Divide(IEnumerable<T> values, T dividend)
        {
            return _divider.Divide(values, dividend);
        }

        public T Multiply(T valueX, T valueY)
        {
            return _multiplier.Multiply(valueX, valueY);
        }

        public T Multiply(IEnumerable<T> values)
        {
            return _multiplier.Multiply(values);
        }

        public T Subtract(T valueX, T valueY)
        {
            return _subtractor.Subtract(valueX, valueY);
        }

        public T Subtract(IEnumerable<T> values, T minuend)
        {
            return _subtractor.Subtract(values, minuend);
        }
    }
}
