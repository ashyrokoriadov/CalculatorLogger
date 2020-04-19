using Autofac.Features.AttributeFilters;
using Calculator.Service.Constants;
using Calculator.Service.Interfaces;
using LoggingCalculator.AbstractionsAndModels.Models;
using LoggingCalculator.AbstractionsAndModels.Payloads;
using LoggingCalculator.AbstractionsAndModels.Validators;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Calculator.Service.Validators
{
    public class CalculatorValidator : ICalculatorValidator
    {
        private readonly IValidator<CalculatorValue> _nullValidator;
        private readonly IValidator<CalculatorValue> _zeroValidator;

        public CalculatorValidator(
              [KeyFilter("NullValidator")] IValidator<CalculatorValue> nullValidator
            , [KeyFilter("ZeroValidator")] IValidator<CalculatorValue> zeroValidator)
        {
            _nullValidator = nullValidator;
            _zeroValidator = zeroValidator;
        }

        public bool ValidateNull(CalculatorPayload payload, out ActionResult<CalculatorValue> result)
        {
            if (_nullValidator.Validate(payload.ValueX))
            {
                result = new BadRequestObjectResult(string.Format(ErrorMessages.NULL_VALUE_TEMPLATE, nameof(payload.ValueX)));
                return true;
            }

            if (_nullValidator.Validate(payload.ValueY))
            {
                result = new BadRequestObjectResult(string.Format(ErrorMessages.NULL_VALUE_TEMPLATE, nameof(payload.ValueY)));
                return true;
            }

            result = null;
            return false;
        }

        public bool ValidateNull(CalculatorSubtractPayload payload, out ActionResult<CalculatorValue> result)
        {
            if (_nullValidator.Validate(payload.Minuend))
            {
                result = new BadRequestObjectResult(string.Format(ErrorMessages.NULL_VALUE_TEMPLATE, nameof(payload.Minuend)));
                return true;
            }

            foreach (var vaues in payload.Values)
            {
                if (_nullValidator.Validate(vaues))
                {
                    result = new BadRequestObjectResult(ErrorMessages.NULL_VALUE);
                    return true;
                }
            }

            result = null;
            return false;
        }

        public bool ValidateNull(CalculatorDividePayload payload, out ActionResult<CalculatorValue> result)
        {
            if (_nullValidator.Validate(payload.Dividend))
            {
                result = new BadRequestObjectResult(string.Format(ErrorMessages.NULL_VALUE_TEMPLATE, nameof(payload.Dividend)));
                return true;
            }

            foreach (var vaues in payload.Values)
            {
                if (_nullValidator.Validate(vaues))
                {
                    result = new BadRequestObjectResult(ErrorMessages.NULL_VALUE);
                    return true;
                }
            }

            result = null;
            return false;
        }

        public bool ValidateNull(CalculatorEnumerablePayload values, out ActionResult<CalculatorValue> result)
        {
            foreach (var payload in values.Values)
            {
                if (_nullValidator.Validate(payload))
                {
                    result = new BadRequestObjectResult(ErrorMessages.NULL_VALUE);
                    return true;
                }
            }

            result = null;
            return false;
        }

        public bool ValidateZero(CalculatorPayload payload, out ActionResult<CalculatorValue> result)
        {
            if (_zeroValidator.Validate(payload.ValueY))
            {
                result = new BadRequestObjectResult(string.Format(ErrorMessages.NULL_DIVISION_TEMPLATE, nameof(payload.ValueY)));
                return true;
            }

            result = null;
            return false;
        }

        public bool ValidateZero(CalculatorEnumerablePayload values, out ActionResult<CalculatorValue> result)
        {
            foreach (var payload in values.Values)
            {
                if (_zeroValidator.Validate(payload))
                {
                    result = new BadRequestObjectResult(ErrorMessages.NULL_DIVISION);
                    return true;
                }
            }

            result = null;
            return false;
        }

        public bool ValidateZero(CalculatorDividePayload payload, out ActionResult<CalculatorValue> result)
        {
            foreach (var value in payload.Values)
            {
                if (_zeroValidator.Validate(value))
                {
                    result = new BadRequestObjectResult(ErrorMessages.NULL_DIVISION);
                    return true;
                }
            }

            result = null;
            return false;
        }
    }
}
