using Microsoft.VisualStudio.TestTools.UnitTesting;

using CalculatorLoggerLibrary.Models;
using CalculatorLoggerLibrary.Models.Operations;

namespace CalculatorLoggerTests.ModelsTests
{
    [TestClass]
    public class SwitchTest
    {
        SingleCondition sc1;
        SingleCondition sc2;
        SingleCondition sc3;
        SingleCondition sc4;

        CalculationUnit cu1;
        CalculationUnit cu2;
        CalculationUnit cu3;
        CalculationUnit cu4;

        CalculationUnit valueToCompare1;
        CalculationUnit valueToCompare2;
        CalculationUnit valueToCompare3;

        MultiCondition mc1;
        MultiCondition mc2;

        Switch s1;
        Switch s2;
        Switch s3;

        [TestInitialize]
        public void TestInitialize()
        {
            cu1 = new CalculationUnit(5M, "Value1");
            sc1 = new SingleCondition(ConditionOperator.GreaterThan, cu1, 1, -1);

            cu2 = new CalculationUnit(10M, "Value2");
            sc2 = new SingleCondition(ConditionOperator.LessThan, cu2, 2, -2);

            cu3 = new CalculationUnit(7.5M, "Value3");
            sc3 = new SingleCondition(ConditionOperator.GreaterThanOrEqual, cu3, 3, -3);

            cu4 = new CalculationUnit(12.5M, "Value4");
            sc4 = new SingleCondition(ConditionOperator.LessThanOrEqual, cu4, 4, -4);

            valueToCompare1 = new CalculationUnit(7M, "TestValue1");
            valueToCompare2 = new CalculationUnit(11M, "TestValue2");
            valueToCompare3 = new CalculationUnit(11M, "TestValue3");

            mc1 = new MultiCondition(LogicOperator.And, new SingleCondition[] { sc1, sc2 }, 12, -12);
            mc2 = new MultiCondition(LogicOperator.Or, new SingleCondition[] { sc3, sc4 }, 34, -34);

            s1 = new Switch(new MultiCondition[] { mc1, mc2 });
        }

        [TestMethod]
        public void ShouldSetCorrectDefaultValuesForType()
        {
            CollectionAssert.AreEqual(new MultiCondition[] { mc1, mc2 }, s1.MultiConditions);
            Assert.AreEqual(0M, s1.SwitchValue);
            Assert.AreEqual("Switch result name", s1.SwitchResultName);
        }

        [TestMethod]
        public void ShouldSetCorrectResult()
        {
            mc1.SetResult(valueToCompare1);
            mc2.SetResult(valueToCompare2);
            s2 = new Switch (new MultiCondition[] { mc1, mc2 });

            Assert.AreEqual(12M, s2.SwitchValue);
        }

        [TestMethod]
        public void ShouldSetCorrectResultUsingSecondMultiCondition()
        {
            mc1.SetResult(valueToCompare3);
            mc2.SetResult(valueToCompare2);
            s3 = new Switch(new MultiCondition[] { mc1, mc2 });

            Assert.AreEqual(34M, s3.SwitchValue);
        }
    }
}
