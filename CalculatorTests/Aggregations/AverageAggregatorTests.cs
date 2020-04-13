using System.Collections.Generic;
using System.Linq;
using Calculator.Abstractions.Aggregations;
using Calculator.Aggregations;
using Calculator.Models;
using NUnit.Framework;

namespace CalculatorTests.Aggregations
{
    public class AverageAggregatorTests : AggregationTestBase<IAverageAggregator<CalculatorValue>>
    {
        protected override void InitializeSystemUnderTests()
        {
            SystemUnderTests = new AverageAggregator(ArithmeticValidatorMock);
        }

        [TestCaseSource(nameof(AverageTwoValuesCaseSource))]
        public void Has_to_return_correct_value_for_average_of_two_values(decimal valueX, decimal valueY, decimal expected)
        {
            var calculatorValueX = new CalculatorValue(valueX, nameof(valueX));
            var calculatorValueY = new CalculatorValue(valueY, nameof(valueY));
            var expectedResult = new CalculatorValue(expected, $"{nameof(SystemUnderTests.Average)}: " +
                                                               $"{expected} of values: {valueX}, {valueY}.");

            var actualResult = SystemUnderTests.Average(calculatorValueX, calculatorValueY);
            Assert.AreEqual(expectedResult.Value, actualResult.Value);
            Assert.AreEqual(expectedResult.Name, actualResult.Name);
        }

        [TestCaseSource(nameof(AverageSeveralValuesCaseSource))]
        public void Has_to_return_correct_value_for_average_of_several_values(IEnumerable<decimal> values
            , decimal expected
            , string expectedName)
        {
            var calculatorValues = values.Select(v => new CalculatorValue(v));
            var actualResult = SystemUnderTests.Average(calculatorValues);
            var expectedResult = new CalculatorValue(expected, expectedName);
            Assert.AreEqual(expectedResult.Value, actualResult.Value);
            Assert.AreEqual(expectedResult.Name, actualResult.Name);
        }

        [TestCaseSource(nameof(AverageTwoValuesNullCaseSource))]
        public void Has_to_return_empty_object_if_empty_array_is_passed(CalculatorValue valueX, CalculatorValue valueY)
        {
            var actualResult = SystemUnderTests.Average(valueX, valueY);
            var expectedResult = CalculatorValue.Empty();
            Assert.AreEqual(expectedResult.Value, actualResult.Value);
            Assert.AreEqual(expectedResult.Name, actualResult.Name);
        }

        [TestCaseSource(nameof(AverageSeveralValuesNullCaseSource))]
        public void Has_to_return_empty_object_if_empty_values_are_passed(IEnumerable<CalculatorValue> emptyOrNullEnumerable)
        {
            var expectedResult = CalculatorValue.Empty();
            var actualResult = SystemUnderTests.Average(emptyOrNullEnumerable);
            Assert.AreEqual(expectedResult.Value, actualResult.Value);
            Assert.AreEqual(expectedResult.Name, actualResult.Name);
        }

        static readonly object[] AverageTwoValuesCaseSource =
        {
            new object[] {2.0M, 4.0M, 3.0M },
            new object[] {-6.0M, -3.0M, -4.5M },
            new object[] { -2.0M, 2.0M, 0.0M },
            new object[] { -2.0M, 6.0M, 2.0M }
        };

        static readonly object[] AverageSeveralValuesCaseSource =
        {
            new object[] {new []{1.0M, 5.0M}, 3.0M, "Average: 3,0 of values: 1,0, 5,0." },
            new object[] {new []{0.0M, 5.0M, 10.0M}, 5.0M,  "Average: 5,0 of values: 0,0, 5,0, 10,0."  },
            new object[] {new []{2.0M, 3.0M, 4.0M, 5.0M}, 3.5M, "Average: 3,5 of values: 2,0, 3,0, 4,0, 5,0." },
            new object[] {new []{2.0M, 0.0M, -2.0M, 0.0M}, 0.0M, "Average: 0,0 of values: 2,0, 0,0, -2,0, 0,0." }
        };

        static readonly object[] AverageTwoValuesNullCaseSource =
        {
            new object[] {new CalculatorValue(2.0M), null },
            new object[] {null, null }
        };

        static readonly object[] AverageSeveralValuesNullCaseSource =
        {
            new object[] {Enumerable.Empty<CalculatorValue>() },
            new object[] {null }
        };
    }
}
