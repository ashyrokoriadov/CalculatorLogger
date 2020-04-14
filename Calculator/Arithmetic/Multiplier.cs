using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoggingCalculator.AbstractionsAndModels.Arithmetic;
using LoggingCalculator.AbstractionsAndModels.Models;

namespace Calculator.Arithmetic
{
    public class Multiplier : IMultiplier<CalculatorValue>
    {
        public CalculatorValue Multiply(CalculatorValue valueX, CalculatorValue valueY)
        {
            return new CalculatorValue(valueX.Value * valueY.Value, $"{valueX.Name} * {valueY.Name}");
        }

        public CalculatorValue Multiply(IEnumerable<CalculatorValue> values)
        {
            var valuesAsArray = values?.ToArray() ?? new CalculatorValue[0];

            return Calculate(valuesAsArray);
        }

        private CalculatorValue Calculate(CalculatorValue[] values)
        {
            var resultValue = 1M;
            var sb = new StringBuilder();

            foreach (var t in values)
            {
                resultValue *= t.Value;
                sb.Append($"{t.Name} * ");
            }

            var resultName = sb.ToString();

            return new CalculatorValue(resultValue, resultName.Substring(0, resultName.Length - 3));
        }
    }
}
