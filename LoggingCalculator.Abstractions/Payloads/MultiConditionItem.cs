using LoggingCalculator.AbstractionsAndModels.Enums;
using LoggingCalculator.AbstractionsAndModels.Models;

namespace LoggingCalculator.AbstractionsAndModels.Payloads
{
    public class MultiConditionItem
    {
        public CalculatorValue ReferenceValue { get; set; }
        public LogicalOperator Operator { get; set; }
    }
}
