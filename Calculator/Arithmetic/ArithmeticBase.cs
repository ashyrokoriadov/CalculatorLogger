using System;
using System.Collections.Generic;
using System.Text;
using Calculator.Abstractions.Validators;
using Calculator.Models;

namespace Calculator.Arithmetic
{
    public abstract class ArithmeticBase
    {
        protected readonly IArithmeticValidator<CalculatorValue> Validator;

        protected ArithmeticBase(IArithmeticValidator<CalculatorValue> validator)
        {
            Validator = validator;
        }

        protected bool ValidateInput(CalculatorValue valueX, CalculatorValue valueY)
            => Validator.ValidateIsNull(valueX) || Validator.ValidateIsNull(valueY);

        protected bool ValidateInput(IEnumerable<CalculatorValue> values)
            => Validator.ValidateIsEnumerableEmpty(values);
    }
}
