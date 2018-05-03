namespace CalculatorLoggerLibrary.Models
{
    /// <summary>
    /// An enumeration to represent mathematical operators.
    /// </summary>
    /// <remarks>
    /// The possible values are Add, Subtract, Division, Multiplication
    /// </remarks>
    public enum MathOperator
    {
        Add,
        Subtract,
        Division,
        Multiplication
    }

    /// <summary>
    /// An enumeration to represent comparison operators.
    /// </summary>
    /// <remarks>
    ///  The possible values are GreaterThan, LessThan, GreaterThanOrEqual, LessThanOrEqual, Equal, NotEqual
    /// </remarks>
    public enum ConditionOperator
    {
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        Equal,
        NotEqual
    }

    /// <summary>
    /// An enumeration to represent statistical operators.
    /// </summary>
    /// <remarks>
    ///  The possible values are Max, Min, Avg
    /// </remarks>
    public enum StatisticalOperator
    {
        Max,
        Min,
        Avg
    }

    /// <summary>
    /// An enumeration to represent logical operators.
    /// </summary>
    /// <remarks>
    ///  The possible values are And, Or
    /// </remarks>
    public enum LogicOperator
    {
        And,
        Or
    }
}
