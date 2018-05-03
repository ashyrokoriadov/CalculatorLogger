namespace CalculatorLoggerLibrary.Models.Operations
{
    /// <summary>
    /// A base abstract class for <c>Condition-derived</c> calsses.
    /// </summary>
    public abstract class Condition
    {
        /// <summary>
        /// A value to be assigned to a result value if a condition is true.
        /// </summary>
        public decimal ValueIfTrue { get; protected set; }

        /// <summary>
        /// A value to be assigned to a result value if a condition is false.
        /// </summary>
        public decimal ValueIfFalse { get; protected set; }

        /// <summary>
        /// A value to be compared.
        /// </summary>
        public CalculationUnit ValueToCompare { get; set; }

        /// <summary>
        /// A reference value to be compared to.
        /// </summary>
        public CalculationUnit Sample { get; protected set; }

        /// <summary>
        /// A conditional operator to be used in comparison.
        /// </summary>
        public ConditionOperator ConditionOperator;

        /// <summary>
        /// A condtion result after comparison - true or false.
        /// </summary>
        public bool ConditionResult { get; protected set; }

        /// <summary>
        /// A condtion result value after comparison - depends on properties <c>ValueIfTrue</c> and <c>ValueIfFalse</c>.
        /// </summary>
        /// <seealso cref="ValueIfTrue"/>
        /// <seealso cref="ValueIfFalse"/>
        public decimal ConditionValue { get; protected set; }

        /// <summary>
        /// A methods sets result of the condition. The result is assigned to <c>ConditionResult</c> and <c>ConditionValue</c> properties.
        /// </summary>
        /// <param name="cu">A value to be compared.</param>
        /// <seealso cref="ConditionResult"/>
        /// <seealso cref="ConditionValue"/>
        public abstract void SetResult(CalculationUnit cu);
    }
}
