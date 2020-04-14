using System;
using System.Collections.Generic;
using System.Linq;
using LoggingCalculator.AbstractionsAndModels.Aggregations;
using LoggingCalculator.AbstractionsAndModels.Models;

namespace Calculator.Aggregations
{
    public class MaxAggregator : AggregationsBase, IMaxAggregator<CalculatorValue>
    {
        public MaxAggregator()
        {
            FunctionName = nameof(Max);
        }

        public CalculatorValue Max(IEnumerable<CalculatorValue> values) => Calculate(values);

        public CalculatorValue Max(CalculatorValue valueX, CalculatorValue valueY)
        {
            return Max(new[] { valueX, valueY });
        }

        protected override decimal CalculateAggregate(IEnumerable<CalculatorValue> values)
            => values.Max(x => x.Value);
    }
}
