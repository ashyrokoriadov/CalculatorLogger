using CalculatorLoggerLibrary.Models;
using CalculatorLoggerLibrary.Models.Operations;

namespace CalculatorLoggerLibrary.Interfaces
{
    /// <summary>
    /// An interface containing functionality declaration for condition / switch resolving.
    /// </summary>
    public interface IConditionResolver
    {
        /// <summary>
        /// Resolves a single condition.
        /// </summary>
        /// <param name="resultValueName">A name of result to be used in further calculations</param>
        /// <param name="valueToCompare">A string indetifier in an internal collection</param>
        /// <param name="SimpleCondition">A simple condition to be resolved</param>
        /// <param name="log">A flag to indetify whether the method execution has to be logged</param>
        /// <returns>A result of simple condition resolving</returns>
        CalculationUnit ResolveCondition(string resultValueName, string valueToCompare, SingleCondition SimpleCondition, bool log);

        /// <summary>
        /// Resolves a multi condition.
        /// </summary>
        /// <param name="resultValueName">A name of result to be used in further calculations</param>
        /// <param name="valueToCompare">A string indetifier in an internal collection</param>
        /// <param name="MultiCondition">A multi condition to be resolved</param>
        /// <param name="log">A flag to indetify whether the method execution has to be logged</param>
        /// <returns>A result of multi condition resolving</returns>
        CalculationUnit ResolveMultiCondition(string resultValueName, string valueToCompare, MultiCondition MultiCondition, bool log);

        /// <summary>
        /// Resolves a switch expression.
        /// </summary>
        /// <param name="resultValueName">A name of result to be used in further calculations</param>
        /// <param name="valueToCompare">A string indetifier in an internal collection</param>
        /// <param name="MultiConditions">An array of multi conditions that forms a switch expression</param>
        /// <returns>A result of switch expression resolving</returns>
        CalculationUnit ResolveSwitch(string resultValueName, string valueToCompare, MultiCondition[] MultiConditions);
    }
}
