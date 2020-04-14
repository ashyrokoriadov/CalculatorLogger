using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoggingCalculator.AbstractionsAndModels.Arithmetic;
using LoggingCalculator.AbstractionsAndModels.Models;

namespace Calculator.Arithmetic
{
    public class Divider : IDivider<CalculatorValue>
    {
        public CalculatorValue Divide(CalculatorValue valueX, CalculatorValue valueY)
        {
           return new CalculatorValue(valueX.Value / valueY.Value, $"{valueX.Name} / {valueY.Name}");
        }

        public CalculatorValue Divide(IEnumerable<CalculatorValue> values, CalculatorValue dividend)
        {
            var valuesAsArray = values?.ToArray() ?? new CalculatorValue[0];

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
