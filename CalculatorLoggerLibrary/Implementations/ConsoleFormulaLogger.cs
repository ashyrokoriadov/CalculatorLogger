using System;
using System.Text;

using CalculatorLoggerLibrary.Interfaces;
using CalculatorLoggerLibrary.Models;
using CalculatorLoggerLibrary.Models.Operations;

using CalculatorLoggerLibrary.Static;

namespace CalculatorLoggerLibrary.Implementations
{
    /// <summary>
    /// A class used to log calculations into a Console window.
    /// </summary>
    public sealed class ConsoleFormulaLogger : IFormulaLogger
    {
        private int order = 0;
        private Object _lockObject = new Object();

        /// <summary>
        /// A method logs simple mathematical operation.
        /// </summary>
        /// <param name="op">A mathematical operator</param>
        /// <param name="result">A result of calculations</param>
        /// <param name="values">Values that were used in a calculation</param>
        public void LogOperation(MathOperator op, CalculationUnit result, params CalculationUnit[] values)
        {
            StringBuilder formulaText = new StringBuilder();
            StringBuilder formulaResolutionText = new StringBuilder();

            FormulaHelper.BuildOperationFormula(ref formulaText, ref formulaResolutionText, op, values);

            lock (_lockObject)
            {
                string datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                Console.WriteLine("{0} | {1} | {2} | {3}", order.ToString(), datetime, "Formula", formulaText.ToString());
                order++;
                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                Console.WriteLine("{0} | {1} | {2} | {3}", order.ToString(), datetime, "Formula resolution", formulaResolutionText.ToString());
                order++;
                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                Console.WriteLine("{0} | {1} | {2} | {3}", order.ToString(), datetime, "Formula result name", result.ItemDescription);
                order++;
                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                Console.WriteLine("{0} | {1} | {2} | {3}", order.ToString(), datetime, "Formula result value", result.Item.ToString());
                order++;
            }
            
            /*
             * 1 |2017-11-22 08:39:15.299 | Formula | name1 + name2 + name3 + name4
             * 2 |2017-11-22 08:39:15.315 |Formula resolution | (-10.5) + 12.5 + 13.8 + (-9.4)
             * 3 |2017-11-22 08:39:15.316 |Formula result name | SomeTestValue
             * 4 |2017-11-22 08:39:15.316 |Formula result value | 6.4
             */
        }
       
        /// <summary>
        /// A method log a statistical formula.
        /// </summary>
        /// <param name="op">A statistical operator that was used in a calculation</param>
        /// <param name="result">A result of evaliuation of a statistical expression</param>
        /// <param name="values">Values that were evaluated with using of a statistical expression</param>
        public void LogStatisticalFormula(StatisticalOperator op, CalculationUnit result, params CalculationUnit[] values)
        {
            StringBuilder elementsText = new StringBuilder();

            FormulaHelper.BuildStatisticalElements(ref elementsText, values);

            lock (_lockObject)
            {
                string datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                Console.WriteLine("{0} | {1} | {2} | {3}", order.ToString(), datetime, "Elements", elementsText.ToString());
                order++;

                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                Console.WriteLine("{0} | {1} | {2} | {3}", order.ToString(), datetime, "Condition", OperatorConverter.StatisticOperatorToString(op));
                order++;

                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                Console.WriteLine("{0} | {1} | {2} | {3}", order.ToString(), datetime, "Formula result name", result.ItemDescription);
                order++;

                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                Console.WriteLine("{0} | {1} | {2} | {3}", order.ToString(), datetime, "Formula result value", result.Item.ToString());
                order++;
            }
            
            /*
             * Max/min/avg formula log:
             * 1|2017-11-22 08:39:15.299|Elements|name1=value1, name2=value2, ..., nameN=valueN
             * 2|2017-11-22 08:39:15.299|Condition|SELECT MAX (SELECT MIN, SELECT AVERAGE)
             * 3|2017-11-22 08:39:15.299|Formula result name| SomeStandardName
             * 4|2017-11-22 08:39:15.299|Formula result value| value2
             */
        }
    }
}
