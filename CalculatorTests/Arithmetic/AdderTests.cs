using System.Collections.Generic;
using System.Linq;
using Calculator.Arithmetic;
using LoggingCalculator.AbstractionsAndModels.Arithmetic;
using LoggingCalculator.AbstractionsAndModels.Models;
using NUnit.Framework;

namespace CalculatorTests.Arithmetic
{
    public class AdderTests : ArithmeticTestsBase<IAdder<CalculatorValue>>
    {
        protected override void InitializeSystemUnderTests()
        {
            SystemUnderTests = new Adder();
        }

        [TestCaseSource(nameof(SumTwoValuesCaseSource))]
        public void Has_to_return_correct_value_for_sum_of_two_values(decimal valueX, decimal valueY, decimal expected)
        {
            var calculatorValueX = new CalculatorValue(valueX, nameof(valueX));
            var calculatorValueY = new CalculatorValue(valueY, nameof(valueY));
            var expectedResult = new CalculatorValue(expected, $"{nameof(valueX)} + {nameof(valueY)}");

            var actualResult = SystemUnderTests.Add(calculatorValueX, calculatorValueY);
            Assert.AreEqual(expectedResult.Value, actualResult.Value);
            Assert.AreEqual(expectedResult.Name, actualResult.Name);
        }

        [TestCaseSource(nameof(SumSeveralValuesCaseSource))]
        public void Has_to_return_correct_value_for_sum_of_several_values(IEnumerable<decimal> values
            , decimal expected
            , string name
            , string expectedName)
        {
            var calculatorValues = values.Select(v => new CalculatorValue(v, name));
            var actualResult = SystemUnderTests.Add(calculatorValues);
            var expectedResult = new CalculatorValue(expected, expectedName);
            Assert.AreEqual(expectedResult.Value, actualResult.Value);
            Assert.AreEqual(expectedResult.Name, actualResult.Name);
        }

        static readonly object[] SumTwoValuesCaseSource =
        {
            new object[] {2.0M, 3.0M, 5.0M },
            new object[] {-2.0M, -3.0M, -5.0M },
            new object[] { -2.0M, 3.0M, 1.0M },
            new object[] { -2.0M, 0.0M, -2.0M }
        };

        static readonly object[] SumSeveralValuesCaseSource =
        {
            new object[] {new []{1.0M, 2.0M}, 3.0M, "value", "value + value" },
            new object[] {new []{1.0M, 2.0M, 3.0M}, 6.0M,  "value", "value + value + value"  },
            new object[] {new []{2.0M, 3.0M, 4.0M, 5.0M}, 14.0M, "value", "value + value + value + value" },
            new object[] {new []{2.0M, 0.0M, -2.0M, 3.0M}, 3.0M, "value", "value + value + value + value" }
        };
    }
}