using System.Collections.Generic;
using System.Linq;
using LoggingCalculator.AbstractionsAndModels.Aggregations;
using LoggingCalculator.AbstractionsAndModels.Models;

namespace Calculator.Aggregations
{
    public class MinAggregator : AggregationsBase, IMinAggregator<CalculatorValue>
    {
        public MinAggregator()
        {
            FunctionName = nameof(Min);
        }

        public CalculatorValue Min(IEnumerable<CalculatorValue> values) => Calculate(values);

        public CalculatorValue Min(CalculatorValue valueX, CalculatorValue valueY)
        {
            return Min(new[] { valueX, valueY });
        }

        protected override decimal CalculateAggregate(IEnumerable<CalculatorValue> values)
            => values.Min(x => x.Value);
    }
}
