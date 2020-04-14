namespace LoggingCalculator.AbstractionsAndModels.Conditions
{
    public interface ICondition<in T>
    {
        bool Evaluate(T comparingValue);
    }
}
