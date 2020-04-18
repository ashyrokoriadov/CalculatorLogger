using System.Collections.Generic;
using LoggingCalculator.AbstractionsAndModels.Validators;

namespace Calculator.Validator
{
    public class IsNullBandValidator : IValidator<Dictionary<decimal, decimal>>
    {
        public bool Validate(Dictionary<decimal, decimal> @object)
        {
            return @object == null || @object.Count == 0;
        }
    }
}
