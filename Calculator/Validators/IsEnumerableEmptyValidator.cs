using System.Collections;
using System.Linq;
using Calculator.Abstractions.Validators;

namespace Calculator.Validators
{
    public class IsEnumerableEmptyValidator<T> : IValidator<T> where T : class, IEnumerable
    {
        public bool Validate(T value)
        {
            var result = value.Cast<object>().Count();
            return result == 0;
        }
    }
}
