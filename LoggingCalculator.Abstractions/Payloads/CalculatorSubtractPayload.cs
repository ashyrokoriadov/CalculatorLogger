using System.Collections.Generic;
using LoggingCalculator.AbstractionsAndModels.Models;

namespace LoggingCalculator.AbstractionsAndModels.Payloads
{
    public class CalculatorSubtractPayload
    {
        public IEnumerable<CalculatorValue> Values { get; set; }
        public CalculatorValue Minuend { get; set; }
    }
}
