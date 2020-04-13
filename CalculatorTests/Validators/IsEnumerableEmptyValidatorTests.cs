using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calculator.Abstractions.Arithmetic;
using Calculator.Abstractions.Validators;
using Calculator.Arithmetic;
using Calculator.Models;
using Calculator.Validators;
using NUnit.Framework;

namespace CalculatorTests.Validators
{
    public class IsEnumerableEmptyValidatorTests
    {
        private IValidator<IEnumerable<CalculatorValue>> _validatorUnderTests;

        [SetUp]
        public void Setup()
        {
            _validatorUnderTests = new IsEnumerableEmptyValidator<IEnumerable<CalculatorValue>>();
        }

        [TestCaseSource(nameof(EmptyArrayValidationCaseSource))]
        public void Has_to_return_true_for_empty_array(IEnumerable<CalculatorValue> array, bool validationResult)
        {
            Assert.AreEqual(validationResult, _validatorUnderTests.Validate(array));
        }

        static readonly object[] EmptyArrayValidationCaseSource =
        {
            new object[] {Enumerable.Empty<CalculatorValue>(), true },
            new object[] {new []{new CalculatorValue(1.0M)}, false },
            new object[] {new []{new CalculatorValue(1.0M), new CalculatorValue(3.0M) }, false },
        };
    }
}
