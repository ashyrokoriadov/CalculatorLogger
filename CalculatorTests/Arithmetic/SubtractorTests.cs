using System.Collections.Generic;
using System.Linq;
using Calculator.Arithmetic;
using LoggingCalculator.AbstractionsAndModels.Arithmetic;
using LoggingCalculator.AbstractionsAndModels.Models;
using NUnit.Framework;

namespace CalculatorTests.Arithmetic
{
    public class SubtractorTests : ArithmeticTestsBase<ISubtractor<CalculatorValue>>
    {
        protected override void InitializeSystemUnderTests()
        {
            SystemUnderTests = new Subtractor();
        }

        [TestCaseSource(nameof(SubtractionTwoValuesCaseSource))]
        public void Has_to_return_correct_value_for_subtraction_of_two_values(decimal valueX, decimal valueY, decimal expected)
        {
            var calculatorValueX = new CalculatorValue(valueX, nameof(valueX));
            var calculatorValueY = new CalculatorValue(valueY, nameof(valueY));
            var expectedResult = new CalculatorValue(expected, $"{nameof(valueX)} - {nameof(valueY)}");

            var actualResult = SystemUnderTests.Subtract(calculatorValueX, calculatorValueY);
            Assert.AreEqual(expectedResult.Value, actualResult.Value);
            Assert.AreEqual(expectedResult.Name, actualResult.Name);
        }

        [TestCaseSource(nameof(SubtractionSeveralValuesCaseSource))]
        public void Has_to_return_correct_value_for_subtraction_of_several_values(IEnumerable<decimal> values
            , decimal minuend
            , decimal expected
            , string name
            , string expectedName)
        {
            var minuendValue = new CalculatorValue(minuend, "value");
            var calculatorValues = values.Select(v => new CalculatorValue(v, name));
            var actualResult = SystemUnderTests.Subtract(calculatorValues, minuendValue);
            var expectedResult = new CalculatorValue(expected, expectedName);
            Assert.AreEqual(expectedResult.Value, actualResult.Value);
            Assert.AreEqual(expectedResult.Name, actualResult.Name);
        }

        static readonly object[] SubtractionTwoValuesCaseSource =
        {
            new object[] {2.0M, 3.0M, -1.0M },
            new object[] {-2.0M, -3.0M, 1.0M },
            new object[] { -2.0M, 3.0M, -5.0M },
            new object[] { -2.0M, 0.0M, -2.0M }
        };

        static readonly object[] SubtractionSeveralValuesCaseSource =
        {
            new object[] {new []{1.0M, 2.0M}, 4.0M, 1.0M, "value", "value - value - value" },
            new object[] {new []{1.0M, 2.0M, 3.0M}, 4.0M, -2.0M,  "value", "value - value - value - value"  },
            new object[] {new []{-2.0M, 3.0M, -4.0M, 5.0M}, 4.0M, 2.0M, "value", "value - value - value - value - value" },
            new object[] {new []{2.0M, 0.0M, -2.0M, 3.0M}, 4.0M, 1.0M, "value", "value - value - value - value - value" }
        };
    }
}
