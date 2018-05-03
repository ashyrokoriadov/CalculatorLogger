namespace CalculatorLoggerLibrary.Models.Operations
{
    /// <summary>
    /// A class containing multiple conditions and extends functionality of <c>Condition</c> class.
    /// </summary>
    public class MultiCondition : Condition
    {
        /// <summary>
        /// An array of <c>SingleCondition</c> objects to be used as multi-condition components.
        /// </summary>
        public SingleCondition[] SingleConditions { get; private set; }

        /// <summary>
        /// A logic operator to be used with <c>SingleCondition</c> objects.
        /// </summary>
        public LogicOperator LogicOperator;

        /// <summary>
        /// A counstructor for <c>MultiCondtion</c> class
        /// </summary>
        /// <param name="op">A logic operator to be used with <c>SingleCondition</c> objects.</param>
        /// <param name="SingleConditions">An array of <c>SingleCondition</c> object to be used as multi-condition components.</param>
        /// <param name="valueIfTrue">A value to be assigned to a result value if a condition is true. Default is 1.</param>
        /// <param name="valueIfFalse">A value to be assigned to a result value if a condition is false. Default is 0.</param>
        /// <example>
        /// <code>
        /// CalculationUnit calculationUnit1 = new CalculationUnit(-10M, "TestValueName1");
        /// CalculationUnit calculationUnit2 = new CalculationUnit(12M, "TestValueName2");
        /// SingleCondition simpleCondition1 = new SingleCondition(ConditionOperator.GreaterThan, calculationUnit1);
        /// SingleCondition simpleCondition2 = new SingleCondition(ConditionOperator.GreaterThan, calculationUnit2);
        /// MultiCondition mc = new MultiCondition(LogicOperator.And, new SingleCondition[2] { simpleCondition1, simpleCondition2 }, 111, 222);
        /// </code>
        /// </example>
        public MultiCondition(LogicOperator op, SingleCondition[] SingleConditions, decimal valueIfTrue=1, decimal valueIfFalse=0)
        {
            ValueIfTrue = valueIfTrue;
            ValueIfFalse = valueIfFalse;
            LogicOperator = op;
            this.SingleConditions = SingleConditions;
        }

        /// <summary>
        /// A methods sets result of the multicondition. The result is assigned to <c>ConditionResult</c> and <c>ConditionValue</c> properties.
        /// </summary>
        /// <param name="cu">A value to be compared.</param>
        /// <example>
        /// <code>
        /// CalculationUnit calculationUnit1 = new CalculationUnit(-10M, "TestValueName1");
        /// CalculationUnit calculationUnit2 = new CalculationUnit(12M, "TestValueName2");
        /// SingleCondition simpleCondition1 = new SingleCondition(ConditionOperator.GreaterThan, calculationUnit1);
        /// SingleCondition simpleCondition2 = new SingleCondition(ConditionOperator.GreaterThan, calculationUnit2);
        /// MultiCondition multiCondition = new MultiCondition(LogicOperator.And, new SingleCondition[2] { simpleCondition1, simpleCondition2 }, 111, 222);
        /// CalculationUnit calculationUnit3 = new CalculationUnit(13M, "TestValueName3");
        /// multiCondition.SetResult(calculationUnit3);
        /// //Output: true (13M >= -10M AND 13M > 12M), result is 111.
        /// </code>
        /// </example>
        public override void SetResult(CalculationUnit cu)
        {
            bool MultiCondtionResult = false;

            foreach (SingleCondition sc in SingleConditions)
            {
                sc.SetResult(cu);
            }

            int counter = 0;
            foreach (SingleCondition sc in SingleConditions)
            {
                if(counter == 0)
                {
                    MultiCondtionResult = sc.ConditionResult;
                    counter++;
                    continue;
                }

                switch (LogicOperator)
                {
                    case LogicOperator.And:
                        MultiCondtionResult = MultiCondtionResult && sc.ConditionResult;
                        break;
                    case LogicOperator.Or:
                        MultiCondtionResult = MultiCondtionResult || sc.ConditionResult;
                        break;
                }
                counter++;  
            }

            ConditionResult = MultiCondtionResult;
            ConditionValue = ConditionResult ? ValueIfTrue : ValueIfFalse;
        }
    }
}
