using Microsoft.VisualStudio.TestTools.UnitTesting;

using CalculatorLoggerLibrary.Models;
using CalculatorLoggerLibrary.Models.Operations;

namespace CalculatorLoggerTests.ModelsTests
{
    [TestClass]
    public class MultiConditionTest
    {
        SingleCondition sc1;
        SingleCondition sc2;

        CalculationUnit cu1;
        CalculationUnit cu2;

        CalculationUnit valueToCompare1;
        CalculationUnit valueToCompare2;
        CalculationUnit valueToCompare3;
        CalculationUnit valueToCompare4;

        MultiCondition mc;

        [TestInitialize]
        public void TestInitialize()
        {
            cu1 = new CalculationUnit(5.0M, "Sample1");
            sc1 = new SingleCondition(ConditionOperator.GreaterThan, cu1, 10, -10);

            cu2 = new CalculationUnit(10.0M, "Sample2");
            sc2 = new SingleCondition(ConditionOperator.Equal, cu2, 10, -10);

            valueToCompare1 = new CalculationUnit(5.0M, "ValueToCompare1");
            valueToCompare2 = new CalculationUnit(10.0M, "ValueToCompare2");
            valueToCompare3 = new CalculationUnit(4.0M, "ValueToCompare3");
            valueToCompare4 = new CalculationUnit(9.0M, "ValueToCompare3");

            mc = new MultiCondition(LogicOperator.And, new SingleCondition[] { sc1, sc2 }, 11, -11);
        }

        [TestMethod]
        public void ShouldSetCorrectValuesForType()
        {
            CollectionAssert.AreEqual(new SingleCondition[] { sc1, sc2 }, mc.SingleConditions);
            Assert.AreEqual(11, mc.ValueIfTrue);
            Assert.AreEqual(-11, mc.ValueIfFalse);
            Assert.AreEqual(LogicOperator.And, mc.LogicOperator);
            Assert.AreEqual(0M, mc.ConditionValue);
            Assert.IsFalse(mc.ConditionResult);            
        }

        [TestMethod]
        public void ShouldSetCorrectResult_And_False()
        {
            mc.SetResult(valueToCompare1);
            Assert.AreEqual(-11M, mc.ConditionValue);
            Assert.IsFalse(mc.ConditionResult);
        }

        [TestMethod]
        public void ShouldSetCorrectResult_And_True()
        {
            mc.SetResult(valueToCompare2);
            Assert.AreEqual(11M, mc.ConditionValue);
            Assert.IsTrue(mc.ConditionResult);
        }

        [TestMethod]
        public void ShouldSetCorrectResult_Or_False()
        {
            mc.LogicOperator = LogicOperator.Or;
            mc.SetResult(valueToCompare3);
            Assert.AreEqual(-11M, mc.ConditionValue);
            Assert.IsFalse(mc.ConditionResult);
        }

        [TestMethod]
        public void ShouldSetCorrectResult_Or_True()
        {
            mc.LogicOperator = LogicOperator.Or;
            mc.SetResult(valueToCompare4);
            Assert.AreEqual(11M, mc.ConditionValue);
            Assert.IsTrue(mc.ConditionResult);
        }
    }
}
