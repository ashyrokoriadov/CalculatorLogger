using System.Collections.Generic;

namespace LoggingCalculator.AbstractionsAndModels.Arithmetic
{
    public interface IMultiplier<T>
    {
        T Multiply(T valueX, T valueY);
        T Multiply(IEnumerable<T> values);
    }
}
