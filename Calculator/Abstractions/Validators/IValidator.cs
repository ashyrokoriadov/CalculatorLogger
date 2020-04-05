namespace Calculator.Abstractions.Validators
{
    public interface IValidator<in T> where T: class
    {
        bool Validate(T value);
    }
}
