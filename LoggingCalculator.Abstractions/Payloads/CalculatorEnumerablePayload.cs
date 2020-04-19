using LoggingCalculator.AbstractionsAndModels.Models;
using System.Collections.Generic;

namespace LoggingCalculator.AbstractionsAndModels.Payloads
{
    public class CalculatorEnumerablePayload : ICorrelated
    {
        public IEnumerable<CalculatorValue> Values { get; set; }
        public string CorrelationId { get; set; }
    }
}
