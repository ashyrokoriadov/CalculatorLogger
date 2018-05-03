using Microsoft.VisualStudio.TestTools.UnitTesting;

using CalculatorLoggerLibrary.Models;
using CalculatorLoggerLibrary.Models.Operations;

namespace CalculatorLoggerTests.ModelsTests
{
    [TestClass]
    public class SingleConditionTest
    {
        SingleCondition sc1;
        SingleCondition sc2;

        CalculationUnit cu1;
        CalculationUnit cu2;

        CalculationUnit valueToCompare1;
        CalculationUnit valueToCompare2;

        [TestInitialize]
        public void TestInitialize()
        {
            cu1 = new CalculationUnit(5.0M, "Sample1");
            sc1 = new SingleCondition(ConditionOperator.GreaterThan, cu1, 10, -10);

            cu2 = new CalculationUnit(15.0M, "Sample2");
            sc2 = new SingleCondition(ConditionOperator.Equal, cu2, 10, -10);

            valueToCompare1 = new CalculationUnit(5.0M, "ValueToCompare1");
            valueToCompare2 = new CalculationUnit(10.0M, "ValueToCompare2");
        }

        [TestMethod]
        public void ShouldSetCorrectValuesForType()
        {
            Assert.AreEqual(cu1, sc1.Sample);
            Assert.AreEqual(10, sc1.ValueIfTrue);
            Assert.AreEqual(-10, sc1.ValueIfFalse);
            Assert.IsNull(sc1.ValueToCompare);
            Assert.AreEqual(ConditionOperator.GreaterThan, sc1.ConditionOperator);
            Assert.AreEqual(0M, sc1.ConditionValue);
            Assert.IsFalse(sc1.ConditionResult);

            Assert.AreEqual(cu2, sc2.Sample);
            Assert.AreEqual(10, sc2.ValueIfTrue);
            Assert.AreEqual(-10, sc2.ValueIfFalse);
            Assert.IsNull(sc2.ValueToCompare);
            Assert.AreEqual(ConditionOperator.Equal, sc2.ConditionOperator);
            Assert.AreEqual(0M, sc2.ConditionValue);
            Assert.IsFalse(sc2.ConditionResult);
        }

        [TestMethod]
        public void ShouldSetCorrectResult_GreaterThan()
        {
            sc1.SetResult(valueToCompare1);
            Assert.AreEqual(-10M, sc1.ConditionValue);
            Assert.IsFalse(sc1.ConditionResult);

            sc1.SetResult(valueToCompare2);
            Assert.AreEqual(10M, sc1.ConditionValue);
            Assert.IsTrue(sc1.ConditionResult);
        }

        [TestMethod]
        public void ShouldSetCorrectResult_GreaterThanOrEqual()
        {
            sc1.ConditionOperator = ConditionOperator.GreaterThanOrEqual;

            sc1.SetResult(valueToCompare1);
            Assert.AreEqual(10M, sc1.ConditionValue);
            Assert.IsTrue(sc1.ConditionResult);

            sc1.SetResult(valueToCompare2);
            Assert.AreEqual(10M, sc1.ConditionValue);
            Assert.IsTrue(sc1.ConditionResult);
        }

        [TestMethod]
        public void ShouldSetCorrectResult_LessThan()
        {
            sc1.ConditionOperator = ConditionOperator.LessThan;

            sc1.SetResult(valueToCompare1);
            Assert.AreEqual(-10M, sc1.ConditionValue);
            Assert.IsFalse(sc1.ConditionResult);

            sc1.SetResult(valueToCompare2);
            Assert.AreEqual(-10M, sc1.ConditionValue);
            Assert.IsFalse(sc1.ConditionResult);
        }

        [TestMethod]
        public void ShouldSetCorrectResult_LessThanOrEqual()
        {
            sc1.ConditionOperator = ConditionOperator.LessThanOrEqual;

            sc1.SetResult(valueToCompare1);
            Assert.AreEqual(10M, sc1.ConditionValue);
            Assert.IsTrue(sc1.ConditionResult);

            sc1.SetResult(valueToCompare2);
            Assert.AreEqual(-10M, sc1.ConditionValue);
            Assert.IsFalse(sc1.ConditionResult);
        }

        [TestMethod]
        public void ShouldSetCorrectResult_Equal()
        {
            sc1.ConditionOperator = ConditionOperator.Equal;
            sc1.SetResult(valueToCompare1);
            Assert.AreEqual(10M, sc1.ConditionValue);
            Assert.IsTrue(sc1.ConditionResult);

            sc2.SetResult(valueToCompare2);
            Assert.AreEqual(-10M, sc2.ConditionValue);
            Assert.IsFalse(sc2.ConditionResult);
        }
    }
}
