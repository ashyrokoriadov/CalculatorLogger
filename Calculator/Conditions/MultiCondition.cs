using System.Collections.Generic;
using System.Linq;
using LoggingCalculator.AbstractionsAndModels.Conditions;
using LoggingCalculator.AbstractionsAndModels.Enums;
using LoggingCalculator.AbstractionsAndModels.Models;

namespace Calculator.Conditions
{
    public class MultiCondition : ICondition<CalculatorValue>
    {
        public MultiCondition(IEnumerable<ICondition<CalculatorValue>> conditions, BooleanOperator @operator)
        {
            Conditions = conditions.ToList().AsReadOnly();
            Operator = @operator;
        }

        public IReadOnlyCollection<ICondition<CalculatorValue>> Conditions { get; }

        public BooleanOperator Operator { get; }

        public bool Evaluate(CalculatorValue comparingValue)
        {
            var compareResult = Conditions.Select(x => x.Evaluate(comparingValue));

            switch (Operator)
            {
                case BooleanOperator.And:
                    return compareResult.All(x => x);
                case BooleanOperator.Or:
                    return compareResult.Any(x => x);
                case BooleanOperator.Not:
                    return compareResult.All(x => !x);
                default:
                    return false;
            }
        }
    }
}
