using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CalculatorLoggerLibrary.Models;
using CalculatorLoggerLibrary.Models.Operations;
using CalculatorLoggerLibrary.Implementations;
using CalculatorLoggerLibrary.Interfaces;
using System.Xml.Linq;

namespace CalculatorLoggerTests.ImplementationsTests
{
    [TestClass]
    public class XMLFormulaLoggerTest
    {
        IFormulaLogger logger;

        CalculationUnit item1;
        CalculationUnit item2;
        CalculationUnit item3;
        CalculationUnit item4;
        CalculationUnit result;

        SingleCondition sc1;
        SingleCondition sc2;
        SingleCondition sc3;
        SingleCondition sc4;

        MultiCondition mc1;
        MultiCondition mc2;

        Band b1;
        Dictionary<decimal, decimal> bands;

        [TestInitialize]
        public void TestInitialize()
        {
            logger = new XMLFormulaLogger();
            item1 = new CalculationUnit(1M, "item1");
            item2 = new CalculationUnit(2M, "item2");
            item3 = new CalculationUnit(3M, "item3");
            item4 = new CalculationUnit(4M, "item4");

            sc1 = new SingleCondition(ConditionOperator.GreaterThan, item1);
            sc2 = new SingleCondition(ConditionOperator.LessThanOrEqual, item2);
            sc3 = new SingleCondition(ConditionOperator.GreaterThan, item3);
            sc4 = new SingleCondition(ConditionOperator.LessThanOrEqual, item4);

            mc1 = new MultiCondition(LogicOperator.Or, new SingleCondition[] { sc1, sc2 });
            mc2 = new MultiCondition(LogicOperator.And, new SingleCondition[] { sc3, sc4 });

            bands = new Dictionary<decimal, decimal>();
            bands.Add(1M, 3.84M);
            bands.Add(2M, 4.14M);
            bands.Add(3M, 4.44M);

            b1 = new Band("Band1", bands);
        }

        [TestMethod]
        public void ShouldCorrectlyLogOperationAndThrowNoException()
        {
            result = new CalculationUnit(6M, "result");
            logger.LogOperation(MathOperator.Add, result, new CalculationUnit[] { item1, item2, item3 });

            string logData = GetLoggerData(logger);
            Assert.IsTrue(logData.Contains("<FormulaText position=\"0\" time="));
            Assert.IsTrue(logData.Contains("<FormulaResolution position=\"1\" time="));
            Assert.IsTrue(logData.Contains("<FormulaResultName position=\"2\" time="));
            Assert.IsTrue(logData.Contains("<FormulaResultValue position=\"3\" time="));
            Assert.IsTrue(logData.Contains(">item1 + item2 + item3</FormulaText>"));
            Assert.IsTrue(logData.Contains(">1 + 2 + 3</FormulaResolution>"));
            Assert.IsTrue(logData.Contains(">result</FormulaResultName>"));
            Assert.IsTrue(logData.Contains(">6</FormulaResultValue>"));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void LogOperation_ShouldThrowExceptionWhenResultIsNull()
        {
            logger.LogOperation(MathOperator.Add, null, new CalculationUnit[] { item1, item2, item3 });        
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void LogOperation_ShouldThrowExceptionWhenDataCollectionIsNull()
        {
            result = new CalculationUnit(6M, "result");
            logger.LogOperation(MathOperator.Add, result, null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void LogOperation_ShouldThrowExceptionWhenElementInDataCollectionIsNull()
        {
            result = new CalculationUnit(6M, "result");
            logger.LogOperation(MathOperator.Add, result, new CalculationUnit[] { item1, null, item3 });
        }

        [TestMethod]
        public void ShouldCorrectlyLogConditionAndThrowNoException()
        {
            sc1.SetResult(item2);
            logger.LogCondition(sc1, "SingleConditionResult");

            string logData = GetLoggerData(logger);
            Assert.IsTrue(logData.Contains("<Formula position=\"0\" time="));
            Assert.IsTrue(logData.Contains("<Resolution position=\"1\" time="));
            Assert.IsTrue(logData.Contains("<ResultName position=\"2\" time="));
            Assert.IsTrue(logData.Contains("<ResultValue position=\"3\" time="));
            Assert.IsTrue(logData.Contains(">item2 &gt; item1 ? 1 : 0</Formula>"));
            Assert.IsTrue(logData.Contains(">2 &gt; 1 = True</Resolution>"));
            Assert.IsTrue(logData.Contains(">SingleConditionResult</ResultName>"));
            Assert.IsTrue(logData.Contains(">1</ResultValue>"));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void LogCondition_ShouldThrowExceptionWhenSingleConditionIsNull()
        {
            logger.LogCondition(null, "SingleConditionResult");
        }

        [TestMethod]
        public void LogCondition_ShouldNotThrowExceptionWhenResultNameIsNull()
        {
            sc1.SetResult(item2);
            logger.LogCondition(sc1, null);

            string logData = GetLoggerData(logger);
            Assert.IsTrue(logData.Contains("<Formula position=\"0\" time="));
            Assert.IsTrue(logData.Contains("<Resolution position=\"1\" time="));
            Assert.IsTrue(logData.Contains("<ResultName position=\"2\" time="));
            Assert.IsTrue(logData.Contains("<ResultValue position=\"3\" time="));
            Assert.IsTrue(logData.Contains(">item2 &gt; item1 ? 1 : 0</Formula>"));
            Assert.IsTrue(logData.Contains(">2 &gt; 1 = True</Resolution>"));
            Assert.IsTrue(logData.Contains(">1</ResultValue>"));
        }

        [TestMethod]
        public void ShouldCorrectlyLogMultiConditionAndNotThrowException()
        {
            mc1.SetResult(item2);
            logger.LogMultiCondition(mc1);

            string logData = GetLoggerData(logger);
            Assert.IsTrue(logData.Contains("<ValueIfTrue position=\"0\" time="));
            Assert.IsTrue(logData.Contains("<ValueIfFalse position=\"1\" time="));
            Assert.IsTrue(logData.Contains("<LogicalOperator position=\"2\" time="));
            Assert.IsTrue(logData.Contains("<Formula position=\"3\" time="));
            Assert.IsTrue(logData.Contains("<Resolution position=\"4\" time="));
            Assert.IsTrue(logData.Contains("<ResultName position=\"5\" time="));
            Assert.IsTrue(logData.Contains("<ResultValue position=\"6\" time="));
            Assert.IsTrue(logData.Contains("<Formula position=\"7\" time="));
            Assert.IsTrue(logData.Contains("<Resolution position=\"8\" time="));
            Assert.IsTrue(logData.Contains("<ResultName position=\"9\" time="));
            Assert.IsTrue(logData.Contains("<ResultValue position=\"10\" time="));
            Assert.IsTrue(logData.Contains("<ResultName position=\"11\" time="));
            Assert.IsTrue(logData.Contains("<ResultValue position=\"12\" time="));

            Assert.IsTrue(logData.Contains(">1</ValueIfTrue>"));
            Assert.IsTrue(logData.Contains(">0</ValueIfFalse>"));
            Assert.IsTrue(logData.Contains(">OR</LogicalOperator>"));
            Assert.IsTrue(logData.Contains(">item2 &gt; item1 ? 1 : 0</Formula>"));
            Assert.IsTrue(logData.Contains(">2 &gt; 1 = True</Resolution>"));
            Assert.IsTrue(logData.Contains(">TemporaryValue1</ResultName>"));
            Assert.IsTrue(logData.Contains(">1</ResultValue>"));
            Assert.IsTrue(logData.Contains(">item2 =&lt; item2 ? 1 : 0</Formula>"));
            Assert.IsTrue(logData.Contains(">2 =&lt; 2 = True</Resolution>"));
            Assert.IsTrue(logData.Contains(">TemporaryValue2</ResultName>"));
            Assert.IsTrue(logData.Contains(">1</ResultValue>"));
            Assert.IsTrue(logData.Contains(">True</ResultName>"));
            Assert.IsTrue(logData.Contains(">1</ResultValue>"));
        }


        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void LogMultiCondition_ShouldThrowExceptionWhenMultiConditionIsNull()
        {
            logger.LogMultiCondition(null);
        }

        [TestMethod]
        public void ShouldCorrectlyLogSwitchAndNotThrowException()
        {
            mc1.SetResult(item1);
            mc2.SetResult(item3);
            result = new CalculationUnit(6M, "result");
            logger.LogSwitch(new MultiCondition[] { mc1, mc2}, result);

            string logData = GetLoggerData(logger);
            Assert.IsTrue(logData.Contains("<ValueIfTrue position=\"0\" time=") && logData.Contains(">1</ValueIfTrue>"));
            Assert.IsTrue(logData.Contains("<ValueIfFalse position=\"1\" time=") && logData.Contains(">0</ValueIfFalse>"));
            Assert.IsTrue(logData.Contains("<LogicalOperator position=\"2\" time=") && logData.Contains(">OR</LogicalOperator>"));
            Assert.IsTrue(logData.Contains("<Formula position=\"3\" time=") && logData.Contains(">item1 &gt; item1 ? 1 : 0</Formula>"));
            Assert.IsTrue(logData.Contains("<Resolution position=\"4\" time=") && logData.Contains(">1 &gt; 1 = False</Resolution>"));
            Assert.IsTrue(logData.Contains("<ResultName position=\"5\" time=") && logData.Contains(">TemporaryValue1</ResultName>"));
            Assert.IsTrue(logData.Contains("<ResultValue position=\"6\" time=") && logData.Contains(">0</ResultValue>"));
            Assert.IsTrue(logData.Contains("<Formula position=\"7\" time=") && logData.Contains(">item1 =&lt; item2 ? 1 : 0</Formula>"));
            Assert.IsTrue(logData.Contains("<Resolution position=\"8\" time=") && logData.Contains(">1 =&lt; 2 = True</Resolution>"));
            Assert.IsTrue(logData.Contains("<ResultName position=\"9\" time=") && logData.Contains(">TemporaryValue2</ResultName>"));
            Assert.IsTrue(logData.Contains("<ResultValue position=\"10\" time=") && logData.Contains(">1</ResultValue>"));
            Assert.IsTrue(logData.Contains("<ResultName position=\"11\" time=") && logData.Contains(">True</ResultName>"));
            Assert.IsTrue(logData.Contains("<ResultValue position=\"12\" time=") && logData.Contains(">1</ResultValue>"));

            Assert.IsTrue(logData.Contains("<ValueIfTrue position=\"13\" time=") && logData.Contains(">1</ValueIfTrue>"));
            Assert.IsTrue(logData.Contains("<ValueIfFalse position=\"14\" time=") && logData.Contains(">0</ValueIfFalse>"));
            Assert.IsTrue(logData.Contains("<LogicalOperator position=\"15\" time=") && logData.Contains(">AND</LogicalOperator>"));
            Assert.IsTrue(logData.Contains("<Formula position=\"16\" time=") && logData.Contains(">item3 &gt; item3 ? 1 : 0</Formula>"));
            Assert.IsTrue(logData.Contains("<Resolution position=\"17\" time=") && logData.Contains("3 &gt; 3 = False</Resolution>"));
            Assert.IsTrue(logData.Contains("<ResultName position=\"18\" time=") && logData.Contains(">TemporaryValue1</ResultName>"));
            Assert.IsTrue(logData.Contains("<ResultValue position=\"19\" time=") && logData.Contains(">0</ResultValue>"));
            Assert.IsTrue(logData.Contains("<Formula position=\"20\" time=") && logData.Contains(">item3 =&lt; item4 ? 1 : 0</Formula>"));
            Assert.IsTrue(logData.Contains("<Resolution position=\"21\" time=") && logData.Contains(">3 =&lt; 4 = True</Resolution>"));
            Assert.IsTrue(logData.Contains("<ResultName position=\"22\" time=") && logData.Contains(">TemporaryValue2</ResultName>"));
            Assert.IsTrue(logData.Contains("<ResultValue position=\"23\" time=") && logData.Contains(">1</ResultValue>"));
            Assert.IsTrue(logData.Contains("<ResultName position=\"24\" time=") && logData.Contains(">False</ResultName>"));
            Assert.IsTrue(logData.Contains("<ResultValue position=\"25\" time=") && logData.Contains(">0</ResultValue>"));

            Assert.IsTrue(logData.Contains("<ResultName position=\"26\" time=") && logData.Contains(">result</ResultName>"));
            Assert.IsTrue(logData.Contains("<ResultValue position=\"27\" time=") && logData.Contains(">6</ResultValue>"));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void LogSwitch_ShouldThrowExceptionWhenMultiConditionsArrayIsNull()
        {
            result = new CalculationUnit(6M, "result");
            logger.LogSwitch(null, result);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void LogSwitch_ShouldThrowExceptionWhenResultIsNull()
        {
            mc1.SetResult(item1);
            mc2.SetResult(item3);
            logger.LogSwitch(new MultiCondition[] { mc1, mc2 }, null);
        }

        [TestMethod]
        public void ShouldCorrectlyLogBandAndNotThrowException()
        {
            b1.ValueToCompare = new CalculationUnit(2M, "result");
            b1.SetResult();
            logger.LogBand(b1);

            string logData = GetLoggerData(logger);
            Assert.IsTrue(logData.Contains("<Name position=\"0\" time=") && logData.Contains(">Band1</Name>"));
            Assert.IsTrue(logData.Contains("<EvaluatedValueName position=\"1\" time=") && logData.Contains(">result</EvaluatedValueName>"));
            Assert.IsTrue(logData.Contains("<EvaluatedValue position=\"2\" time=") && logData.Contains(">2</EvaluatedValue>"));
            Assert.IsTrue(logData.Contains("<MaxValue position=\"3\" time=") && logData.Contains(">3</MaxValue>"));
            Assert.IsTrue(logData.Contains("<ResultValue position=\"4\" time=") && logData.Contains(">4,44</ResultValue>"));           
        }

        [TestMethod]
        public void ShouldCorrectlyLogBandWhenValueToCompareIsGreaterThanHighestBand()
        {
            b1.ValueToCompare = new CalculationUnit(6M, "result");
            b1.SetResult();
            logger.LogBand(b1);

            string logData = GetLoggerData(logger);
            Assert.IsTrue(logData.Contains("<Name position=\"0\" time=") && logData.Contains(">Band1</Name>"));
            Assert.IsTrue(logData.Contains("<EvaluatedValueName position=\"1\" time=") && logData.Contains(">result</EvaluatedValueName>"));
            Assert.IsTrue(logData.Contains("<EvaluatedValue position=\"2\" time=") && logData.Contains(">6</EvaluatedValue>"));
            Assert.IsTrue(logData.Contains("<MaxValue position=\"3\" time=") && logData.Contains(">0</MaxValue>"));
            Assert.IsTrue(logData.Contains("<ResultValue position=\"4\" time=") && logData.Contains(">0</ResultValue>"));
        }

        [TestMethod]
        public void ShouldCorrectlyLogBandWhenValueToCompareIsLessThanMinimumBand()
        {
            b1.ValueToCompare = new CalculationUnit(-6M, "result");
            b1.SetResult();
            logger.LogBand(b1);

            string logData = GetLoggerData(logger);
            Assert.IsTrue(logData.Contains("<Name position=\"0\" time=") && logData.Contains(">Band1</Name>"));
            Assert.IsTrue(logData.Contains("<EvaluatedValueName position=\"1\" time=") && logData.Contains(">result</EvaluatedValueName>"));
            Assert.IsTrue(logData.Contains("<EvaluatedValue position=\"2\" time=") && logData.Contains(">-6</EvaluatedValue>"));
            Assert.IsTrue(logData.Contains("<MaxValue position=\"3\" time=") && logData.Contains(">1</MaxValue>"));
            Assert.IsTrue(logData.Contains("<ResultValue position=\"4\" time=") && logData.Contains(">3,84</ResultValue>"));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void LogBand_ShouldThrowAnExceptonWhenBandIsNull()
        {
            logger.LogBand(null);            
        }

        [TestMethod]
        public void ShouldCorrectlyLogStatisticalForumalndThrowNoException()
        {
            result = new CalculationUnit(3M, "result");
            logger.LogStatisticalFormula(StatisticalOperator.Max, result, new CalculationUnit[] { item1, item2, item3 });

            string logData = GetLoggerData(logger);
            Assert.IsTrue(logData.Contains("<Elements position=\"0\" time=") && logData.Contains(">item1 = 1, item2 = 2, item3 = 3</Elements>"));
            Assert.IsTrue(logData.Contains("<Condition position=\"1\" time=") && logData.Contains(">SELECT MAX</Condition>"));
            Assert.IsTrue(logData.Contains("<FormulaResultName position=\"2\" time=") && logData.Contains(">result</FormulaResultName>"));
            Assert.IsTrue(logData.Contains("<FormulaResultValue position=\"3\" time=") && logData.Contains(">3</FormulaResultValue>"));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void LogStatisticalFormula_ShouldThrowExceptionWhenResultIsNull()
        {
            logger.LogStatisticalFormula(StatisticalOperator.Max, null, new CalculationUnit[] { item1, item2, item3 });
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void LogStatisticalFormula_ShouldThrowExceptionWhenDataCollectionIsNull()
        {
            result = new CalculationUnit(6M, "result");
            logger.LogStatisticalFormula(StatisticalOperator.Max, result, null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void LogStatisticalFormula_ShouldThrowExceptionWhenElementInDataCollectionIsNull()
        {
            result = new CalculationUnit(6M, "result");
            logger.LogStatisticalFormula(StatisticalOperator.Max, result, new CalculationUnit[] { item1, null, item3 });
        }

        private string GetLoggerData(IFormulaLogger logger)
        {
            if (logger is IDataManager)
            {
                XDocument doc = ((IDataManager)logger).GetLogData() as XDocument;
                if (doc != null)
                    return doc.ToString();
            }
            return null;
        }
    }
}
