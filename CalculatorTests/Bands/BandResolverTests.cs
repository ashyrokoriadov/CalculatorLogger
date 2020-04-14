using System.Collections.Generic;
using LoggingCalculator.AbstractionsAndModels.Bands;
using LoggingCalculator.AbstractionsAndModels.Models;
using NUnit.Framework;

namespace CalculatorTests.Bands
{
    abstract class BandResolverTests
    {
        protected IBandResolver<CalculatorValue> SystemUnderTests;

        [SetUp]
        public virtual void Setup()
        {
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
