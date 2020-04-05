using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calculator.Abstractions.Arithmetic;
using Calculator.Abstractions.Validators;
using Calculator.Arithmetic;
using Calculator.Models;
using CalculatorTests.Mocks;
using NUnit.Framework;

namespace CalculatorTests.Arithmetic
{
    public class AdderTests
    {
        private IAdder<CalculatorValue> _adderUnderTests;
        private IArithmeticValidator<CalculatorValue> _arithmeticValidatorMock;

        [SetUp]
        public void Setup()
        {
            if(_arithmeticValidatorMock == null)
                _arithmeticValidatorMock = ArithmeticValidatorMock.GetMock();
            _adderUnderTests = new Adder(_arithmeticValidatorMock);
        }

        [TestCaseSource(nameof(SumTwoValuesCaseSource))]
        public void Has_to_return_correct_value_for_sum_of_two_values(decimal valueX, decimal valueY, decimal expected)
        {
            var calculatorValueX = new CalculatorValue(valueX, nameof(valueX));
            var calculatorValueY = new CalculatorValue(valueY, nameof(valueY));
            var expectedResult = new CalculatorValue(expected, $"{nameof(valueX)} + {nameof(valueY)}");

            var actualResult = _adderUnderTests.Add(calculatorValueX, calculatorValueY);
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
            var actualResult = _adderUnderTests.Add(calculatorValues);
            var expectedResult = new CalculatorValue(expected, expectedName);
            Assert.AreEqual(expectedResult.Value, actualResult.Value);
            Assert.AreEqual(expectedResult.Name, actualResult.Name);
        }

        [TestCaseSource(nameof(SumTwoValuesNullCaseSource))]
        public void Has_to_return_empty_object_if_empty_array_is_passed(CalculatorValue valueX, CalculatorValue valueY)
        {
            var actualResult = _adderUnderTests.Add(valueX, valueY);
            var expectedResult = CalculatorValue.Empty();
            Assert.AreEqual(expectedResult.Value, actualResult.Value);
            Assert.AreEqual(expectedResult.Name, actualResult.Name);
        }

        [TestCaseSource(nameof(SumSeveralValuesNullCaseSource))]
        public void Has_to_return_empty_object_if_empty_values_are_passed(IEnumerable<CalculatorValue> emptyOrNullEnumerable)
        {
            var expectedResult = CalculatorValue.Empty();
            var actualResult = _adderUnderTests.Add(emptyOrNullEnumerable);
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

        static readonly object[] SumTwoValuesNullCaseSource =
        {
            new object[] {new CalculatorValue(2.0M), null },
            new object[] {null, null }
        };

        static readonly object[] SumSeveralValuesNullCaseSource =
        {
            new object[] {Enumerable.Empty<CalculatorValue>() },
            new object[] {null }
        };
    }
}