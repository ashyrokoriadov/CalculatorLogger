using LoggingCalculator.AbstractionsAndModels.Models;
using LoggingCalculator.AbstractionsAndModels.Validators;

namespace Calculator.Validator
{
    public class IsZeroValidator : IValidator<CalculatorValue>
    {
        public bool Validate(CalculatorValue @object)
        {
            return @object.Value == 0M;
        }
    }
}
