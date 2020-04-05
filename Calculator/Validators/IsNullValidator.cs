using Calculator.Abstractions.Validators;

namespace Calculator.Validators
{
    public class IsNullValidator<T> : IValidator<T> where T : class
    {
        public bool Validate(T value) => value == null;
    }
}
