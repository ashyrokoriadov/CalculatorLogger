using System.Collections.Generic;
using Calculator.Abstractions.Conditions;
using Calculator.Abstractions.Switch;
using Calculator.Conditions;
using Calculator.Enums;
using Calculator.Models;
using Calculator.Switches;
using NUnit.Framework;

namespace CalculatorTests.Switches
{
    class SwitchTests
    {
        private readonly Dictionary<IOrderedCondition<int, CalculatorValue>, decimal> _switchData;

        public SwitchTests()
        {
            var condition1 = new Condition(new CalculatorValue(10M), LogicalOperator.Greater);
            var condition2 = new Condition(new CalculatorValue(20M), LogicalOperator.LessOrEqual);
            var condition3 = new Condition(new CalculatorValue(12M), LogicalOperator.Less);
            var condition4 = new Condition(new CalculatorValue(2M), LogicalOperator.GreaterOrEqual);

            var multiCondition1 = new MultiCondition(new[] { condition1, condition2 }, BooleanOperator.And);
            var multiCondition2 = new MultiCondition(new[] { condition3, condition4 }, BooleanOperator.And);

            _switchData = new Dictionary<IOrderedCondition<int, CalculatorValue>, decimal>
            {
                { new OrderedCondition(multiCondition2, 1), 1 },
                { new OrderedCondition(multiCondition1, 0), 0 }
            };
        }

        [Test]
        public void Has_to_return_correct_value_regular_order()
        {
            ISwitch<CalculatorValue> systemUnderTests = new Switch<int>(_switchData, -128M);

            var valueToCompare1 = new CalculatorValue(11M);
            var valueToCompare2 = new CalculatorValue(9M);

            Assert.AreEqual(0M, systemUnderTests.Evaluate(valueToCompare1));
            Assert.AreEqual(1M, systemUnderTests.Evaluate(valueToCompare2));
        }

        [Test]
        public void Has_to_return_correct_default_value()
        {
            ISwitch<CalculatorValue> systemUnderTests = new Switch<int>(_switchData, -128M);

            var valueToCompare = new CalculatorValue(22M);

            Assert.AreEqual(-128M, systemUnderTests.Evaluate(valueToCompare));
        }

        [Test]
        public void Has_to_return_correct_value_irregular_order()
        {
            ISwitch<CalculatorValue> systemUnderTests = new Switch<int>(_switchData, -128M);

            var valueToCompare = new CalculatorValue(11M);

            Assert.AreEqual(0M, systemUnderTests.Evaluate(valueToCompare));
        }
    }
}