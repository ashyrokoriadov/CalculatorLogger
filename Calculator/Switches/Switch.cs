using System.Collections.Generic;
using System.Linq;
using LoggingCalculator.AbstractionsAndModels.Conditions;
using LoggingCalculator.AbstractionsAndModels.Models;
using LoggingCalculator.AbstractionsAndModels.Switches;

namespace Calculator.Switches
{
    public class Switch<TOrderValue> : ISwitch<CalculatorValue>
    {
        public Switch(IReadOnlyDictionary<IOrderedCondition<TOrderValue, CalculatorValue>, decimal> conditions, decimal defaultValue)
        {
            var orderedDictionary
                = conditions.OrderBy(x => x.Key.Order)
                    .ToDictionary(x=> x.Key, x => x.Value);

            Conditions = orderedDictionary;
            DefaultValue = defaultValue;
        }

        public decimal Evaluate(CalculatorValue comparingValue)
        {
            var conditionKey = Conditions.Keys
                .FirstOrDefault(value => value.Evaluate(comparingValue));

            return conditionKey == null ? DefaultValue : Conditions[conditionKey];
        }

        public IReadOnlyDictionary<IOrderedCondition<TOrderValue, CalculatorValue>, decimal> Conditions { get; }

        public decimal DefaultValue { get; }
    }
}
