using System.Collections.Generic;
using System.Linq;
using Calculator.Arithmetic;
using LoggingCalculator.AbstractionsAndModels.Arithmetic;
using LoggingCalculator.AbstractionsAndModels.Models;
using NUnit.Framework;

namespace CalculatorTests.Arithmetic
{
    public class MultiplierTests : ArithmeticTestsBase<IMultiplier<CalculatorValue>>
    {
        protected override void InitializeSystemUnderTests()
        {
            SystemUnderTests = new Multiplier();
        }

        [TestCaseSource(nameof(ProductTwoValuesCaseSource))]
        public void Has_to_return_correct_value_for_product_of_two_values(decimal valueX, decimal valueY, decimal expected)
        {
            var calculatorValueX = new CalculatorValue(valueX, nameof(valueX));
            var calculatorValueY = new CalculatorValue(valueY, nameof(valueY));
            var expectedResult = new CalculatorValue(expected, $"{nameof(valueX)} * {nameof(valueY)}");

            var actualResult = SystemUnderTests.Multiply(calculatorValueX, calculatorValueY);
            Assert.AreEqual(expectedResult.Value, actualResult.Value);
            Assert.AreEqual(expectedResult.Name, actualResult.Name);
        }

        [TestCaseSource(nameof(ProductSeveralValuesCaseSource))]
        public void Has_to_return_correct_value_for_product_of_several_values(IEnumerable<decimal> values
            , decimal expected
            , string name
            , string expectedName)
        {
            var calculatorValues = values.Select(v => new CalculatorValue(v, name));
            var actualResult = SystemUnderTests.Multiply(calculatorValues);
            var expectedResult = new CalculatorValue(expected, expectedName);
            Assert.AreEqual(expectedResult.Value, actualResult.Value);
            Assert.AreEqual(expectedResult.Name, actualResult.Name);
        }

        static readonly object[] ProductTwoValuesCaseSource =
        {
            new object[] {2.0M, 3.0M, 6.0M },
            new object[] {-2.0M, -3.0M, 6.0M },
            new object[] { -2.0M, 3.0M, -6.0M },
            new object[] { -2.0M, 0.0M, 0.0M }
        };

        static readonly object[] ProductSeveralValuesCaseSource =
        {
            new object[] {new []{1.0M, 2.0M}, 2.0M, "value", "value * value" },
            new object[] {new []{1.0M, 2.0M, 3.0M}, 6.0M,  "value", "value * value * value"  },
            new object[] {new []{2.0M, 3.0M, 4.0M, 5.0M}, 120.0M, "value", "value * value * value * value" },
            new object[] {new []{2.0M, 0.0M, -2.0M, 3.0M}, 0.0M, "value", "value * value * value * value" }
        };
    }
}
