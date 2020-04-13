using Calculator.Abstractions.Conditions;
using Calculator.Models;

namespace Calculator.Conditions
{
    public class OrderedCondition : IOrderedCondition<int, CalculatorValue>
    {
        public ICondition<CalculatorValue> Condition { get; }

        public OrderedCondition(ICondition<CalculatorValue> condition, int order)
        {
            Condition = condition;
            Order = order;
        }

        public int Order { get; }

        public bool Evaluate(CalculatorValue comparingValue) => Condition.Evaluate(comparingValue);
    }
}
