using System.Collections.Generic;
using Autofac.Features.AttributeFilters;
using Calculator.Bands;
using Calculator.Service.Constants;
using LoggingCalculator.AbstractionsAndModels.Bands;
using LoggingCalculator.AbstractionsAndModels.Models;
using LoggingCalculator.AbstractionsAndModels.Payloads;
using LoggingCalculator.AbstractionsAndModels.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BandController : ControllerBase
    {
        private readonly IValidator<Dictionary<decimal, decimal>> _nullBandValidator;
        private readonly IValidator<CalculatorValue> _nullValidator;

        public BandController(
              [KeyFilter("BandDecimalValidator")]IValidator<Dictionary<decimal, decimal>> nullBandValidator
            , [KeyFilter("NullValidator")] IValidator<CalculatorValue> nullValidator)
        {
            _nullBandValidator = nullBandValidator;
            _nullValidator = nullValidator;
        }

        [HttpGet("Excluding")]
        public ActionResult<CalculatorValue> ResolveExcludingBand([FromBody] BandResolverPayload payload)
        {
            if(Validate(payload, out var validationResult))
            {
                return validationResult;
            }

            IBandResolver<CalculatorValue> resolver = new ExcludingBandResolver(payload.Bands);
            var result = resolver.Resolve(payload.ValueToResolve);
            return Ok(result);
        }

        [HttpGet("Including")]
        public ActionResult<CalculatorValue> ResolveIncludingBand([FromBody] BandResolverPayload payload)
        {
            if (Validate(payload, out var validationResult))
            {
                return validationResult;
            }

            IBandResolver<CalculatorValue> resolver = new IncludingBandResolver(payload.Bands);
            var result = resolver.Resolve(payload.ValueToResolve);
            return Ok(result);
        }

        private bool Validate(BandResolverPayload payload, out ActionResult<CalculatorValue> result)
        {
            if (payload == null)
            {
                result = BadRequest(ErrorMessages.NULL_PAYLOAD);
                return true;
            }

            if (_nullBandValidator.Validate(payload.Bands))
            {
                result = BadRequest(ErrorMessages.NULL_BAND);
                return true;
            }

            if (_nullValidator.Validate(payload.ValueToResolve))
            {
                result = BadRequest(string.Format(ErrorMessages.NULL_VALUE_TEMPLATE, nameof(payload.ValueToResolve)));
                return true;
            }

            result = null;
            return false;
        }
    }
}
