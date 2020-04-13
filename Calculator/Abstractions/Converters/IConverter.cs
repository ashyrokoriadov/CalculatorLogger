namespace Calculator.Abstractions.Converters
{
    public interface IConverter<in TInput, out TResult>
    {
        TResult Convert(TInput @operator);
    }
}
