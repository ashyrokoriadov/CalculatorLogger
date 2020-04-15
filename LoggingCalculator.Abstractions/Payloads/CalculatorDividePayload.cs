using System.Collections.Generic;
using LoggingCalculator.AbstractionsAndModels.Models;

namespace LoggingCalculator.AbstractionsAndModels.Payloads
{
    public class CalculatorDividePayload
    {
        public IEnumerable<CalculatorValue> Values { get; set; }
        public CalculatorValue Dividend { get; set; }
    }
}
