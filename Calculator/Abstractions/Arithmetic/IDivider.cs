using System.Collections.Generic;

namespace Calculator.Abstractions.Arithmetic
{
    public interface IDivider<T>
    {
        T Divide(T valueX, T valueY);
        T Divider(IEnumerable<T> values, T dividend);
    }
}
