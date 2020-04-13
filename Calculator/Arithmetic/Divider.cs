using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calculator.Abstractions.Arithmetic;
using Calculator.Abstractions.Validators;
using Calculator.Models;

namespace Calculator.Arithmetic
{
    public class Divider : ArithmeticBase, IDivider<CalculatorValue>
    {
        public Divider(IArithmeticValidator<CalculatorValue> validator) : base(validator)
        {
        }

        public CalculatorValue Divide(CalculatorValue valueX, CalculatorValue valueY)
        {
            if (ValidateInput(valueX, valueY))
                return CalculatorValue.Empty();

            return new CalculatorValue(valueX.Value / valueY.Value, $"{valueX.Name} / {valueY.Name}");
        }

        public CalculatorValue Divide(IEnumerable<CalculatorValue> values, CalculatorValue dividend)
        {
            var valuesAsArray = values?.ToArray() ?? new CalculatorValue[0];

            if (ValidateInput(valuesAsArray) || Validator.ValidateIsNull(dividend))
                return CalculatorValue.Empty();

            return Calculate(valuesAsArray, dividend);
        }

        private CalculatorValue Calculate(CalculatorValue[] values, CalculatorValue dividend)
        {
            var resultValue = dividend.Value;
            var sb = new StringBuilder();
            sb.Append($"{dividend.Name}");

            foreach (var t in values)
            {
                resultValue /= t.Value;
                sb.Append($" / {t.Name}");
            }

            return new CalculatorValue(resultValue, sb.ToString());
        }
    }
}
