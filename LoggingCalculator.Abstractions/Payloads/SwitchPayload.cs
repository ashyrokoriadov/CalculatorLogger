using System.Collections.Generic;
using LoggingCalculator.AbstractionsAndModels.Models;

namespace LoggingCalculator.AbstractionsAndModels.Payloads
{
    public class SwitchPayload
    {
        public IEnumerable<SwitchItem> ReferenceData { get; set; }
        public CalculatorValue ComparingValue { get; set; }
        public decimal DefaultValue { get; set; }
        public string CorrelationId { get; set; }
    }
}
