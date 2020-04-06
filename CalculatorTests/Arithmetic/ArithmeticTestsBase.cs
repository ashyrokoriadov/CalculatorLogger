using Calculator.Abstractions.Arithmetic;
using Calculator.Abstractions.Validators;
using Calculator.Arithmetic;
using Calculator.Models;
using NUnit.Framework;

namespace CalculatorTests.Arithmetic
{
    public abstract class ArithmeticTestsBase<T>
    {
        protected T SystemUnderTests;
        protected IArithmeticValidator<CalculatorValue> ArithmeticValidatorMock;

        protected ArithmeticTestsBase()
        {
            InitializeSystemUnderTests();
        }

        [SetUp]
        public virtual void Setup()
        {
            if (ArithmeticValidatorMock == null)
                ArithmeticValidatorMock = Mocks.ArithmeticValidatorMock.GetMock();
            InitializeSystemUnderTests();
        }

        protected abstract void InitializeSystemUnderTests();
    }
}