using Calculator.Abstractions.Validators;
using Calculator.Models;
using Calculator.Validators;
using NUnit.Framework;

namespace CalculatorTests.Validators
{
    public class IsNullValidatorTests
    {
        private IValidator<CalculatorValue> _validatorUnderTests;

        [SetUp]
        public void Setup()
        {
            _validatorUnderTests = new IsNullValidator<CalculatorValue>();
        }

        [TestCaseSource(nameof(EmptyArrayValidationCaseSource))]
        public void Has_to_return_true_for_null_value(CalculatorValue array, bool validationResult)
        {
            Assert.AreEqual(validationResult, _validatorUnderTests.Validate(array));
        }

        static readonly object[] EmptyArrayValidationCaseSource =
        {
            new object[] {null, true },
            new object[] {new CalculatorValue(1.0M), false }
        };
    }
}
