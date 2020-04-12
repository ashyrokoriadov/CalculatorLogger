using System;
using System.Collections.Generic;
using System.Text;
using Calculator.Abstractions.Converters;
using Calculator.Enums;

namespace Calculator.Converters
{
    public class BooleanOperatorToString : IConverter<BooleanOperator, string>
    {
        public string Convert(BooleanOperator @operator)
        {
            switch (@operator)
            {
                case BooleanOperator.And:
                    return "AND";
                case BooleanOperator.Or:
                    return "OR";
                case BooleanOperator.Not:
                    return "NOT";
                default:
                    return string.Empty;
            }
        }
    }
}
