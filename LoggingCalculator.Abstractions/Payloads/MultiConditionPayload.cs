using System.Collections.Generic;
using LoggingCalculator.AbstractionsAndModels.Enums;
using LoggingCalculator.AbstractionsAndModels.Models;

namespace LoggingCalculator.AbstractionsAndModels.Payloads
{
    public class MultiConditionPayload
    {
        public IEnumerable<MultiConditionItem> ReferenceData { get; set; }
        public BooleanOperator Operator { get; set; }
        public CalculatorValue ComparingValue { get; set; }
        public string CorrelationId { get; set; }

    }
}
