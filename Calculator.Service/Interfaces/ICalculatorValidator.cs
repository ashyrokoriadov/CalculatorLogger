using LoggingCalculator.AbstractionsAndModels.Models;
using LoggingCalculator.AbstractionsAndModels.Payloads;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Calculator.Service.Interfaces
{
    public interface ICalculatorValidator
    {
        bool ValidateNull(CalculatorPayload payload, out ActionResult<CalculatorValue> result);
        bool ValidateNull(CalculatorSubtractPayload payload, out ActionResult<CalculatorValue> result);
        bool ValidateNull(CalculatorDividePayload payload, out ActionResult<CalculatorValue> result);
        bool ValidateNull(CalculatorEnumerablePayload values, out ActionResult<CalculatorValue> result);
        bool ValidateZero(CalculatorPayload payload, out ActionResult<CalculatorValue> result);
        bool ValidateZero(CalculatorDividePayload payload, out ActionResult<CalculatorValue> result);
        bool ValidateZero(CalculatorEnumerablePayload values, out ActionResult<CalculatorValue> result);
    }
}
