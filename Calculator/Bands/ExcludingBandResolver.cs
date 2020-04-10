using System.Collections.Generic;
using System.Linq;
using Calculator.Abstractions.Validators;
using Calculator.Models;

namespace Calculator.Bands
{
    public class ExcludingBandResolver : BandResolver
    {
        public ExcludingBandResolver(
            IArithmeticValidator<CalculatorValue> validator
            , Dictionary<decimal, decimal> bands) : base(validator, bands)
        {}

        protected override decimal SelectKey(CalculatorValue value)
            => Bands.LastOrDefault(b => b.Key < value.Value).Key;

        protected override bool IsOutOfRange(decimal key, CalculatorValue value) => key >= value.Value;
    }
}
