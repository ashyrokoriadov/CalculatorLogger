using System.Collections.Generic;

namespace Calculator.Abstractions.Arithmetic
{
    public interface ISubtractor<T>
    {
        T Subtract(T valueX, T valueY);
        T Subtract(IEnumerable<T> values, T minuend);
    }
}
