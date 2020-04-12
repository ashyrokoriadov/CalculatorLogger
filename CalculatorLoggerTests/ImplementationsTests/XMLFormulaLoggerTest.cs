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

        Dictionary<decimal, decimal> bands;

        [TestInitialize]
        public void TestInitialize()
        {
            logger = new XMLFormulaLogger();
            item1 = new CalculationUnit(1M, "item1");
            item2 = new CalculationUnit(2M, "item2");
            item3 = new CalculationUnit(3M, "item3");
            item4 = new CalculationUnit(4M, "item4");


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
