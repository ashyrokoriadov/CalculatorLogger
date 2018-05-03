using System;

using CalculatorLoggerLibrary.Models;

namespace CalculatorLoggerLibrary.Static
{
    /// <summary>
    /// A static class for converting enums to a corresponding mathematical or logical sign, statistical expression.
    /// </summary>
    public static class OperatorConverter
    {
        /// <summary>
        /// A method converts an enum to a mathematical sign.
        /// </summary>
        /// <param name="op">An enum to be converted.</param>
        /// <returns>A string representation of a mathematical sign.</returns>
        public static string MathOperatorToString(MathOperator op)
        {
            switch (op)
            {
                case MathOperator.Add:
                    return "+";
                case MathOperator.Subtract:
                    return "-";
                case MathOperator.Division:
                    return "/";
                case MathOperator.Multiplication:
                    return "*";
            }

            return string.Empty;
        }

        /// <summary>
        /// A method converts an enum to a logical sign.
        /// </summary>
        /// <param name="op">An enum to be converted.</param>
        /// <returns>A string representation of a logical sign.</returns>
        public static string LogicOperatorToString(ConditionOperator op)
        {
            switch (op)
            {
                case ConditionOperator.GreaterThan:
                    return ">";
                case ConditionOperator.LessThan:
                    return "<";
                case ConditionOperator.GreaterThanOrEqual:
                    return ">=";
                case ConditionOperator.LessThanOrEqual:
                    return "=<";
                case ConditionOperator.Equal:
                    return "=";
                case ConditionOperator.NotEqual:
                    return "!=";
            }

            return string.Empty;
        }

        /// <summary>
        /// A method converts an enum to a statistical expression.
        /// </summary>
        /// <param name="op">An enum to be converted.</param>
        /// <returns>A string representation of la statistical expression.</returns>
        public static string StatisticOperatorToString(StatisticalOperator op)
        {
            switch (op)
            {
                case StatisticalOperator.Max:
                    return "SELECT MAX";
                case StatisticalOperator.Min:
                    return "SELECT MIN";
                case StatisticalOperator.Avg:
                    return "SELECT AVERAGE";
            }

            return string.Empty;
        }
    }
}
