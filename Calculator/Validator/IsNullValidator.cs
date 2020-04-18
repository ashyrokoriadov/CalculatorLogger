using LoggingCalculator.AbstractionsAndModels.Models;
using LoggingCalculator.AbstractionsAndModels.Validators;

namespace Calculator.Validator
{
    public class IsNullValidator : IValidator<CalculatorValue>
    {
        public bool Validate(CalculatorValue @object)
        {
            return @object == null;
        }
    }
}
