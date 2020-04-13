using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calculator.Abstractions.Bands;
using Calculator.Abstractions.Validators;
using Calculator.Models;

namespace Calculator.Bands
{
    public abstract class BandResolver : IBandResolver<CalculatorValue>
    {
        protected BandResolver(IArithmeticValidator<CalculatorValue> validator, Dictionary<decimal, decimal> bands)
        {
            Validator = validator;
            Bands = new SortedDictionary<decimal, decimal>(bands); ;
        }

        public virtual CalculatorValue Resolve(CalculatorValue value)
        {
            if (Validator.ValidateIsNull(value))
                return CalculatorValue.Empty();

            var resolvedKey = SelectKey(value);

            if (IsOutOfRange(resolvedKey, value))
                return new CalculatorValue(0.0M, ERROR_NAME);

            return Bands.TryGetValue(resolvedKey, out var resolvedValue)
                ? new CalculatorValue(resolvedValue, DEFAULT_NAME)
                : new CalculatorValue(0.0M, ERROR_NAME);
        }

        protected abstract decimal SelectKey(CalculatorValue value);
        protected abstract bool IsOutOfRange(decimal key, CalculatorValue value);

        protected readonly SortedDictionary<decimal, decimal> Bands;
        protected readonly IArithmeticValidator<CalculatorValue> Validator;

        protected const string DEFAULT_NAME = "Band resolution result";
        protected const string ERROR_NAME = "Out of band range result";
    }
}
