using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CalculatorLoggerLibrary.Models;
using CalculatorLoggerLibrary.Models.Operations;
using CalculatorLoggerLibrary.Implementations;
using CalculatorLoggerLibrary.Interfaces;

namespace CalculatorLoggerTests.ImplementationsTests
{
    [TestClass]
    public class ConsoleFormulaLoggerTest
    {
        IFormulaLogger logger;

        CalculationUnit item1;
        CalculationUnit item2;
        CalculationUnit item3;
        CalculationUnit item4;
        CalculationUnit result;


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
