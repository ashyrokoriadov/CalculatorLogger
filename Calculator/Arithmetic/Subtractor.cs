using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoggingCalculator.AbstractionsAndModels.Arithmetic;
using LoggingCalculator.AbstractionsAndModels.Models;

namespace Calculator.Arithmetic
{
    public class Subtractor : ISubtractor<CalculatorValue>
    {
        public CalculatorValue Subtract(CalculatorValue valueX, CalculatorValue valueY)
        {
            return new CalculatorValue(valueX.Value - valueY.Value, $"{valueX.Name} - {valueY.Name}");
        }

        public CalculatorValue Subtract(IEnumerable<CalculatorValue> values, CalculatorValue minuend)
        {
            var valuesAsArray = values?.ToArray() ?? new CalculatorValue[0];

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
