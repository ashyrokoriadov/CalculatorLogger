using System;
using System.Collections.Generic;
using System.Text;
using Calculator.Abstractions.Validators;
using Calculator.Models;
using NUnit.Framework;

namespace CalculatorTests.Aggregations
{
    public abstract class AggregationTestBase<T>
    {
        protected T SystemUnderTests;
        protected IArithmeticValidator<CalculatorValue> ArithmeticValidatorMock;

        protected AggregationTestBase()
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
