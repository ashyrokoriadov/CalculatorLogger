namespace Calculator.Abstractions.Conditions
{
    public interface IOrderedCondition<out TOrderBy, in T> : ICondition<T>
    {
        TOrderBy Order { get; }
    }
}
