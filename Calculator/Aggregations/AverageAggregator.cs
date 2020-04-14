using System.Collections.Generic;
using System.Linq;
using LoggingCalculator.AbstractionsAndModels.Aggregations;
using LoggingCalculator.AbstractionsAndModels.Models;

namespace Calculator.Aggregations
{
    public class AverageAggregator : AggregationsBase, IAverageAggregator<CalculatorValue>
    {
        public AverageAggregator()
        {
            FunctionName = nameof(Average);
        }

        public CalculatorValue Average(IEnumerable<CalculatorValue> values) => Calculate(values);

        public CalculatorValue Average(CalculatorValue valueX, CalculatorValue valueY)
        {
            return Average(new [] {valueX, valueY});
        }

        protected override decimal CalculateAggregate(IEnumerable<CalculatorValue> values) 
            => values.Average(x => x.Value);
    }
}
