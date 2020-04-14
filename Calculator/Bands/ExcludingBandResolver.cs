using System.Collections.Generic;
using System.Linq;
using LoggingCalculator.AbstractionsAndModels.Models;

namespace Calculator.Bands
{
    public class ExcludingBandResolver : BandResolver
    {
        public ExcludingBandResolver(Dictionary<decimal, decimal> bands) : base( bands)
        {}

        protected override decimal SelectKey(CalculatorValue value)
            => Bands.LastOrDefault(b => b.Key < value.Value).Key;

        protected override bool IsOutOfRange(decimal key, CalculatorValue value) => key >= value.Value;
    }
}
