namespace LoggingCalculator.AbstractionsAndModels.Converters
{
    public interface IConverter<in TInput, out TResult>
    {
        TResult Convert(TInput @operator);
    }
}
