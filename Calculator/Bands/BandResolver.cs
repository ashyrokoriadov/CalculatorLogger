using System.Collections.Generic;
using LoggingCalculator.AbstractionsAndModels.Bands;
using LoggingCalculator.AbstractionsAndModels.Models;

namespace Calculator.Bands
{
    public abstract class BandResolver : IBandResolver<CalculatorValue>
    {
        protected BandResolver(Dictionary<decimal, decimal> bands)
        {
            Bands = new SortedDictionary<decimal, decimal>(bands); ;
        }

        public virtual CalculatorValue Resolve(CalculatorValue value)
        {
            var resolvedKey = SelectKey(value);

            if (IsOutOfRange(resolvedKey, value))
                return new CalculatorValue(0.0M, ERROR_NAME);

            return Bands.TryGetValue(resolvedKey, out var resolvedValue)
                ? new CalculatorValue(resolvedValue, DEFAULT_NAME)
                : new CalculatorValue(0.0M, ERROR_NAME);
        }

        protected abstract decimal SelectKey(CalculatorValue value);
        protected abstract bool IsOutOfRange(decimal key, CalculatorValue value);

        protected readonly SortedDictionary<decimal, decimal> Bands;

        protected const string DEFAULT_NAME = "Band resolution result";
        protected const string ERROR_NAME = "Out of band range result";
    }
}
