using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Abstractions.Validators
{
    public interface IArithmeticValidator<in T> where T : class
    {
        bool ValidateIsNull(T value);

        bool ValidateIsEnumerableEmpty(IEnumerable<T> value);
    }
}
