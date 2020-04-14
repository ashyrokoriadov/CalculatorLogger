using System.Collections.Generic;

namespace LoggingCalculator.AbstractionsAndModels.Arithmetic
{
    public interface ISubtractor<T>
    {
        T Subtract(T valueX, T valueY);
        T Subtract(IEnumerable<T> values, T minuend);
    }
}
