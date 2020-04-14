namespace LoggingCalculator.AbstractionsAndModels.Switches
{
    public interface ISwitch<in T>
    {
        decimal DefaultValue { get; }

        decimal Evaluate(T comparingValue);
    }
}
