using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calculator.Abstractions.Arithmetic;
using Calculator.Abstractions.Validators;
using Calculator.Models;

namespace Calculator.Arithmetic
{
    public class Subtractor : ArithmeticBase, ISubtractor<CalculatorValue>
    {
        public Subtractor(IArithmeticValidator<CalculatorValue> validator)
            : base(validator)
        { }

        public CalculatorValue Subtract(CalculatorValue valueX, CalculatorValue valueY)
        {
            if (ValidateInput(valueX, valueY))
                return CalculatorValue.Empty();

            return new CalculatorValue(valueX.Value - valueY.Value, $"{valueX.Name} - {valueY.Name}");
        }

        public CalculatorValue Subtract(IEnumerable<CalculatorValue> values, CalculatorValue minuend)
        {
            var valuesAsArray = values?.ToArray() ?? new CalculatorValue[0];

            if (ValidateInput(valuesAsArray) || Validator.ValidateIsNull(minuend))
                return CalculatorValue.Empty();

            return Calculate(valuesAsArray, minuend);
        }

        private CalculatorValue Calculate(CalculatorValue[] values, CalculatorValue minuend)
        {
            var resultValue = minuend.Value;
            var sb = new StringBuilder();
            sb.Append($"{minuend.Name}");

            foreach (var t in values)
            {
                resultValue -= t.Value;
                sb.Append($" - {t.Name}");
            }

            return new CalculatorValue(resultValue, sb.ToString());
        }
    }
}
