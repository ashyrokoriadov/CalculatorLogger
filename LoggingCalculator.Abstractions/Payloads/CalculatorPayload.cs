using LoggingCalculator.AbstractionsAndModels.Models;

namespace LoggingCalculator.AbstractionsAndModels.Payloads
{
    public class CalculatorPayload : ICorrelated
    {
        public CalculatorValue ValueX { get; set; }
        public CalculatorValue ValueY { get; set; }
        public string CorrelationId { get; set; }
    }
}
