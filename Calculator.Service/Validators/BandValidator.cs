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
    public class BandValidator : IBandValidator
    {
        private readonly IValidator<Dictionary<decimal, decimal>> _nullBandValidator;
        private readonly IValidator<CalculatorValue> _nullValidator;

        public BandValidator(
            [KeyFilter("BandDecimalValidator")]IValidator<Dictionary<decimal, decimal>> nullBandValidator
            , [KeyFilter("NullValidator")] IValidator<CalculatorValue> nullValidator)
        {
            _nullBandValidator = nullBandValidator;
            _nullValidator = nullValidator;
        }

        public bool Validate(BandResolverPayload payload, out ActionResult<CalculatorValue> result)
        {
            if (payload == null)
            {
                result = new BadRequestObjectResult(ErrorMessages.NULL_PAYLOAD);
                return true;
            }

            if (_nullBandValidator.Validate(payload.Bands))
            {
                result = new BadRequestObjectResult(ErrorMessages.NULL_BAND);
                return true;
            }

            if (_nullValidator.Validate(payload.ValueToResolve))
            {
                result = new BadRequestObjectResult(string.Format(ErrorMessages.NULL_VALUE_TEMPLATE, nameof(payload.ValueToResolve)));
                return true;
            }

            result = null;
            return false;
        }
    }
}
