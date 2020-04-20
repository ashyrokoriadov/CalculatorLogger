using System;
using System.Collections.Generic;
using System.Text;

namespace LoggingCalculator.AbstractionsAndModels.Payloads
{
    public class ConditionResult : ICorrelated
    {
        public bool Result { get; set; }
        public string CorrelationId { get; set; }
    }
}
