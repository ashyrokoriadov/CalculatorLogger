using System.Collections.Generic;
using System.Linq;
using Autofac.Features.AttributeFilters;
using Calculator.Service.Constants;
using LoggingCalculator.AbstractionsAndModels;
using LoggingCalculator.AbstractionsAndModels.Models;
using LoggingCalculator.AbstractionsAndModels.Payloads;
using LoggingCalculator.AbstractionsAndModels.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculator<CalculatorValue> _calculator;
        private readonly IValidator<CalculatorValue> _nullValidator;
        private readonly IValidator<CalculatorValue> _zeroValidator;

        public CalculatorController(
            ICalculator<CalculatorValue> calculator
            , [KeyFilter("NullValidator")] IValidator<CalculatorValue> nullValidator
            , [KeyFilter("ZeroValidator")] IValidator<CalculatorValue> zeroValidator)
        {
            _calculator = calculator;
            _nullValidator = nullValidator;
            _zeroValidator = zeroValidator;
        }

        [HttpGet("Add")]
        public ActionResult<CalculatorValue> Add([FromBody]CalculatorPayload payload)
        {
            if (ValidateNull(payload, out var result))
            {
                return result;
            }

            return Ok(_calculator.Add(payload.ValueX, payload.ValueY));
        }

        [HttpGet("AddEnumerable")]
        public ActionResult<CalculatorValue> Add([FromBody]IEnumerable<CalculatorValue> values)
        {
            var valuesAsArray = values.ToArray();

            if (ValidateNull(valuesAsArray, out var result))
            {
                return result;
            }

            return Ok(_calculator.Add(valuesAsArray));
        }

        [HttpGet("Average")]
        public ActionResult<CalculatorValue> Average([FromBody]CalculatorPayload payload)
        {
            if (ValidateNull(payload, out var result))
            {
                return result;
            }

            return Ok(_calculator.Average(payload.ValueX, payload.ValueY));
        }

        [HttpGet("AverageEnumerable")]
        public ActionResult<CalculatorValue> Average([FromBody]IEnumerable<CalculatorValue> values)
        {
            var valuesAsArray = values.ToArray();

            if (ValidateNull(valuesAsArray, out var result))
            {
                return result;
            }

            return Ok(_calculator.Average(valuesAsArray));
        }

        [HttpGet("Divide")]
        public ActionResult<CalculatorValue> Divide([FromBody]CalculatorPayload payload)
        {
            if (ValidateNull(payload, out var nullResult))
            {
                return nullResult;
            }

            if (ValidateZero(payload, out var zeroResult))
            {
                return zeroResult;
            }

            return Ok(_calculator.Divide(payload.ValueX, payload.ValueY));
        }

        [HttpGet("DivideEnumerable")]
        public ActionResult<CalculatorValue> Divide([FromBody]CalculatorDividePayload payload)
        {
            if (ValidateNull(payload, out var nullResult))
            {
                return nullResult;
            }

            if (ValidateZero(payload, out var zeroResult))
            {
                return zeroResult;
            }

            return Ok(_calculator.Divide(payload.Values, payload.Dividend));
        }

        [HttpGet("Max")]
        public ActionResult<CalculatorValue> Max([FromBody]CalculatorPayload payload)
        {
            if (ValidateNull(payload, out var result))
            {
                return result;
            }

            return Ok(_calculator.Max(payload.ValueX, payload.ValueY));
        }

        [HttpGet("MaxEnumerable")]
        public ActionResult<CalculatorValue> Max([FromBody]IEnumerable<CalculatorValue> values)
        {
            var valuesAsArray = values.ToArray();

            if (ValidateNull(valuesAsArray, out var result))
            {
                return result;
            }

            return Ok(_calculator.Max(valuesAsArray));
        }

        [HttpGet("Min")]
        public ActionResult<CalculatorValue> Min([FromBody]CalculatorPayload payload)
        {
            if (ValidateNull(payload, out var result))
            {
                return result;
            }

            return Ok(_calculator.Min(payload.ValueX, payload.ValueY));
        }

        [HttpGet("MinEnumerable")]
        public ActionResult<CalculatorValue> Min([FromBody]IEnumerable<CalculatorValue> values)
        {
            var valuesAsArray = values.ToArray();

            if (ValidateNull(valuesAsArray, out var result))
            {
                return result;
            }

            return Ok(_calculator.Min(valuesAsArray));
        }

        [HttpGet("Multiply")]
        public ActionResult<CalculatorValue> Multiply([FromBody]CalculatorPayload payload)
        {
            if (ValidateNull(payload, out var result))
            {
                return result;
            }

            return Ok(_calculator.Multiply(payload.ValueX, payload.ValueY));
        }

        [HttpGet("MultiplyEnumerable")]
        public ActionResult<CalculatorValue> Multiply([FromBody]IEnumerable<CalculatorValue> values)
        {
            var valuesAsArray = values.ToArray();

            if (ValidateNull(valuesAsArray, out var result))
            {
                return result;
            }

            return Ok(_calculator.Multiply(valuesAsArray));
        }

        [HttpGet("Subtract")]
        public ActionResult<CalculatorValue> Subtract([FromBody]CalculatorPayload payload)
        {
            if (ValidateNull(payload, out var result))
            {
                return result;
            }

            return Ok(_calculator.Subtract(payload.ValueX, payload.ValueY));
        }

        [HttpGet("SubtractEnumerable")]
        public ActionResult<CalculatorValue> Subtract([FromBody]CalculatorSubtractPayload payload)
        {
            if (ValidateNull(payload, out var result))
            {
                return result;
            }

            return Ok(_calculator.Subtract(payload.Values, payload.Minuend));
        }

        private bool ValidateNull(CalculatorPayload payload, out ActionResult<CalculatorValue> result)
        {
            if (_nullValidator.Validate(payload.ValueX))
            {
                result = BadRequest(string.Format(ErrorMessages.NULL_VALUE_TEMPLATE, nameof(payload.ValueX)));
                return true;
            }

            if (_nullValidator.Validate(payload.ValueY))
            {
                result = BadRequest(string.Format(ErrorMessages.NULL_VALUE_TEMPLATE, nameof(payload.ValueY)));
                return true;
            }

            result = null;
            return false;
        }

        private bool ValidateNull(CalculatorSubtractPayload payload, out ActionResult<CalculatorValue> result)
        {
            if (_nullValidator.Validate(payload.Minuend))
            {
                result = BadRequest(string.Format(ErrorMessages.NULL_VALUE_TEMPLATE, nameof(payload.Minuend)));
                return true;
            }

            if (ValidateNull(payload.Values, out result))
            {
                return true;
            }

            result = null;
            return false;
        }

        private bool ValidateNull(CalculatorDividePayload payload, out ActionResult<CalculatorValue> result)
        {
            if (_nullValidator.Validate(payload.Dividend))
            {
                result = BadRequest(string.Format(ErrorMessages.NULL_VALUE_TEMPLATE, nameof(payload.Dividend)));
                return true;
            }

            if (ValidateNull(payload.Values, out result))
            {
                return true;
            }

            result = null;
            return false;
        }

        private bool ValidateNull(IEnumerable<CalculatorValue> values, out ActionResult<CalculatorValue> result)
        {
            foreach (var payload in values)
            {
                if (_nullValidator.Validate(payload))
                {
                    result = BadRequest(ErrorMessages.NULL_VALUE);
                    return true;
                }
            }

            result = null;
            return false;
        }

        private bool ValidateZero(CalculatorPayload payload, out ActionResult<CalculatorValue> result)
        {
            if (_zeroValidator.Validate(payload.ValueY))
            {
                result = BadRequest(string.Format(ErrorMessages.NULL_DIVISION_TEMPLATE, nameof(payload.ValueY)));
                return true;
            }

            result = null;
            return false;
        }

        private bool ValidateZero(IEnumerable<CalculatorValue> values, out ActionResult<CalculatorValue> result)
        {
            foreach (var payload in values)
            {
                if (_zeroValidator.Validate(payload))
                {
                    result = BadRequest(ErrorMessages.NULL_DIVISION);
                    return true;
                }
            }

            result = null;
            return false;
        }

        private bool ValidateZero(CalculatorDividePayload payload, out ActionResult<CalculatorValue> result)
        {
            if (ValidateZero(payload.Values, out result))
            {
                return true;
            }

            result = null;
            return false;
        }
    }
}