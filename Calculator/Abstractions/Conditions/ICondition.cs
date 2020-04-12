namespace Calculator.Abstractions.Conditions
{
    public interface ICondition<in T>
    {
        bool Evaluate(T comparingValue);
    }
}
