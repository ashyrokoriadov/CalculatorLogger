namespace CalculatorLoggerLibrary.Models.Operations
{
    /// <summary>
    /// A class containing one condition and extends functionality of <c>Condition</c> class.
    /// </summary>
    public class SingleCondition:Condition
    {
        /// <summary>
        /// A counstructor for <c>SingleCondtion</c> class
        /// </summary>
        /// <param name="op">A conditional operator to be used in comparison.</param>
        /// <param name="sample">A reference value to be compared to.</param>
        /// <param name="valueIfTrue">A value to be assigned to a result value if a condition is true. Default is 1.</param>
        /// <param name="valueIfFalse">A value to be assigned to a result value if a condition is false. Default is 0.</param>
        /// <example>
        /// <code>
        /// CalculationUnit calculationUnit1 = new CalculationUnit(-9.8M, "TestValueName");
        /// SingleCondition simpleCondition1 = new SingleCondition(ConditionOperator.GreaterThan, calculationUnit1, 123, 354);
        /// </code>
        /// </example>
        public SingleCondition(ConditionOperator op, CalculationUnit sample, decimal valueIfTrue=1, decimal valueIfFalse=0)
        {
            ValueIfTrue = valueIfTrue;
            ValueIfFalse = valueIfFalse;
            ConditionOperator = op;
            Sample = sample;
        }

        /// <summary>
        /// A methods sets result of the condition. The result is assigned to <c>ConditionResult</c> and <c>ConditionValue</c> properties.
        /// </summary>
        /// <param name="cu">A value to be compared.</param>
        /// <example>
        /// <code>
        /// CalculationUnit calculationUnit1 = new CalculationUnit(-10M, "TestValueName1");
        /// SingleCondition simpleCondition1 = new SingleCondition(ConditionOperator.GreaterThan, calculationUnit1, 123, 354);
        /// CalculationUnit calculationUnit2 = new CalculationUnit(12M, "TestValueName2");
        /// simpleCondition1.SetResult(calculationUnit2);
        /// //Output: true (12M >= -10M), result is 123.
        /// </code>
        /// </example>
        public override void SetResult(CalculationUnit valueToCompare)
        {
            ValueToCompare = valueToCompare;

            if (ValueToCompare != null)
            {
                switch(ConditionOperator)
                {
                    case ConditionOperator.GreaterThan:
                        ConditionResult = ValueToCompare > Sample;
                        break;
                    case ConditionOperator.GreaterThanOrEqual:
                        ConditionResult = ValueToCompare >= Sample;
                        break;
                    case ConditionOperator.LessThan:
                        ConditionResult = ValueToCompare < Sample;
                        break;
                    case ConditionOperator.LessThanOrEqual:
                        ConditionResult = ValueToCompare <= Sample;
                        break;
                    case ConditionOperator.Equal:
                        ConditionResult = ValueToCompare.Item==Sample.Item;
                        break;
                    default:
                        ConditionResult = false;
                        break;
                }

                ConditionValue = ConditionResult ? ValueIfTrue : ValueIfFalse;
            }
        }
    }
}
