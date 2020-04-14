namespace LoggingCalculator.AbstractionsAndModels.Bands
{
    public interface IBandResolver<T>
    {
        T Resolve(T value);
    }
}
