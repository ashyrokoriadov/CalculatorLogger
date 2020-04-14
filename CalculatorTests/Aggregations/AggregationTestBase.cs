using LoggingCalculator.AbstractionsAndModels.Models;
using NUnit.Framework;

namespace CalculatorTests.Aggregations
{
    public abstract class AggregationTestBase<T>
    {
        protected T SystemUnderTests;

        protected AggregationTestBase()
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
