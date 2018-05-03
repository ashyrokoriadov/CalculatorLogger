using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CalculatorLoggerLibrary.Models;
using CalculatorLoggerLibrary.Models.Operations;
using CalculatorLoggerLibrary.Implementations;
using CalculatorLoggerLibrary.Interfaces;

namespace CalculatorLoggerTests.ImplementationsTests
{
    [TestClass]
    public class StandardCalculatorTest
    {
        IFormulaLogger logger;
        ICalculator calculator;

        SingleCondition sc1;
        SingleCondition sc2;
        SingleCondition sc3;
        SingleCondition sc4;

        CalculationUnit cu1;
        CalculationUnit cu2;
        CalculationUnit cu3;
        CalculationUnit cu4;

        MultiCondition mc1;
        MultiCondition mc2;

        Band b1;
        Dictionary<decimal, decimal> bands;

        [TestInitialize]
        public void TestInitialize()
        {
            logger = new XMLFormulaLogger();
            calculator = new StandardCalculator(logger);
            calculator["item1"] = 5.2M;
            calculator["item2"] = 7.8M;
            calculator["item3"] = 6.4M;
            calculator["item4"] = 8.6M;
            calculator["item5"] = 16.7M;
            calculator["zeroItem"] = 0M;

            cu1 = new CalculationUnit(5.0M, "SimpleConditionSample1");
            sc1 = new SingleCondition(ConditionOperator.GreaterThan, cu1);

            cu2 = new CalculationUnit(10.0M, "SimpleConditionSample2");
            sc2 = new SingleCondition(ConditionOperator.Equal, cu2);

            mc1 = new MultiCondition(LogicOperator.Or, new SingleCondition[] { sc1, sc2 });

            cu3 = new CalculationUnit(15.0M, "SimpleConditionSample3");
            sc3 = new SingleCondition(ConditionOperator.GreaterThanOrEqual, cu3);

            cu4 = new CalculationUnit(20.0M, "SimpleConditionSample4");
            sc4 = new SingleCondition(ConditionOperator.LessThanOrEqual, cu4);

            mc2 = new MultiCondition(LogicOperator.And, new SingleCondition[] { sc3, sc4 });

            bands = new Dictionary<decimal, decimal>();
            bands.Add(5M, 3.84M);
            bands.Add(10M, 4.14M);
            bands.Add(20M, 4.44M);

            b1 = new Band("Band1", bands);
        }

        [TestMethod]
        public void ShouldCorrectlyAddElements()
        {
            CalculationUnit cu = calculator.Add("Sum", new string[] { "item1", "item2", "item3", "item4" });
            Assert.AreEqual("Sum", cu.ItemDescription);
            Assert.AreEqual(28M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyAddElementsIfArrayContainsNullElements()
        {
            CalculationUnit cu = calculator.Add("Sum", new string[] { null, null, "item3", "item4" });
            Assert.AreEqual("Sum", cu.ItemDescription);
            Assert.AreEqual(15M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyAddElementsAndNotThrowAnExceptionWhenIdentifierIsNull()
        {
            CalculationUnit cu = calculator.Add(null, new string[] { "item1", "item2", "item3", "item4" });
            Assert.AreEqual("SumResult", cu.ItemDescription);
            Assert.AreEqual(28M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyAddElementsAndNotThrowAnExceptionWhenDataIsNull()
        {
            CalculationUnit cu = calculator.Add("Sum", null);
            Assert.AreEqual("Sum", cu.ItemDescription);
            Assert.AreEqual(0M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlySubtractElements()
        {
            CalculationUnit cu = calculator.Subtract("Difference", new string[] { "item1", "item2" });
            Assert.AreEqual("Difference", cu.ItemDescription);
            Assert.AreEqual(-2.6M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlySubtractElementsIfArrayContainsNullElements()
        {
            CalculationUnit cu = calculator.Subtract("Difference", new string[] { "item1", "item2", null});
            Assert.AreEqual("Difference", cu.ItemDescription);
            Assert.AreEqual(-2.6M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlySubtractElementsAndNotThrowAnExceptionWhenIdentifierIsNull()
        {
            CalculationUnit cu = calculator.Subtract(null, new string[] { "item1", "item2" });
            Assert.AreEqual("SubtractResult", cu.ItemDescription);
            Assert.AreEqual(-2.6M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlySubtractElementsAndNotThrowAnExceptionWhenDataIsNull()
        {
            CalculationUnit cu = calculator.Subtract("Difference", null);
            Assert.AreEqual("Difference", cu.ItemDescription);
            Assert.AreEqual(0M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyMultipleElements()
        {
            CalculationUnit cu = calculator.Multiple("Product", new string[] { "item3", "item2" });
            Assert.AreEqual("Product", cu.ItemDescription);
            Assert.AreEqual(49.92M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyMultipleElementsIfArrayContainsNullElements()
        {
            CalculationUnit cu = calculator.Multiple("Product", new string[] { "item3", "item2", null });
            Assert.AreEqual("Product", cu.ItemDescription);
            Assert.AreEqual(49.92M, cu.Item);
        }

        [TestMethod]
        public void ShouldMultipleElementsAndNotThrowAnExceptionWhenIdentifierIsNull()
        {
            CalculationUnit cu = calculator.Multiple(null, new string[] { "item3", "item2" });
            Assert.AreEqual("ProductResult", cu.ItemDescription);
            Assert.AreEqual(49.92M, cu.Item);
        }

        [TestMethod]
        public void ShouldMultipleElementsAndNotThrowAnExceptionWhenDataIsNull()
        {
            CalculationUnit cu = calculator.Multiple("Product", null);
            Assert.AreEqual("Product", cu.ItemDescription);
            Assert.AreEqual(1M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyDivideElements()
        {
            CalculationUnit cu = calculator.Divide("Quotient", "item3", "item2");
            Assert.AreEqual("Quotient", cu.ItemDescription);
            Assert.AreEqual(0.8205128205128205128205128205M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyDivideElementsAndNotThrowAnExceptionWhenIdentifierIsNull()
        {
            CalculationUnit cu = calculator.Divide(null, "item3", "item2");
            Assert.AreEqual("QuotientResult", cu.ItemDescription);
            Assert.AreEqual(0.8205128205128205128205128205M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyDivideElementsAndNotThrowAnExceptionWhenDividentIsNull()
        {
            CalculationUnit cu = calculator.Divide("Quotient", null, "item2");
            Assert.AreEqual("Quotient", cu.ItemDescription);
            Assert.AreEqual(0M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyDivideElementsAndNotThrowAnExceptionWhenDivisorIsNull()
        {
            CalculationUnit cu = calculator.Divide("Quotient", "item3", null);
            Assert.AreEqual("Quotient", cu.ItemDescription);
            Assert.AreEqual(0M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyDivideElementsAndNotThrowAnExceptionWhenDivisorEqualsZero()
        {
            CalculationUnit cu = calculator.Divide("Quotient", "item3", "zeroItem");
            Assert.AreEqual("Quotient", cu.ItemDescription);
            Assert.AreEqual(0M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyReturnMaximumElement()
        {
            CalculationUnit cu = calculator.Max("MaxElement", new string[] { "item3", "item2", "item4" });
            Assert.AreEqual("MaxElement", cu.ItemDescription);
            Assert.AreEqual(8.6M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyReturnMaximumElementIfArrayContainsNullElements()
        {
            CalculationUnit cu = calculator.Max("MaxElement", new string[] { "item3", "item2", "item4", null });
            Assert.AreEqual("MaxElement", cu.ItemDescription);
            Assert.AreEqual(8.6M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyReturnMaximumElementAndNotThrowAnExceptionWhenIdentifierIsNull()
        {
            CalculationUnit cu = calculator.Max(null, new string[] { "item3", "item2", "item4" });
            Assert.AreEqual("StatisticFunctionResult", cu.ItemDescription);
            Assert.AreEqual(8.6M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyReturnMaximumElementAndNotThrowAnExceptionWhenDataIsNull()
        {
            CalculationUnit cu = calculator.Max("MaxElement", null);
            Assert.AreEqual("MaxElement", cu.ItemDescription);
            Assert.AreEqual(0M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyReturnMinimumElement()
        {
            CalculationUnit cu = calculator.Min("MinElement", new string[] { "item3", "item2", "item4" });
            Assert.AreEqual("MinElement", cu.ItemDescription);
            Assert.AreEqual(6.4M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyReturnMinimumElementWhenArrayContainsNulls()
        {
            CalculationUnit cu = calculator.Min("MinElement", new string[] { "item3", "item2", "item4", null});
            Assert.AreEqual("MinElement", cu.ItemDescription);
            Assert.AreEqual(6.4M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyReturnMinimumElementAndNotThrowAnExceptionWhenIdentifierIsNull()
        {
            CalculationUnit cu = calculator.Min(null, new string[] { "item3", "item2", "item4" });
            Assert.AreEqual("StatisticFunctionResult", cu.ItemDescription);
            Assert.AreEqual(6.4M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyReturnMinimumElementAndNotThrowAnExceptionWhenDataIsNull()
        {
            CalculationUnit cu = calculator.Min("MinElement", null);
            Assert.AreEqual("MinElement", cu.ItemDescription);
            Assert.AreEqual(0M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyReturnAverageElement()
        {
            CalculationUnit cu = calculator.Average("AverageElement", new string[] { "item3", "item2", "item4" });
            Assert.AreEqual("AverageElement", cu.ItemDescription);
            Assert.AreEqual(7.6M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyReturnAverageElementWhenArrayContainsNulls()
        {
            CalculationUnit cu = calculator.Average("AverageElement", new string[] { "item3", "item2", "item4", null });
            Assert.AreEqual("AverageElement", cu.ItemDescription);
            Assert.AreEqual(7.6M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyReturnAverageElementAndNotThrowAnExceptionWhenIdentifierIsNull()
        {
            CalculationUnit cu = calculator.Average(null, new string[] { "item3", "item2", "item4" });
            Assert.AreEqual("StatisticFunctionResult", cu.ItemDescription);
            Assert.AreEqual(7.6M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyReturnAverageElementAndNotThrowAnExceptionWhenDataIsNull()
        {
            CalculationUnit cu = calculator.Average("AverageElement", null);
            Assert.AreEqual("AverageElement", cu.ItemDescription);
            Assert.AreEqual(0M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyResolveSingleCondition()
        {
            CalculationUnit cu = calculator.ResolveCondition("SingleConditionValue", "item1", sc1, false);
            Assert.AreEqual("SingleConditionValue", cu.ItemDescription);
            Assert.AreEqual(1M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyResolveSingleConditionAndNotThrowAnExceptionWhenIdentifierIsNull()
        {
            CalculationUnit cu = calculator.ResolveCondition(null, "item1", sc1, false);
            Assert.AreEqual("SingleConditionResult", cu.ItemDescription);
            Assert.AreEqual(1M, cu.Item);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ShouldCorrectlyResolveSingleConditionAndThrowAnExceptionWhenValueToCompareIsNull()
        {
            CalculationUnit cu = calculator.ResolveCondition("SingleConditionValue", null, sc1, false);
        }

        [TestMethod]
        public void ShouldCorrectlyResolveSingleConditionAndNotThrowAnExceptionWhenSingleConditionIsNull()
        {
            CalculationUnit cu = calculator.ResolveCondition("SingleConditionValue", "item1", null, false);
            Assert.AreEqual("SingleConditionIsNull", cu.ItemDescription);
            Assert.AreEqual(0M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyResolveMultiCondition()
        {
            CalculationUnit cu = calculator.ResolveMultiCondition("MultiConditionValue", "item2", mc1, false);
            Assert.AreEqual("MultiConditionValue", cu.ItemDescription);
            Assert.AreEqual(1M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyResolveMultiConditionAndNotThrowAnExceptionWhenIdentifierIsNull()
        {
            CalculationUnit cu = calculator.ResolveMultiCondition(null, "item2", mc1, false);
            Assert.AreEqual("MultiConditionResult", cu.ItemDescription);
            Assert.AreEqual(1M, cu.Item);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ShouldCorrectlyResolveMultiConditionAndThrowAnExceptionWhenValueToCompareIsNull()
        {
            CalculationUnit cu = calculator.ResolveMultiCondition("MultiConditionValue", null, mc1, false);
        }

        [TestMethod]
        public void ShouldCorrectlyResolveMultiConditionAndNotThrowAnExceptionWhenMultiConditionIsNull()
        {
            CalculationUnit cu = calculator.ResolveMultiCondition("MultiConditionValue", "item2", null, false);
            Assert.AreEqual("MultiConditionIsNull", cu.ItemDescription);
            Assert.AreEqual(0M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyResolveSwitchCondition()
        {
            CalculationUnit cu = calculator.ResolveSwitch("SwitchValue", "item5", new MultiCondition[] { mc1, mc2 });
            Assert.AreEqual("SwitchValue", cu.ItemDescription);
            Assert.AreEqual(1M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyResolveSwitchConditionAndNotThrowAnExceptionWhenIdentifierIsNull()
        {
            CalculationUnit cu = calculator.ResolveSwitch(null, "item5", new MultiCondition[] { mc1, mc2 });
            Assert.AreEqual("SwitchResult", cu.ItemDescription);
            Assert.AreEqual(1M, cu.Item);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ShouldCorrectlyResolveSwitchConditionAndThrowAnExceptionWhenValueToCompareIsNull()
        {
            CalculationUnit cu = calculator.ResolveSwitch("SwitchValue", null, new MultiCondition[] { mc1, mc2 });
        }

        [TestMethod]
        public void ShouldCorrectlyResolveSwitchConditionAndNotThrowAnExceptionWhenMultiConditionsIsNull()
        {
            CalculationUnit cu = calculator.ResolveSwitch("SwitchValue", "item5", null);
            Assert.AreEqual("MultiConditionsInSwitchAreNull", cu.ItemDescription);
            Assert.AreEqual(0M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyResolveBandCondition()
        {
            CalculationUnit cu = calculator.ResolveBand("BandValue", "item1", b1);
            Assert.AreEqual("BandValue", cu.ItemDescription);
            Assert.AreEqual(4.14M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyResolveBandConditionAndNotThrowAnExceptionWhenIdentifierIsNull()
        {
            CalculationUnit cu = calculator.ResolveBand(null, "item1", b1);
            Assert.AreEqual("BandResult", cu.ItemDescription);
            Assert.AreEqual(4.14M, cu.Item);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ShouldCorrectlyResolveBandConditionAndThrowAnExceptionWhenValueToCompareIsNull()
        {
            CalculationUnit cu = calculator.ResolveBand("BandValue", null, b1);
        }

        [TestMethod]
        public void ShouldNotThrowAnExceptionWheBandIsNull()
        {
            CalculationUnit cu = calculator.ResolveBand("BandValue", "item1", null);
            Assert.AreEqual("BandIsNull", cu.ItemDescription);
            Assert.AreEqual(0M, cu.Item);
        }

        [TestMethod]
        public void ShouldCorrectlyReturnLogData()
        {
            if (calculator is IDataManager)
            {
                XDocument doc = ((IDataManager)calculator).GetLogData() as XDocument;
                if (doc != null)
                {
                    Assert.IsInstanceOfType(doc, typeof(XDocument));
                    Assert.AreEqual("<XmlLogger />", doc.ToString());
                }
            }
        }

        [TestMethod]
        public void ShouldAddItemToACollection()
        {
            StandardCalculator calc = calculator as StandardCalculator;
            CalculationUnit testElement = new CalculationUnit(10, "testElement");
            calc?.Add(testElement);
            Assert.AreEqual(10M, calc?["testElement"]);
        }

        [TestMethod]
        public void ShouldAddItemToACollection_ShouldNotTHrowAnException()
        {
            StandardCalculator calc = calculator as StandardCalculator;
            CalculationUnit testElement = null;
            calc?.Add(testElement);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void ShouldRemoveItemFromACollection()
        {
            StandardCalculator calc = calculator as StandardCalculator;
            CalculationUnit testElement = new CalculationUnit(10, "testElement");
            calc?.Add(testElement);
            Assert.AreEqual(10M, calc?["testElement"]);

            calc?.Remove(testElement);
            decimal? testValue = calc?["testElement"];
        }

        [TestMethod]
        public void ShouldRemoveItemFromACollection_ShouldNotThrowAnException()
        {
            StandardCalculator calc = calculator as StandardCalculator;
            CalculationUnit testElement = null;
            calc?.Remove(testElement);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void ShouldRemoveItemFromACollectionByIndex()
        {
            StandardCalculator calc = calculator as StandardCalculator;
            CalculationUnit testElement = new CalculationUnit(10, "testElement");
            calc?.Add(testElement);
            Assert.AreEqual(10M, calc?["testElement"]);

            calc?.Remove("testElement");
            decimal? testValue = calc?["testElement"];
        }

        [TestMethod]
        public void ShouldRemoveItemFromACollectionByIndex_ShouldNotThrowAnException()
        {
            StandardCalculator calc = calculator as StandardCalculator;
            calc?.Remove(string.Empty);
        }
    }
}
