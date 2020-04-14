using System.Collections.Generic;

namespace LoggingCalculator.AbstractionsAndModels.Arithmetic
{
    public interface IAdder<T>
    {
        T Add(T valueX, T valueY);
        T Add(IEnumerable<T> values);
    }
}
