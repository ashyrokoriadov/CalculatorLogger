using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CalculatorLoggerLibrary.Models;
using CalculatorLoggerLibrary.Models.Operations;

namespace CalculatorLoggerTests.ModelsTests
{
    [TestClass]
    public class BandTest
    {
        Band Band1;
        Band Band2;
        Dictionary<decimal, decimal> bands;
        CalculationUnit ValueToCompare;

        [TestInitialize]
        public void TestInitialize()
        {
            Band1 = new Band("Band1");

            bands = new Dictionary<decimal, decimal>();
            bands.Add(10000M, 3.84M);
            bands.Add(20000M, 4.14M);
            bands.Add(30000M, 4.44M);

            Band2 = new Band("Band2", bands);

            ValueToCompare = new CalculationUnit(15000M, "ValueToCompare");
        }

        [TestMethod]
        public void ShouldSetDefaultValuesForType()
        {
            Band1.ValueToCompare = ValueToCompare;

            Assert.AreEqual("Band1", Band1.Name);
            Assert.AreEqual(ValueToCompare, Band1.ValueToCompare);
            Assert.AreEqual(0.0M, Band1.ResultMaxValue);
            Assert.AreEqual(0.0M, Band1.ResultValue);
        }

        [TestMethod]
        public void ShouldSetCorrectValuesForType()
        {
            Band2.ValueToCompare = ValueToCompare;

            Assert.AreEqual("Band2", Band2.Name);
            Assert.AreEqual(ValueToCompare, Band2.ValueToCompare);
            Assert.AreEqual(20000.0M, Band2.ResultMaxValue);
            Assert.AreEqual(4.14M, Band2.ResultValue);
        }

        [TestMethod]
        public void ShouldSetCorrectBandsForType()
        {
            Band1.SetBand(bands);
            Band1.ValueToCompare = ValueToCompare;
            Band1.SetResult();

            Assert.AreEqual(20000.0M, Band1.ResultMaxValue);
            Assert.AreEqual(4.14M, Band1.ResultValue);
        }

        [TestMethod]
        public void ShouldNotThrowErrors_NoValueToCompare_NoBands()
        {
            Band1.SetResult();

            Assert.AreEqual(-1M, Band1.ResultMaxValue);
            Assert.AreEqual(-1M, Band1.ResultValue);
        }

        [TestMethod]
        public void ShouldNotThrowErrors_NoBands()
        {
            Band1.ValueToCompare = ValueToCompare;
            Band1.SetResult();

            Assert.AreEqual(-1M, Band1.ResultMaxValue);
            Assert.AreEqual(-1M, Band1.ResultValue);
        }

    }
}
