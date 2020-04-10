using System;
using System.Collections.Generic;
using System.Text;
using Calculator.Abstractions.Bands;
using Calculator.Abstractions.Validators;
using Calculator.Bands;
using Calculator.Models;
using NUnit.Framework;

namespace CalculatorTests.Bands
{
    abstract class BandResolverTests
    {
        protected IBandResolver<CalculatorValue> SystemUnderTests;
        protected IArithmeticValidator<CalculatorValue> ArithmeticValidatorMock;

        [SetUp]
        public virtual void Setup()
        {
            if (ArithmeticValidatorMock == null)
                ArithmeticValidatorMock = Mocks.ArithmeticValidatorMock.GetMock();

            var bands = new Dictionary<decimal, decimal>(new[]
            {
                new KeyValuePair<decimal, decimal>(0M, 1.25M),
                new KeyValuePair<decimal, decimal>(10000M, 1.75M),
                new KeyValuePair<decimal, decimal>(15000M, 2.25M),
                new KeyValuePair<decimal, decimal>(25000M, 3.50M),
                new KeyValuePair<decimal, decimal>(40000M, 4.30M)
            });

            InitializeSystemUnderTests(bands);
        }

        protected abstract void InitializeSystemUnderTests(Dictionary<decimal, decimal> bands);
    }
}
