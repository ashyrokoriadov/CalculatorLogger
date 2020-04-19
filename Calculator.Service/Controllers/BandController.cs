using Calculator.Bands;
using Calculator.Service.Interfaces;
using LoggingCalculator.AbstractionsAndModels.Bands;
using LoggingCalculator.AbstractionsAndModels.Models;
using LoggingCalculator.AbstractionsAndModels.Payloads;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BandController : ControllerBase
    {
        private readonly IBandValidator _bandValidator;

        public BandController(IBandValidator bandValidator)
        {
            _bandValidator = bandValidator;
        }

        [HttpGet("Excluding")]
        public ActionResult<CalculatorValue> ResolveExcludingBand([FromBody] BandResolverPayload payload)
        {
            if(_bandValidator.Validate(payload, out var validationResult))
            {
                return validationResult;
            }

            IBandResolver<CalculatorValue> resolver = new ExcludingBandResolver(payload.Bands);
            var result = resolver.Resolve(payload.ValueToResolve);
            result.CorrelationId = payload.CorrelationId;
            return Ok(result);
        }

        [HttpGet("Including")]
        public ActionResult<CalculatorValue> ResolveIncludingBand([FromBody] BandResolverPayload payload)
        {
            if (_bandValidator.Validate(payload, out var validationResult))
            {
                return validationResult;
            }

            IBandResolver<CalculatorValue> resolver = new IncludingBandResolver(payload.Bands);
            var result = resolver.Resolve(payload.ValueToResolve);
            result.CorrelationId = payload.CorrelationId;
            return Ok(result);
        }       
    }
}
