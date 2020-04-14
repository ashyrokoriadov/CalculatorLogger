using LoggingCalculator.AbstractionsAndModels.Converters;
using LoggingCalculator.AbstractionsAndModels.Enums;

namespace Calculator.Converters
{
    public class LogicalOperatorToString : IConverter<LogicalOperator, string>
    {
        public string Convert(LogicalOperator @operator)
        {
            switch (@operator)
            {
                case LogicalOperator.Equal:
                    return "==";
                case LogicalOperator.NotEqual:
                    return "!=";
                case LogicalOperator.Greater:
                    return ">";
                case LogicalOperator.GreaterOrEqual:
                    return ">=";
                case LogicalOperator.Less:
                    return "<";
                case LogicalOperator.LessOrEqual:
                    return "<=";
                default:
                    return string.Empty;
            }
        }
    }
}
