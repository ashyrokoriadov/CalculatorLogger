using Calculator.Abstractions.Conditions;
using Calculator.Conditions;
using Calculator.Enums;
using Calculator.Models;
using NUnit.Framework;

namespace CalculatorTests.Conditions
{
    class MultiConditionTests
    {
        private readonly ICondition<CalculatorValue> _condition1;
        private readonly ICondition<CalculatorValue> _condition2;

        public MultiConditionTests()
        {
            _condition1 = new Condition(new CalculatorValue(10M), LogicalOperator.Greater);
            _condition2 = new Condition(new CalculatorValue(15M), LogicalOperator.Equal);
        }

        [Test]
        public void Has_to_return_correct_value_for_and_operator()
        {
            ICondition<CalculatorValue>  systemUnderTests = new MultiCondition(new[] { _condition1, _condition2 }, BooleanOperator.And);

            var valueToCompareTrue = new CalculatorValue(15M);
            var valueToCompareFalse = new CalculatorValue(16M);

            Assert.IsTrue(systemUnderTests.Evaluate(valueToCompareTrue));
            Assert.IsFalse(systemUnderTests.Evaluate(valueToCompareFalse));
        }

        [Test]
        public void Has_to_return_correct_value_for_or_operator()
        {
            ICondition<CalculatorValue> systemUnderTests = new MultiCondition(new[] { _condition1, _condition2 }, BooleanOperator.Or);

            var valueToCompareTrue1 = new CalculatorValue(15M);
            var valueToCompareTrue2 = new CalculatorValue(11M);
            var valueToCompareFalse = new CalculatorValue(10M);

            Assert.IsTrue(systemUnderTests.Evaluate(valueToCompareTrue1));
            Assert.IsTrue(systemUnderTests.Evaluate(valueToCompareTrue1));
            Assert.IsFalse(systemUnderTests.Evaluate(valueToCompareFalse));
        }

        [Test]
        public void Has_to_return_correct_value_for_not_operator()
        {
            ICondition<CalculatorValue> systemUnderTests = new MultiCondition(new[] { _condition1, _condition2 }, BooleanOperator.Not);

            var valueToCompareFalse1 = new CalculatorValue(15M);
            var valueToCompareFalse2 = new CalculatorValue(11M);
            var valueToCompareTrue = new CalculatorValue(10M);

            Assert.IsTrue(systemUnderTests.Evaluate(valueToCompareTrue));
            Assert.IsFalse(systemUnderTests.Evaluate(valueToCompareFalse1));
            Assert.IsFalse(systemUnderTests.Evaluate(valueToCompareFalse2));
        }
    }
}
