using System.Collections.Generic;
using Calculator.Abstractions.Validators;

namespace Calculator.Validators
{
    public class ArithmeticValidator<T> : IArithmeticValidator<T> where T : class
    {
        private readonly IValidator<T> _isNullValidator;
        private readonly IValidator<IEnumerable<T>> _isEnumerableEmptyValidator;

        public ArithmeticValidator(
            IValidator<T> isNullValidator
            , IValidator<IEnumerable<T>> isEnumerableEmptyValidator)
        {
            _isNullValidator = isNullValidator;
            _isEnumerableEmptyValidator = isEnumerableEmptyValidator;
        }

        public bool ValidateIsNull(T value) => _isNullValidator.Validate(value);

        public bool ValidateIsEnumerableEmpty(IEnumerable<T> value) => _isEnumerableEmptyValidator.Validate(value);
    }
}
