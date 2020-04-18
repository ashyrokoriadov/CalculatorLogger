using System.Collections.Generic;
using LoggingCalculator.AbstractionsAndModels.Models;

namespace LoggingCalculator.AbstractionsAndModels.Payloads
{
    public class BandResolverPayload : ICorrelated
    {
        public Dictionary<decimal, decimal> Bands { get; set; }
        public CalculatorValue ValueToResolve { get; set; }
        public string CorrelationId { get; set; }
    }
}
