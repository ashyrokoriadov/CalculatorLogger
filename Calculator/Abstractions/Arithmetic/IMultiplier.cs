using System.Collections.Generic;

namespace Calculator.Abstractions.Arithmetic
{
    public interface IMultiplier<T>
    {
        T Multiply(T valueX, T valueY);
        T Multiply(IEnumerable<T> values);
    }
}
