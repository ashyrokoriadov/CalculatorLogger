namespace Calculator.Abstractions.Switch
{
    public interface ISwitch<in T>
    {
        decimal DefaultValue { get; }

        decimal Evaluate(T comparingValue);
    }
}
