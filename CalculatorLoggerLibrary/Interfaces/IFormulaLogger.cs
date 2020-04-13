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
        /// A method log a statistical formula.
        /// </summary>
        /// <param name="op">A statistical operator that was used in a calculation</param>
        /// <param name="result">A result of evaliuation of a statistical expression</param>
        /// <param name="values">Values that were evaluated with using of a statistical expression</param>
        void LogStatisticalFormula(StatisticalOperator op, CalculationUnit result, params CalculationUnit[] values);
    }
}
