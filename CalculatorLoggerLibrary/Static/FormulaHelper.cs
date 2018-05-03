using System.Text;

using CalculatorLoggerLibrary.Models;

namespace CalculatorLoggerLibrary.Static
{
    /// <summary>
    /// A static class for creating formulas for displaying / saving in loggers.
    /// </summary>
    public static class FormulaHelper
    {
        /// <summary>
        /// A method creates string representation of mathematical formula with basic mathematical operations.
        /// </summary>
        /// <param name="formulaText">A string builder object containing a formula's text.</param>
        /// <param name="formulaResolutionText">A string builder object containing a formula's text where variable are substituted by values.</param>
        /// <param name="op">An enum representing a mathematical operation.</param>
        /// <param name="values">An array of values to be used in calculation.</param>
        public static void BuildOperationFormula(ref StringBuilder formulaText, ref StringBuilder formulaResolutionText, MathOperator op, params CalculationUnit[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (i == 0)
                {
                    formulaText.Append(values[i].ItemDescription);
                    if (values[i].Item < 0)
                    {
                        formulaResolutionText.AppendFormat("({0})", values[i].Item);
                    }
                    else
                    {
                        formulaResolutionText.Append(values[i].Item);
                    }
                }
                else
                {
                    formulaText.AppendFormat(" {0} {1}", OperatorConverter.MathOperatorToString(op), values[i].ItemDescription);
                    if (values[i].Item < 0)
                    {
                        formulaResolutionText.AppendFormat(" {0} ({1})", OperatorConverter.MathOperatorToString(op), values[i].Item);
                    }
                    else
                    {
                        formulaResolutionText.AppendFormat(" {0} {1}", OperatorConverter.MathOperatorToString(op), values[i].Item);
                    }
                }
            }
        }

        /// <summary>
        /// A method creates string representation of statistical operations on values.
        /// </summary>
        /// <param name="elementsText">A string builder object containing values to be compared.</param>
        /// <param name="values">An array of values to be used in calculation.</param>
        public static void BuildStatisticalElements(ref StringBuilder elementsText, params CalculationUnit[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (i != (values.Length - 1))
                {
                    elementsText.AppendFormat("{0} = {1}, ", values[i].ItemDescription, values[i].Item.ToString());
                }
                else
                {
                    elementsText.AppendFormat("{0} = {1}", values[i].ItemDescription, values[i].Item.ToString());
                }
            }
        }
    }
}
