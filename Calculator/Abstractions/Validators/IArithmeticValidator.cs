using System.Collections.Generic;

namespace Calculator.Abstractions.Validators
{
    public interface IArithmeticValidator<in T> where T : class
    {
        bool ValidateIsNull(T value);

        bool ValidateIsEnumerableEmpty(IEnumerable<T> value);
    }
}
