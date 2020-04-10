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
        [ExpectedException(typeof(NullReferenceException))]
        public void LogCondition_ShouldThrowExceptionWhenSingleConditionIsNull()
        {
            logger.LogCondition(null, "SingleConditionResult");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void LogMultiCondition_ShouldThrowExceptionWhenMultiConditionIsNull()
        {
            logger.LogMultiCondition(null);
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
    }
}
