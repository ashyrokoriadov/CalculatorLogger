using LoggingCalculator.AbstractionsAndModels.Enums;
using LoggingCalculator.AbstractionsAndModels.Models;

namespace LoggingCalculator.AbstractionsAndModels.Payloads
{
    public class ConditionPayload : ICorrelated
    {
        public CalculatorValue ReferenceValue { get; set; }
        public CalculatorValue ComparingValue { get; set; }
        public LogicalOperator Operator { get; set; }
        public string CorrelationId { get; set; }
    }
}
