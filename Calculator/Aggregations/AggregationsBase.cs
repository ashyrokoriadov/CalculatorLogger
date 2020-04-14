using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoggingCalculator.AbstractionsAndModels.Models;

namespace Calculator.Aggregations
{
    public abstract class AggregationsBase
    {
        protected string FunctionName = string.Empty;

        protected CalculatorValue Calculate(IEnumerable<CalculatorValue> values)
        {
            var valuesAsArray = values?.ToArray() ?? new CalculatorValue[0];

            var resultValue = CalculateAggregate(valuesAsArray);
            var resultName = GenerateName(resultValue, valuesAsArray);

            return new CalculatorValue(resultValue, resultName);
        }

        protected abstract decimal CalculateAggregate(IEnumerable<CalculatorValue> values);

        private string GenerateName(decimal resultValue, IEnumerable<CalculatorValue> values)
        {
            var resultName = new StringBuilder();
            resultName.Append($"{FunctionName}: {resultValue} of values: ");

            foreach (var value in values)
            {
                resultName.Append($"{value.Value}, ");
            }

            resultName.Remove(resultName.Length - 2, 2);
            resultName.Append('.');
            return resultName.ToString();
        }
    }
}
