using System.Collections.Generic;

namespace LoggingCalculator.AbstractionsAndModels.Payloads
{
    public class SwitchItem
    {
        public int Order { get; set; }
        public IEnumerable<MultiConditionItem> Conditions { get; set; }
        public decimal Value { get; set; }
    }
}
