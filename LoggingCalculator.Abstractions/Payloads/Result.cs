namespace LoggingCalculator.AbstractionsAndModels.Payloads
{
    public class Result<T> : ICorrelated
    {
        public T Value { get; set; }
        public string CorrelationId { get; set; }
    }
}
