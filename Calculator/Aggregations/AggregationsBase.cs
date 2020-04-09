using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calculator.Abstractions.Validators;
using Calculator.Models;

namespace Calculator.Aggregations
{
    public abstract class AggregationsBase
    {
        protected readonly IArithmeticValidator<CalculatorValue> Validator;
        protected string FunctionName = string.Empty;

        protected AggregationsBase(IArithmeticValidator<CalculatorValue> validator)
        {
            Validator = validator;
        }

        protected CalculatorValue Calculate(IEnumerable<CalculatorValue> values)
        {
            var valuesAsArray = values?.ToArray() ?? new CalculatorValue[0];

            if (ValidateInput(valuesAsArray))
                return CalculatorValue.Empty();

            var resultValue = CalculateAggregate(valuesAsArray);
            var resultName = GenerateName(resultValue, valuesAsArray);

            return new CalculatorValue(resultValue, resultName);
        }

        protected bool ValidateInput(CalculatorValue valueX, CalculatorValue valueY)
            => Validator.ValidateIsNull(valueX) || Validator.ValidateIsNull(valueY);

        protected bool ValidateInput(IEnumerable<CalculatorValue> values)
            => Validator.ValidateIsEnumerableEmpty(values);

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
