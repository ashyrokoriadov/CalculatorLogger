using System.Collections.Generic;
using Calculator.Bands;
using LoggingCalculator.AbstractionsAndModels.Models;
using NUnit.Framework;

namespace CalculatorTests.Bands
{
    class IncludingBandResolverTests : BandResolverTests
    {
        protected override void InitializeSystemUnderTests(Dictionary<decimal, decimal> bands)
        {
            SystemUnderTests = new IncludingBandResolver(bands);
        }

        [TestCaseSource(nameof(BandValuesCaseSource))]
        public void Has_to_return_correct_value_for_band(decimal value, decimal expected, string expectedName)
        {
            var calculatorValue = new CalculatorValue(value, nameof(value));
            var expectedResult = new CalculatorValue(expected, expectedName);

            var actualResult = SystemUnderTests.Resolve(calculatorValue);
            Assert.AreEqual(expectedResult.Value, actualResult.Value);
            Assert.AreEqual(expectedResult.Name, actualResult.Name);
        }

        static readonly object[] BandValuesCaseSource =
        {
            new object[] {9000M, 1.25M, "Band resolution result" },
            new object[] {9999M, 1.25M, "Band resolution result" },
            new object[] { -2.0M, 0M, "Out of band range result" },
            new object[] { 0.0M, 1.25M, "Band resolution result" },
            new object[] {10000M, 1.75M, "Band resolution result" },
            new object[] {19000M, 2.25M, "Band resolution result" },
            new object[] {12000M, 1.75M, "Band resolution result" },
            new object[] {30000M, 3.50M, "Band resolution result" },
            new object[] {40000M, 4.30M, "Band resolution result" },
            new object[] {40001M, 4.30M, "Band resolution result" }
        };
    }
}
