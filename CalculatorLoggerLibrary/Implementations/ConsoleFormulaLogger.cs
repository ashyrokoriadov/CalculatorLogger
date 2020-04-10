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
        /// A method logs a resolving of <c>SingleCondition</c>.
        /// </summary>
        /// <param name="sc">A single condition to be resolved</param>
        /// <param name="newValueName">A name of a single condition result to be used in calculations</param>
        public void LogCondition(SingleCondition sc, string newValueName)
        {
            StringBuilder formulaText = new StringBuilder();
            StringBuilder formulaResolutionText = new StringBuilder();

            formulaText.AppendFormat("{0} {1} {2} ? {3} : {4}", sc.ValueToCompare.ItemDescription, OperatorConverter.LogicOperatorToString(sc.ConditionOperator), sc.Sample.ItemDescription, sc.ValueIfTrue, sc.ValueIfFalse);
            formulaResolutionText.AppendFormat("{0} {1} {2} = {3}", sc.ValueToCompare.Item, OperatorConverter.LogicOperatorToString(sc.ConditionOperator), sc.Sample.Item, sc.ConditionResult.ToString());

            lock (_lockObject)
            {
                string datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                Console.WriteLine("{0} | {1} | {2} | {3}", order.ToString(), datetime, "Condition", formulaText.ToString());
                order++;
                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                Console.WriteLine("{0} | {1} | {2} | {3}", order.ToString(), datetime, "Condition resolution", formulaResolutionText.ToString());
                order++;
                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                Console.WriteLine("{0} | {1} | {2} | {3}", order.ToString(), datetime, "Condition result name", newValueName);
                order++;
                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                Console.WriteLine("{0} | {1} | {2} | {3}", order.ToString(), datetime, "Condition result value", sc.ConditionValue.ToString());
                order++;
            }

            /*
             * Simple condition resolution log:
             * 1 |2017-11-22 08:39:15.299 |Condition | name1 "condition operator" valueToCompare ? VALUE-IF-TRUE : VALUE-IF-FALSE
             * 2 |2017-11-22 08:39:15.315 |Condition resolution | 11.5 < 0 = [Result] 
             * 3 |2017-11-22 08:39:15.316 |Condition result name | SomeName
             * 4 |2017-11-22 08:39:15.316 |Condition result value | VALUE-IF-FALSE
             */ 
        }

        /// <summary>
        /// A method logs a resolving of <c>MultiCondition</c>.
        /// </summary>
        /// <param name="mc">A multi condition to be resolved</param>
        public void LogMultiCondition(MultiCondition mc)
        {            
            lock (_lockObject)
            {
                string datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                Console.WriteLine("{0} | {1} | {2} ", order.ToString(), datetime, "**********Section MULTI CONDITION Start**********");
                order++;
                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                Console.WriteLine("{0} | {1} | {2} | {3}", order.ToString(), datetime, "Value if true", mc.ValueIfTrue);
                order++;
                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                Console.WriteLine("{0} | {1} | {2} | {3}", order.ToString(), datetime, "Value if false", mc.ValueIfFalse);
                order++;
                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                Console.WriteLine("{0} | {1} | {2} | {3}", order.ToString(), datetime, "Logical operator applied to single conditions", mc.LogicOperator.ToString().ToUpper());
                order++;

                int counter = 1;
                foreach (SingleCondition simpleCondition in mc.SingleConditions)
                {
                    datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                    Console.WriteLine("{0} | {1} | {2} | {3}", order.ToString(), datetime, "Condition", counter.ToString());
                    order++;
                    this.LogCondition(simpleCondition, string.Format("TemporaryValue{0}", counter.ToString()));
                    counter++;
                }

                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                Console.WriteLine("{0} | {1} | {2} | {3}", order.ToString(), datetime, "Multi condition result", mc.ConditionResult.ToString());
                order++;

                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                Console.WriteLine("{0} | {1} | {2} | {3}", order.ToString(), datetime, "Multi condition result value", mc.ConditionValue);
                order++;

                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                Console.WriteLine("{0} | {1} | {2} ", order.ToString(), datetime, "**********Section MULTI CONDITION End************");
                order++;
            }


            /*
             *  Multi condition resolution log:
             *   1|2017-11-22 08:39:15.298|**********Section MULTI CONDITION Start**********
             *   2|2017-11-22 08:39:15.298|Value if true| TRUE-VALUE
             *   3|2017-11-22 08:39:15.298|Value if false| FALSE-VALUE
             *   3|2017-11-22 08:39:15.298|Logical operator applied to single conditions| AND
             *   4|2017-11-22 08:39:15.298|Condition| 1
             *   5|2017-11-22 08:39:15.299|Condition|name1 "condition operator" valueToCompare 
             *   6|2017-11-22 08:39:15.315|Condition resolution| 11.5 < 0 = [Result] //False
             *   7|2017-11-22 08:39:15.316|Condition result name|SomeName
             *   8|2017-11-22 08:39:15.316|Condition result value|VALUE-IF-FALSE
             *   9|2017-11-22 08:39:15.298|Condition| 2
             *   10|2017-11-22 08:39:15.300|Condition|name1 "condition operator" valueToCompare 
             *   11|2017-11-22 08:39:15.316|Condition resolution| 11.5 > 0 = [Result] //True
             *   12|2017-11-22 08:39:15.316|Condition result name|SomeName
             *   13|2017-11-22 08:39:15.316|Condition result value|VALUE-IF-FALSE
             *   14|2017-11-22 08:39:15.298|Condition| 3
             *   15|2017-11-22 08:39:15.301|Condition|name1 "condition operator" valueToCompare 
             *   16|2017-11-22 08:39:15.317|Condition resolution| 11.5 == 0 = [Result] //True
             *   17|2017-11-22 08:39:15.316|Condition result name|SomeName
             *   18|2017-11-22 08:39:15.316|Condition result value|VALUE-IF-FALSE
             *   19|2017-11-22 08:39:15.319|Multi condition result| False
             *   20|2017-11-22 08:39:15.319|Multi condition result value| FALSE-VALUE
             *   21|2017-11-22 08:39:15.298|**********Section MULTI CONDITION End************
             */
        }

        /// <summary>
        /// A method logs a switch resolving.
        /// </summary>
        /// <param name="mc">An array of multi conditions that were used to create a switch</param>
        /// <param name="result">A result returned be a switch resolving</param>
        public void LogSwitch(MultiCondition[] mc, CalculationUnit result)
        {
            lock (_lockObject)
            {
                string datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                Console.WriteLine("{0} | {1} | {2} ", order.ToString(), datetime, "**********Section SWITCH Start*******************");
                order++;

                foreach (MultiCondition multiCondition in mc)
                {
                    this.LogMultiCondition(multiCondition);
                }

                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                Console.WriteLine("{0} | {1} | {2} | {3}", order.ToString(), datetime, "Formula result name", result.ItemDescription);
                order++;

                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                Console.WriteLine("{0} | {1} | {2} | {3}", order.ToString(), datetime, "Formula result value", result.Item.ToString());
                order++;

                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                Console.WriteLine("{0} | {1} | {2} ", order.ToString(), datetime, "**********Section SWITCH End*********************");
                order++;
            }

            /*Switch statement resolution log:
             * 1|2017-11-22 08:39:15.298|**********Section SWITCH Start**********
             * 1|2017-11-22 08:39:15.298|<MULTI CONDITION 1>
             * 2|2017-11-22 08:39:15.298|<MULTI CONDITION 2>
             * 3|2017-11-22 08:39:15.298|<MULTI CONDITION ...>
             * 4|2017-11-22 08:39:15.299|<MULTI CONDITION N>
             * 5|2017-11-22 08:39:15.299|Formula result name| SomeStandardName
             * 6|2017-11-22 08:39:15.299|Formula result value| SOME-VALUE
             * 7|2017-11-22 08:39:15.298|**********Section SWITCH End************
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
