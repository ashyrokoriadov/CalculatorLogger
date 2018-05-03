using CalculatorLoggerLibrary.Models;
using CalculatorLoggerLibrary.Models.Operations;

namespace CalculatorLoggerLibrary.Interfaces
{
    /// <summary>
    /// An interface containing functionality declaration for band resolving.
    /// </summary>
    public interface IBandResolver
    {
        /// <summary>
        /// Resolves a band.
        /// </summary>
        /// <param name="resultValueName">A name of result to be used in further calculations</param>
        /// <param name="valueToCompare">A string indetifier in an internal collection</param>
        /// <param name="band">A band to be resolved</param>
        /// <returns>A result of band resolving</returns>
        CalculationUnit ResolveBand(string resultValueName, string valueToCompare, Band band);
    }
}
