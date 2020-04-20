using Autofac.Features.AttributeFilters;
using Calculator.Conditions;
using LoggingCalculator.AbstractionsAndModels.Conditions;
using LoggingCalculator.AbstractionsAndModels.Models;
using LoggingCalculator.AbstractionsAndModels.Payloads;
using LoggingCalculator.AbstractionsAndModels.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConditionController : ControllerBase
    {
        private IValidator<CalculatorValue> _nullValidator;

        public ConditionController(
            [KeyFilter("NullValidator")] IValidator<CalculatorValue> nullValidator)
        {
            _nullValidator = nullValidator;
        }

        [HttpGet("Single")]
        public ActionResult<CalculatorValue> ResolveExcludingBand([FromBody] ConditionPayload payload)
        {
            if (payload == null)
                return BadRequest($"{nameof(ConditionPayload)} cannot be null.");

            if(_nullValidator.Validate(payload.ReferenceValue))
                return BadRequest($"{nameof(payload.ReferenceValue)} cannot be null.");

            if (_nullValidator.Validate(payload.ComparingValue))
                return BadRequest($"{nameof(payload.ComparingValue)} cannot be null.");

            ICondition<CalculatorValue> resolver = new Condition(payload.ReferenceValue, payload.Operator);
            var result = resolver.Evaluate(payload.ComparingValue);
            return Ok(new ConditionResult()
                {
                    Result = result,
                    CorrelationId = payload.CorrelationId
                }
            );
        }   
    }
}
