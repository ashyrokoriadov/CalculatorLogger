using System.Collections.Generic;

namespace Calculator.Abstractions.Arithmetic
{
    public interface IDivider<T>
    {
        T Divide(T valueX, T valueY);
        T Divide(IEnumerable<T> values, T dividend);
    }
}
