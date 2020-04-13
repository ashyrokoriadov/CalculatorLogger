using System.Collections.Generic;
using System.Linq;
using Calculator.Abstractions.Aggregations;
using Calculator.Abstractions.Validators;
using Calculator.Models;

namespace Calculator.Aggregations
{
    public class MinAggregator : AggregationsBase, IMinAggregator<CalculatorValue>
    {
        public MinAggregator(IArithmeticValidator<CalculatorValue> validator)
            : base(validator)
        {
            FunctionName = nameof(Min);
        }

        public CalculatorValue Min(IEnumerable<CalculatorValue> values) => Calculate(values);

        public CalculatorValue Min(CalculatorValue valueX, CalculatorValue valueY)
        {
            if (ValidateInput(valueX, valueY))
                return CalculatorValue.Empty();

            return Min(new[] { valueX, valueY });
        }

        protected override decimal CalculateAggregate(IEnumerable<CalculatorValue> values)
            => values.Min(x => x.Value);
    }
}
