using System.Collections.Generic;
using System.Linq;
using Autofac.Features.AttributeFilters;
using Calculator.Conditions;
using Calculator.Switches;
using LoggingCalculator.AbstractionsAndModels.Conditions;
using LoggingCalculator.AbstractionsAndModels.Enums;
using LoggingCalculator.AbstractionsAndModels.Models;
using LoggingCalculator.AbstractionsAndModels.Payloads;
using LoggingCalculator.AbstractionsAndModels.Switches;
using LoggingCalculator.AbstractionsAndModels.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SwitchController : ControllerBase
    {
        private readonly IValidator<CalculatorValue> _nullValidator;

        public SwitchController(
            [KeyFilter("NullValidator")] IValidator<CalculatorValue> nullValidator)
        {
            _nullValidator = nullValidator;
        }

        [HttpGet]
        public ActionResult<CalculatorValue> ResolveSwitch([FromBody] SwitchPayload payload)
        {
            if (payload == null)
                return BadRequest($"{nameof(ConditionPayload)} cannot be null.");

            if (payload.ReferenceData == null || !payload.ReferenceData.Any())
                return BadRequest($"{nameof(payload.ReferenceData)} cannot be null or empty.");

            if (_nullValidator.Validate(payload.ComparingValue))
                return BadRequest($"{nameof(payload.ComparingValue)} cannot be null.");


            var switchData = GetSwitchData(payload);

            ISwitch<CalculatorValue> resolver  = new Switch<int>(switchData, payload.DefaultValue);
            var result = resolver.Evaluate(payload.ComparingValue);
            var operationResult  = new Result<decimal>(){CorrelationId = payload.CorrelationId, Value = result};
            return Ok(operationResult);
        }

        private Dictionary<IOrderedCondition<int, CalculatorValue>, decimal> GetSwitchData(SwitchPayload payload)
        {
            var result = new Dictionary<IOrderedCondition<int, CalculatorValue>, decimal>();

            foreach (var item in payload.ReferenceData)
            {
                if (item.Conditions == null || !item.Conditions.Any())
                    continue;

                var conditions = item.Conditions
                    .Where(condition => condition?.ReferenceValue != null)
                    .Select(mConditionItem => new Condition(mConditionItem.ReferenceValue, mConditionItem.Operator))
                    .Cast<ICondition<CalculatorValue>>();

                var multiCondition = new MultiCondition(conditions, BooleanOperator.And);
                var orderedCondition = new OrderedCondition(multiCondition, item.Order);
                result.Add(orderedCondition, item.Value);
            }

            return result;
        }
    }
}