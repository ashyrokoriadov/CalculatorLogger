using CalculatorLoggerLibrary.Models;
using CalculatorLoggerLibrary.Models.Operations;

namespace CalculatorLoggerLibrary.Interfaces
{
    /// <summary>
    /// An interface to be implemmented by <c>Logger</c> class.
    /// </summary>
    public interface IFormulaLogger
    {
        /// <summary>
        /// A method logs simple mathematical operation.
        /// </summary>
        /// <param name="op">A mathematical operator</param>
        /// <param name="result">A result of calculations</param>
        /// <param name="values">Values that were used in a calculation</param>
        void LogOperation(MathOperator op, CalculationUnit result, params CalculationUnit[] values);

        /// <summary>
        /// A method logs a resolving of <c>SingleCondition</c>.
        /// </summary>
        /// <param name="sc">A single condition to be resolved</param>
        /// <param name="newValueName">A name of a single condition result to be used in calculations</param>
        void LogCondition(SingleCondition sc, string newValueName);

        /// <summary>
        /// A method logs a resolving of <c>MultiCondition</c>.
        /// </summary>
        /// <param name="mc">A multi condition to be resolved</param>
        void LogMultiCondition(MultiCondition mc);

        /// <summary>
        /// A method logs a switch resolving.
        /// </summary>
        /// <param name="mc">An array of multi conditions that were used to create a switch</param>
        /// <param name="result">A result returned be a switch resolving</param>
        void LogSwitch(MultiCondition[] mc, CalculationUnit result);

        /// <summary>
        /// A method log a statistical formula.
        /// </summary>
        /// <param name="op">A statistical operator that was used in a calculation</param>
        /// <param name="result">A result of evaliuation of a statistical expression</param>
        /// <param name="values">Values that were evaluated with using of a statistical expression</param>
        void LogStatisticalFormula(StatisticalOperator op, CalculationUnit result, params CalculationUnit[] values);
    }
}
