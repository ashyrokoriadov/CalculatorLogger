namespace LoggingCalculator.AbstractionsAndModels.Validators
{
    public interface IValidator<in T> where T:class
    {
        bool Validate(T @object);
    }
}
