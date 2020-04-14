using LoggingCalculator.AbstractionsAndModels.Conditions;
using LoggingCalculator.AbstractionsAndModels.Enums;
using LoggingCalculator.AbstractionsAndModels.Models;

namespace Calculator.Conditions
{
    public class Condition : ICondition<CalculatorValue>
    {
        public Condition(CalculatorValue referenceValue, LogicalOperator @operator)
        {
            ReferenceValue = referenceValue;
            Operator = @operator;
        }

        public CalculatorValue ReferenceValue { get; }

        public LogicalOperator Operator { get; }

        public bool Evaluate(CalculatorValue comparingValue)
        {
            switch (Operator)
            {
                case LogicalOperator.Equal:
                    return comparingValue.Value == ReferenceValue.Value;
                case LogicalOperator.NotEqual:
                    return comparingValue.Value != ReferenceValue.Value;
                case LogicalOperator.Greater:
                    return comparingValue.Value > ReferenceValue.Value;
                case LogicalOperator.GreaterOrEqual:
                    return comparingValue.Value >= ReferenceValue.Value; ;
                case LogicalOperator.Less:
                    return comparingValue.Value < ReferenceValue.Value; ;
                case LogicalOperator.LessOrEqual:
                    return comparingValue.Value <= ReferenceValue.Value; ;
                default:
                    return false;
            }
        }
    }
}
