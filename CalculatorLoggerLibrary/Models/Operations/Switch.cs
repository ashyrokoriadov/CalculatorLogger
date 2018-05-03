using System.Linq;

namespace CalculatorLoggerLibrary.Models.Operations
{
    /// <summary>
    /// A class resolving several multiple conditions.
    /// </summary>
    public class Switch
    {
        /// <summary>
        /// An array of <c>MultiCondition</c> objects to be used as switch components.
        /// </summary>
        public MultiCondition[] MultiConditions { get; protected set; }

        /// <summary>
        /// A value to be used as string identifier for a switch result.
        /// </summary>
        public string SwitchResultName { get; protected set; }

        /// <summary>
        /// A value to be returned after switch resolving.
        /// </summary>
        public decimal SwitchValue { get; protected set; }

        /// <summary>
        /// A counstructor for <c>Switch</c> class
        /// </summary>
        /// <param name="MultiConditions"> An array of <c>MultiCondition</c> objects to be used as switch components.</param>
        /// <remarks><c>SwitchResultName</c> is defaulted to "Switch result name"</remarks>
        /// <example>
        /// <code>
        /// CalculationUnit calculationUnit1 = new CalculationUnit(-10M, "TestValueName1");
        /// CalculationUnit calculationUnit2 = new CalculationUnit(12M, "TestValueName2");
        /// CalculationUnit calculationUnit3 = new CalculationUnit(-8M, "TestValueName3");
        /// CalculationUnit calculationUnit4 = new CalculationUnit(14M, "TestValueName4");
        /// SingleCondition simpleCondition1 = new SingleCondition(ConditionOperator.GreaterThan, calculationUnit1);
        /// SingleCondition simpleCondition2 = new SingleCondition(ConditionOperator.GreaterThan, calculationUnit2);
        /// SingleCondition simpleCondition3 = new SingleCondition(ConditionOperator.GreaterThan, calculationUnit3);
        /// SingleCondition simpleCondition4 = new SingleCondition(ConditionOperator.GreaterThan, calculationUnit4);
        /// MultiCondition mc1 = new MultiCondition(LogicOperator.And, new SingleCondition[2] { simpleCondition1, simpleCondition2 }, 111, 222);
        /// MultiCondition mc2 = new MultiCondition(LogicOperator.And, new SingleCondition[2] { simpleCondition3, simpleCondition4 }, 333, 444);
        /// Switch switch = new Switch(new MultiCondition[2] { mc1, mc2 });
        /// </code>
        /// </example>
        public Switch(MultiCondition[] MultiConditions) : this(MultiConditions, "Switch result name") { }

        /// <summary>
        /// A counstructor for <c>Switch</c> class
        /// </summary>
        /// <param name="MultiConditions"> An array of <c>MultiCondition</c> objects to be used as switch components.</param>
        /// <param name="resultValueName">A value to be used as string identifier for a switch result.</param>
        /// <example>
        /// <code>
        /// CalculationUnit calculationUnit1 = new CalculationUnit(-10M, "TestValueName1");
        /// CalculationUnit calculationUnit2 = new CalculationUnit(12M, "TestValueName2");
        /// CalculationUnit calculationUnit3 = new CalculationUnit(-8M, "TestValueName3");
        /// CalculationUnit calculationUnit4 = new CalculationUnit(14M, "TestValueName4");
        /// SingleCondition simpleCondition1 = new SingleCondition(ConditionOperator.GreaterThan, calculationUnit1);
        /// SingleCondition simpleCondition2 = new SingleCondition(ConditionOperator.GreaterThan, calculationUnit2);
        /// SingleCondition simpleCondition3 = new SingleCondition(ConditionOperator.GreaterThan, calculationUnit3);
        /// SingleCondition simpleCondition4 = new SingleCondition(ConditionOperator.GreaterThan, calculationUnit4);
        /// MultiCondition mc1 = new MultiCondition(LogicOperator.And, new SingleCondition[2] { simpleCondition1, simpleCondition2 }, 111, 222);
        /// MultiCondition mc2 = new MultiCondition(LogicOperator.And, new SingleCondition[2] { simpleCondition3, simpleCondition4 }, 333, 444);
        /// Switch switch = new Switch(new MultiCondition[2] { mc1, mc2 }, "SwitchResult");
        /// </code>
        /// </example>
        public Switch(MultiCondition[] MultiConditions, string resultValueName)
        {
            this.MultiConditions = MultiConditions;
            SwitchResultName = resultValueName;
            SetResult();
        }

        /// <summary>
        /// Sets switch result.
        /// </summary>
        /// <example>
        /// <code>
        /// CalculationUnit calculationUnit1 = new CalculationUnit(-10M, "TestValueName1");
        /// CalculationUnit calculationUnit2 = new CalculationUnit(12M, "TestValueName2");
        /// CalculationUnit calculationUnit3 = new CalculationUnit(-8M, "TestValueName3");
        /// CalculationUnit calculationUnit4 = new CalculationUnit(14M, "TestValueName4");
        /// SingleCondition simpleCondition1 = new SingleCondition(ConditionOperator.GreaterThan, calculationUnit1);
        /// SingleCondition simpleCondition2 = new SingleCondition(ConditionOperator.GreaterThan, calculationUnit2);
        /// SingleCondition simpleCondition3 = new SingleCondition(ConditionOperator.GreaterThan, calculationUnit3);
        /// SingleCondition simpleCondition4 = new SingleCondition(ConditionOperator.GreaterThan, calculationUnit4);
        /// MultiCondition mc1 = new MultiCondition(LogicOperator.And, new SingleCondition[2] { simpleCondition1, simpleCondition2 }, 111, 222);
        /// MultiCondition mc2 = new MultiCondition(LogicOperator.And, new SingleCondition[2] { simpleCondition3, simpleCondition4 }, 333, 444);
        /// Switch switchObject = new Switch(new MultiCondition[2] { mc1, mc2 }, "SwitchResult");
        /// switchObject.SetResult();
        /// //Output: 111 since the first multicondition returns "true".
        /// </code>
        /// </example>
        public void SetResult()
        {
            SwitchValue = MultiConditions
                .Where(x => x.ConditionResult)
                .Select(x=>x.ConditionValue)
                .FirstOrDefault();
        }
    }
}
