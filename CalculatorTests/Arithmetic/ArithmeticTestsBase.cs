using NUnit.Framework;

namespace CalculatorTests.Arithmetic
{
    public abstract class ArithmeticTestsBase<T>
    {
        protected T SystemUnderTests;

        protected ArithmeticTestsBase()
        {
            InitializeSystemUnderTests();
        }

        [SetUp]
        public virtual void Setup()
        {
            InitializeSystemUnderTests();
        }

        protected abstract void InitializeSystemUnderTests();
    }
}