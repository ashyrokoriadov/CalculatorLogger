using System.Collections.Generic;
using System.Linq;
using Calculator.Service.Interfaces;
using LoggingCalculator.AbstractionsAndModels;
using LoggingCalculator.AbstractionsAndModels.Models;
using LoggingCalculator.AbstractionsAndModels.Payloads;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculator<CalculatorValue> _calculator;
        private readonly ICalculatorValidator _validator;

        public CalculatorController(
            ICalculator<CalculatorValue> calculator
            , ICalculatorValidator validator)
        {
            _calculator = calculator;
            _validator = validator;
        }

        [HttpGet("Add")]
        public ActionResult<CalculatorValue> Add([FromBody]CalculatorPayload payload)
        {
            if (_validator.ValidateNull(payload, out var result))
            {
                return result;
            }

            var calculatioResult = _calculator.Add(payload.ValueX, payload.ValueY);
            calculatioResult.CorrelationId = payload.CorrelationId;
            return Ok(calculatioResult);
        }

        [HttpGet("AddEnumerable")]
        public ActionResult<CalculatorValue> Add([FromBody]CalculatorEnumerablePayload payload)
        {
            if (_validator.ValidateNull(payload, out var result))
            {
                return result;
            }

            var calculatioResult = _calculator.Add(payload.Values);
            calculatioResult.CorrelationId = payload.CorrelationId;
            return Ok(calculatioResult);
        }

        [HttpGet("Average")]
        public ActionResult<CalculatorValue> Average([FromBody]CalculatorPayload payload)
        {
            if (_validator.ValidateNull(payload, out var result))
            {
                return result;
            }

            var calculatioResult = _calculator.Average(payload.ValueX, payload.ValueY);
            calculatioResult.CorrelationId = payload.CorrelationId;
            return Ok(calculatioResult);
        }

        [HttpGet("AverageEnumerable")]
        public ActionResult<CalculatorValue> Average([FromBody]CalculatorEnumerablePayload payload)
        {
            if (_validator.ValidateNull(payload, out var result))
            {
                return result;
            }

            var calculatioResult = _calculator.Average(payload.Values);
            calculatioResult.CorrelationId = payload.CorrelationId;
            return Ok(calculatioResult);
        }

        [HttpGet("Divide")]
        public ActionResult<CalculatorValue> Divide([FromBody]CalculatorPayload payload)
        {
            if (_validator.ValidateNull(payload, out var nullResult))
            {
                return nullResult;
            }

            if (_validator.ValidateZero(payload, out var zeroResult))
            {
                return zeroResult;
            }

            var calculatioResult = _calculator.Divide(payload.ValueX, payload.ValueY);
            calculatioResult.CorrelationId = payload.CorrelationId;
            return Ok(calculatioResult);
        }

        [HttpGet("DivideEnumerable")]
        public ActionResult<CalculatorValue> Divide([FromBody]CalculatorDividePayload payload)
        {
            if (_validator.ValidateNull(payload, out var nullResult))
            {
                return nullResult;
            }

            if (_validator.ValidateZero(payload, out var zeroResult))
            {
                return zeroResult;
            }

            var calculatioResult = _calculator.Divide(payload.Values, payload.Dividend);
            calculatioResult.CorrelationId = payload.CorrelationId;
            return Ok(calculatioResult);
        }

        [HttpGet("Max")]
        public ActionResult<CalculatorValue> Max([FromBody]CalculatorPayload payload)
        {
            if (_validator.ValidateNull(payload, out var result))
            {
                return result;
            }

            var calculatioResult = _calculator.Max(payload.ValueX, payload.ValueY);
            calculatioResult.CorrelationId = payload.CorrelationId;
            return Ok(calculatioResult);
        }

        [HttpGet("MaxEnumerable")]
        public ActionResult<CalculatorValue> Max([FromBody]CalculatorEnumerablePayload payload)
        {
            if (_validator.ValidateNull(payload, out var result))
            {
                return result;
            }

            var calculatioResult = _calculator.Max(payload.Values);
            calculatioResult.CorrelationId = payload.CorrelationId;
            return Ok(calculatioResult);
        }

        [HttpGet("Min")]
        public ActionResult<CalculatorValue> Min([FromBody]CalculatorPayload payload)
        {
            if (_validator.ValidateNull(payload, out var result))
            {
                return result;
            }

            var calculatioResult = _calculator.Min(payload.ValueX, payload.ValueY);
            calculatioResult.CorrelationId = payload.CorrelationId;
            return Ok(calculatioResult);
        }

        [HttpGet("MinEnumerable")]
        public ActionResult<CalculatorValue> Min([FromBody]CalculatorEnumerablePayload payload)
        {
            if (_validator.ValidateNull(payload, out var result))
            {
                return result;
            }

            var calculatioResult = _calculator.Min(payload.Values);
            calculatioResult.CorrelationId = payload.CorrelationId;
            return Ok(calculatioResult);
        }

        [HttpGet("Multiply")]
        public ActionResult<CalculatorValue> Multiply([FromBody]CalculatorPayload payload)
        {
            if (_validator.ValidateNull(payload, out var result))
            {
                return result;
            }

            var calculatioResult = _calculator.Multiply(payload.ValueX, payload.ValueY);
            calculatioResult.CorrelationId = payload.CorrelationId;
            return Ok(calculatioResult);
        }

        [HttpGet("MultiplyEnumerable")]
        public ActionResult<CalculatorValue> Multiply([FromBody]CalculatorEnumerablePayload payload)
        {

            if (_validator.ValidateNull(payload, out var result))
            {
                return result;
            }

            var calculatioResult = _calculator.Multiply(payload.Values);
            calculatioResult.CorrelationId = payload.CorrelationId;
            return Ok(calculatioResult);
        }

        [HttpGet("Subtract")]
        public ActionResult<CalculatorValue> Subtract([FromBody]CalculatorPayload payload)
        {
            if (_validator.ValidateNull(payload, out var result))
            {
                return result;
            }

            var calculatioResult = _calculator.Subtract(payload.ValueX, payload.ValueY);
            calculatioResult.CorrelationId = payload.CorrelationId;
            return Ok(calculatioResult);
        }

        [HttpGet("SubtractEnumerable")]
        public ActionResult<CalculatorValue> Subtract([FromBody]CalculatorSubtractPayload payload)
        {
            if (_validator.ValidateNull(payload, out var result))
            {
                return result;
            }

            return Ok(_calculator.Subtract(payload.Values, payload.Minuend));
        }
    }
}