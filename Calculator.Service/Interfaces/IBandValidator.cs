using LoggingCalculator.AbstractionsAndModels.Models;
using LoggingCalculator.AbstractionsAndModels.Payloads;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.Service.Interfaces
{
    public interface IBandValidator
    {
        bool Validate(BandResolverPayload payload, out ActionResult<CalculatorValue> result);
    }
}
