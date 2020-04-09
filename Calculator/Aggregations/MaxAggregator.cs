using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calculator.Abstractions.Aggregations;
using Calculator.Abstractions.Validators;
using Calculator.Models;

namespace Calculator.Aggregations
{
    public class MaxAggregator : AggregationsBase, IMaxAggregator<CalculatorValue>
    {
        public MaxAggregator(IArithmeticValidator<CalculatorValue> validator)
            : base(validator)
        {
            FunctionName = nameof(Max);
        }

        public CalculatorValue Max(IEnumerable<CalculatorValue> values) => Calculate(values);

        public CalculatorValue Max(CalculatorValue valueX, CalculatorValue valueY)
        {
            if (ValidateInput(valueX, valueY))
                return CalculatorValue.Empty();

            return Max(new[] { valueX, valueY });
        }

        protected override decimal CalculateAggregate(IEnumerable<CalculatorValue> values)
            => values.Max(x => x.Value);
    }
}
