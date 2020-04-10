namespace Calculator.Abstractions.Bands
{
    public interface IBandResolver<T>
    {
        T Resolve(T value);
    }
}
