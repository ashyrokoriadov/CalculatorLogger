using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CalculatorLoggerLibrary.Models;

namespace CalculatorLoggerTests.ModelsTests
{
    [TestClass]
    public class CalculationUnitTest
    {
        CalculationUnit cu1;
        CalculationUnit cu2;
        CalculationUnit cu3;
        CalculationUnit cu4;
        CalculationUnit cu5;
        object NotACalculationUnitObject;

        [TestInitialize]
        public void TestInitialize()
        {
            cu1 = new CalculationUnit();
            cu2 = new CalculationUnit(9.5M, "CalculationUnit2");
            cu3 = new CalculationUnit(11.78M, "CalculationUnit3");
            cu4 = new CalculationUnit(223.47M, "CalculationUnit4");
            cu5 = new CalculationUnit(223.47M, "CalculationUnit5");
            NotACalculationUnitObject = new object();
        }

        [TestMethod]
        public void ShouldSetDefaultValuesForType()
        {
            Assert.AreEqual(0.0M, cu1.Item);
            Assert.IsNull(cu1.ItemDescription);
        }

        [TestMethod]
        public void ShouldSetCorrectValuesForType()
        {
            Assert.AreEqual(9.5M, cu2.Item);
            Assert.AreEqual("CalculationUnit2", cu2.ItemDescription);
        }

        [TestMethod]
        public void ShouldCorrectlyCompareObjects()
        {
            Assert.AreEqual(-1, cu2.CompareTo(cu3));
        }

        [TestMethod]
        public void ShouldCorrectlyCompareObjects_ObjectsAreEqual()
        {
            Assert.AreEqual(0, cu4.CompareTo(cu5));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowExceptionDuringComparing()
        {
            Assert.AreEqual(0, cu4.CompareTo(NotACalculationUnitObject));
        }

        [TestMethod]
        public void ShouldCorrectlyCompareObjects_DifferentObjects()
        {
            Assert.IsTrue(cu2 < cu3);
            Assert.IsFalse(cu2 > cu3);
            Assert.IsTrue(cu4 <= cu5);
            Assert.IsTrue(cu4 >= cu5);
        }
    }
}
