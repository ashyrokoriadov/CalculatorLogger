using System.Collections.Generic;
using System.Linq;
using Calculator.Abstractions.Aggregations;
using Calculator.Abstractions.Validators;
using Calculator.Models;

namespace Calculator.Aggregations
{
    public class AverageAggregator : AggregationsBase, IAverageAggregator<CalculatorValue>
    {
        public AverageAggregator(IArithmeticValidator<CalculatorValue> validator)
            : base(validator)
        {
            FunctionName = nameof(Average);
        }

        public CalculatorValue Average(IEnumerable<CalculatorValue> values) => Calculate(values);

        public CalculatorValue Average(CalculatorValue valueX, CalculatorValue valueY)
        {
            if (ValidateInput(valueX, valueY))
                return CalculatorValue.Empty();

            return Average(new [] {valueX, valueY});
        }

        protected override decimal CalculateAggregate(IEnumerable<CalculatorValue> values) 
            => values.Average(x => x.Value);
    }
}
