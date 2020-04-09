using System.Collections.Generic;
using System.Linq;
using Calculator.Abstractions.Aggregations;
using Calculator.Aggregations;
using Calculator.Models;
using NUnit.Framework;

namespace CalculatorTests.Aggregations
{
    public class MaxAggregatorTests : AggregationTestBase<IMaxAggregator<CalculatorValue>>
    {
        protected override void InitializeSystemUnderTests()
        {
            SystemUnderTests = new MaxAggregator(ArithmeticValidatorMock);
        }

        [TestCaseSource(nameof(MaxTwoValuesCaseSource))]
        public void Has_to_return_correct_value_for_max_of_two_values(decimal valueX, decimal valueY, decimal expected)
        {
            var calculatorValueX = new CalculatorValue(valueX, nameof(valueX));
            var calculatorValueY = new CalculatorValue(valueY, nameof(valueY));
            var expectedResult = new CalculatorValue(expected, $"{nameof(SystemUnderTests.Max)}: " +
                                                               $"{expected} of values: {valueX}, {valueY}.");

            var actualResult = SystemUnderTests.Max(calculatorValueX, calculatorValueY);
            Assert.AreEqual(expectedResult.Value, actualResult.Value);
            Assert.AreEqual(expectedResult.Name, actualResult.Name);
        }

        [TestCaseSource(nameof(MaxSeveralValuesCaseSource))]
        public void Has_to_return_correct_value_for_max_of_several_values(IEnumerable<decimal> values
            , decimal expected
            , string expectedName)
        {
            var calculatorValues = values.Select(v => new CalculatorValue(v));
            var actualResult = SystemUnderTests.Max(calculatorValues);
            var expectedResult = new CalculatorValue(expected, expectedName);
            Assert.AreEqual(expectedResult.Value, actualResult.Value);
            Assert.AreEqual(expectedResult.Name, actualResult.Name);
        }

        [TestCaseSource(nameof(MaxTwoValuesNullCaseSource))]
        public void Has_to_return_empty_object_if_empty_array_is_passed(CalculatorValue valueX, CalculatorValue valueY)
        {
            var actualResult = SystemUnderTests.Max(valueX, valueY);
            var expectedResult = CalculatorValue.Empty();
            Assert.AreEqual(expectedResult.Value, actualResult.Value);
            Assert.AreEqual(expectedResult.Name, actualResult.Name);
        }

        [TestCaseSource(nameof(MaxSeveralValuesNullCaseSource))]
        public void Has_to_return_empty_object_if_empty_values_are_passed(IEnumerable<CalculatorValue> emptyOrNullEnumerable)
        {
            var expectedResult = CalculatorValue.Empty();
            var actualResult = SystemUnderTests.Max(emptyOrNullEnumerable);
            Assert.AreEqual(expectedResult.Value, actualResult.Value);
            Assert.AreEqual(expectedResult.Name, actualResult.Name);
        }

        static readonly object[] MaxTwoValuesCaseSource =
        {
            new object[] {2.0M, 4.0M, 4.0M },
            new object[] {-6.0M, -3.0M, -3.0M },
            new object[] { -2.0M, 2.0M, 2.0M },
            new object[] { -2.0M, 6.0M, 6.0M }
        };

        static readonly object[] MaxSeveralValuesCaseSource =
        {
            new object[] {new []{1.0M, 5.0M}, 5.0M, "Max: 5,0 of values: 1,0, 5,0." },
            new object[] {new []{0.0M, 5.0M, 10.0M}, 10.0M,  "Max: 10,0 of values: 0,0, 5,0, 10,0."  },
            new object[] {new []{2.0M, 3.0M, 4.0M, 5.0M}, 5.0M, "Max: 5,0 of values: 2,0, 3,0, 4,0, 5,0." },
            new object[] {new []{2.0M, 0.0M, -2.0M, 0.0M}, 2.0M, "Max: 2,0 of values: 2,0, 0,0, -2,0, 0,0." }
        };

        static readonly object[] MaxTwoValuesNullCaseSource =
        {
            new object[] {new CalculatorValue(2.0M), null },
            new object[] {null, null }
        };

        static readonly object[] MaxSeveralValuesNullCaseSource =
        {
            new object[] {Enumerable.Empty<CalculatorValue>() },
            new object[] {null }
        };
    }
}
