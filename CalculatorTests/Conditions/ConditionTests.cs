using System;
using System.Collections.Generic;
using System.Text;
using Calculator.Abstractions.Bands;
using Calculator.Abstractions.Conditions;
using Calculator.Abstractions.Validators;
using Calculator.Conditions;
using Calculator.Enums;
using Calculator.Models;
using NUnit.Framework;

namespace CalculatorTests.Conditions
{
    class ConditionTests
    {
        [Test]
        public void Has_to_return_correct_value_for_equal_condition()
        {
            ICondition<CalculatorValue> systemUnderTests = new Condition(new CalculatorValue(10M), LogicalOperator.Equal );

            var valueToCompareTrue= new CalculatorValue(10M);
            var valueToCompareFalse = new CalculatorValue(9M);

            Assert.IsTrue(systemUnderTests.Evaluate(valueToCompareTrue));
            Assert.IsFalse(systemUnderTests.Evaluate(valueToCompareFalse));
        }

        [Test]
        public void Has_to_return_correct_value_for_not_equal_condition()
        {
            ICondition<CalculatorValue> systemUnderTests = new Condition(new CalculatorValue(10M), LogicalOperator.NotEqual);

            var valueToCompareFalse = new CalculatorValue(10M);
            var valueToCompareTrue = new CalculatorValue(9M);

            Assert.IsTrue(systemUnderTests.Evaluate(valueToCompareTrue));
            Assert.IsFalse(systemUnderTests.Evaluate(valueToCompareFalse));
        }

        [Test]
        public void Has_to_return_correct_value_for_greater_condition()
        {
            ICondition<CalculatorValue> systemUnderTests = new Condition(new CalculatorValue(10M), LogicalOperator.Greater);

            var valueToCompareFalse = new CalculatorValue(10M);
            var valueToCompareTrue = new CalculatorValue(11M);

            Assert.IsTrue(systemUnderTests.Evaluate(valueToCompareTrue));
            Assert.IsFalse(systemUnderTests.Evaluate(valueToCompareFalse));
        }

        [Test]
        public void Has_to_return_correct_value_for_greater_or_equal_condition()
        {
            ICondition<CalculatorValue> systemUnderTests = new Condition(new CalculatorValue(10M), LogicalOperator.GreaterOrEqual);

            var valueToCompareFalse = new CalculatorValue(9M);
            var valueToCompareTrue1 = new CalculatorValue(10M);
            var valueToCompareTrue2 = new CalculatorValue(11M);

            Assert.IsTrue(systemUnderTests.Evaluate(valueToCompareTrue1));
            Assert.IsTrue(systemUnderTests.Evaluate(valueToCompareTrue2));
            Assert.IsFalse(systemUnderTests.Evaluate(valueToCompareFalse));
        }

        [Test]
        public void Has_to_return_correct_value_for_less_condition()
        {
            ICondition<CalculatorValue> systemUnderTests = new Condition(new CalculatorValue(10M), LogicalOperator.Less);

            var valueToCompareTrue = new CalculatorValue(9M);
            var valueToCompareFalse = new CalculatorValue(10M);

            Assert.IsTrue(systemUnderTests.Evaluate(valueToCompareTrue));
            Assert.IsFalse(systemUnderTests.Evaluate(valueToCompareFalse));
        }

        [Test]
        public void Has_to_return_correct_value_for_less_or_equal_condition()
        {
            ICondition<CalculatorValue> systemUnderTests = new Condition(new CalculatorValue(10M), LogicalOperator.LessOrEqual);

            var valueToCompareTrue1 = new CalculatorValue(9M);
            var valueToCompareTrue2 = new CalculatorValue(10M);
            var valueToCompareFalse = new CalculatorValue(11M);

            Assert.IsTrue(systemUnderTests.Evaluate(valueToCompareTrue1));
            Assert.IsTrue(systemUnderTests.Evaluate(valueToCompareTrue2));
            Assert.IsFalse(systemUnderTests.Evaluate(valueToCompareFalse));
        }

    }
}
