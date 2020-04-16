using System.Collections.Generic;
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

        public CalculatorController(ICalculator<CalculatorValue> calculator)
        {
            _calculator = calculator;
        }

        [HttpGet("Add")]
        public ActionResult<CalculatorValue> Add([FromBody]CalculatorPayload payload)
        {
            return Ok(_calculator.Add(payload.ValueX, payload.ValueY));
        }

        [HttpGet("AddEnumerable")]
        public ActionResult<CalculatorValue> Add([FromBody]IEnumerable<CalculatorValue> values)
        {
            return Ok(_calculator.Add(values));
        }

        [HttpGet("Average")]
        public ActionResult<CalculatorValue> Average([FromBody]CalculatorPayload payload)
        {
            return Ok(_calculator.Average(payload.ValueX, payload.ValueY));
        }

        [HttpGet("AverageEnumerable")]
        public ActionResult<CalculatorValue> Average([FromBody]IEnumerable<CalculatorValue> values)
        {
            return Ok(_calculator.Average(values));
        }

        [HttpGet("Divide")]
        public ActionResult<CalculatorValue> Divide([FromBody]CalculatorPayload payload)
        {
            return Ok(_calculator.Divide(payload.ValueX, payload.ValueY));
        }

        [HttpGet("Divide")]
        public ActionResult<CalculatorValue> Divide([FromBody]CalculatorDividePayload payload)
        {
            return Ok(_calculator.Divide(payload.Values, payload.Dividend));
        }

        [HttpGet("Max")]
        public ActionResult<CalculatorValue> Max([FromBody]CalculatorPayload payload)
        {
            return Ok(_calculator.Max(payload.ValueX, payload.ValueY));
        }

        [HttpGet("MaxEnumerable")]
        public ActionResult<CalculatorValue> Max([FromBody]IEnumerable<CalculatorValue> values)
        {
            return Ok(_calculator.Max(values));
        }

        [HttpGet("Min")]
        public ActionResult<CalculatorValue> Min([FromBody]CalculatorPayload payload)
        {
            return Ok(_calculator.Min(payload.ValueX, payload.ValueY));
        }

        [HttpGet("MinEnumerable")]
        public ActionResult<CalculatorValue> Min([FromBody]IEnumerable<CalculatorValue> values)
        {
            return Ok(_calculator.Min(values));
        }

        [HttpGet("Multiply")]
        public ActionResult<CalculatorValue> Multiply([FromBody]CalculatorPayload payload)
        {
            return Ok(_calculator.Multiply(payload.ValueX, payload.ValueY));
        }

        [HttpGet("MultiplyEnumerable")]
        public ActionResult<CalculatorValue> Multiply([FromBody]IEnumerable<CalculatorValue> values)
        {
            return Ok(_calculator.Multiply(values));
        }

        [HttpGet("Subtract")]
        public ActionResult<CalculatorValue> Subtract([FromBody]CalculatorPayload payload)
        {
            return Ok(_calculator.Subtract(payload.ValueX, payload.ValueY));
        }

        [HttpGet("Subtract")]
        public ActionResult<CalculatorValue> Subtract([FromBody]CalculatorSubtractPayload payload)
        {
            return Ok(_calculator.Subtract(payload.Values, payload.Minuend));
        }
    }
}