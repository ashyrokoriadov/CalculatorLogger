using System;
using System.Collections.Generic;
using System.Xml.Linq;

using CalculatorLoggerLibrary.Implementations;
using CalculatorLoggerLibrary.Interfaces;
using CalculatorLoggerLibrary.Models;
using CalculatorLoggerLibrary.Models.Operations;
using System.Configuration;

namespace CalculatorLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            #region  Console Window settings
            Console.SetWindowSize(150, 42);
            Console.Title = "Test Console Window Name";
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            #endregion

            #region Config Reader
            string loggerType; string logPath;
            GetConfiguration(out loggerType, out logPath);
            IFormulaLogger logger = GetLogger(loggerType);
            #endregion

            #region  Initial calcualtion data
            CalculationUnit calculationUnit1 = new CalculationUnit(-10M, "TestValue7");
            CalculationUnit calculationUnit2 = new CalculationUnit(16M, "TestValue8");
            CalculationUnit calculationUnit3 = new CalculationUnit(-8M, "TestValue9");
            CalculationUnit calculationUnit4 = new CalculationUnit(14M, "TestValue10");

            SingleCondition simpleCondition1 = new SingleCondition(ConditionOperator.GreaterThan, calculationUnit1, 1, 2);
            SingleCondition simpleCondition2 = new SingleCondition(ConditionOperator.GreaterThan, calculationUnit2, 3, 4);
            SingleCondition simpleCondition3 = new SingleCondition(ConditionOperator.GreaterThan, calculationUnit3, 5, 6);
            SingleCondition simpleCondition4 = new SingleCondition(ConditionOperator.GreaterThan, calculationUnit4, 7, 8);

            MultiCondition multiCondition1 = new MultiCondition(LogicOperator.And, new SingleCondition[2] { simpleCondition1, simpleCondition2 }, 111, 222);
            MultiCondition multiCondition2 = new MultiCondition(LogicOperator.And, new SingleCondition[2] { simpleCondition3, simpleCondition4 }, 333, 444);

            Dictionary<decimal, decimal> testBand = new Dictionary<decimal, decimal>();
            testBand.Add(10, 1.28M);
            testBand.Add(20, 1.29M);
            testBand.Add(30, 1.30M);
            Band band = new Band("TaxBand", testBand);
            #endregion

            #region Console Logger
            IFormulaLogger ConsoleLogger = new ConsoleFormulaLogger();
            ICalculator calc = new StandardCalculator(ConsoleLogger);
            calc["TestValue1"] = -10.4M;
            calc["TestValue2"] = 12.8M;
            calc["TestValue3"] = 13.7M;
            calc["TestValue5"] = 2.34M;
            calc["TestValue6"] = -18.0M;
            calc["SwitchTest"] = 15M;
            calc["ExpectedResult"] = 18.18M;

            calc.Add("AddValueResult", new string[] { "TestValue1", "TestValue2" });
            calc.Max("Max value of 4 items", new string[] { "TestValue1", "TestValue2", "TestValue3", "TestValue5" });
            calc.ResolveCondition("XML log value name", "TestValue1", simpleCondition1, true);
            calc.ResolveMultiCondition("XML log MC value name", "TestValue1", multiCondition1, true);
            calc.ResolveSwitch("SwitchOutputValue", "SwitchTest", new MultiCondition[2] { multiCondition1, multiCondition2 });
            calc.ResolveBand("TestBandName", "ExpectedResult", band);
            #endregion

            #region XML Logger
            ICalculator xmlCalc = new StandardCalculator(logger);
            xmlCalc["TestValue1"] = -10.4M;
            xmlCalc["TestValue2"] = 12.8M;
            xmlCalc["TestValue3"] = 13.7M;
            xmlCalc["TestValue5"] = 2.34M;
            xmlCalc["TestValue6"] = -18.0M;
            xmlCalc["SwitchTest"] = 15M;
            xmlCalc["ExpectedResult"] = 18.18M;

            xmlCalc.Add("AddValueResult", new string[] { "TestValue1", "TestValue2" });
            xmlCalc.Max("Max value of 4 items", new string[] { "TestValue1", "TestValue2", "TestValue3", "TestValue5" });            
            xmlCalc.ResolveCondition("XML log value name", "TestValue1", simpleCondition1, true);
            xmlCalc.ResolveMultiCondition("XML log MC value name", "TestValue1", multiCondition1, true);            
            xmlCalc.ResolveSwitch("SwitchOutputValue", "SwitchTest", new MultiCondition[2] { multiCondition1, multiCondition2 });
            xmlCalc.ResolveBand("TestBandName", "ExpectedResult", band);
            
            if (xmlCalc is IDataManager)
            {
                XDocument doc = ((IDataManager)xmlCalc).GetLogData() as XDocument;
                if (doc != null)
                    doc.Save(logPath);
            }
            #endregion

            Console.ReadKey();
        }

        #region Private Methods
        private static void GetConfiguration(out string loggerType, out string logPath)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);            
            loggerType = config.AppSettings.Settings["LoggerType"] !=null ? config.AppSettings.Settings["LoggerType"].Value : "ConsoleFormulaLogger";
            logPath = config.AppSettings.Settings["LogPath"].Value !=null ? config.AppSettings.Settings["LogPath"].Value : "D:\\";
        }

        private static IFormulaLogger GetLogger(string loggerType)
        {
            IFormulaLogger logger;

            switch (loggerType)
            {
                case "XMLFormulaLogger":
                    logger = new XMLFormulaLogger();
                    break;
                case "ConsoleFormulaLogger":
                default:
                    logger = new ConsoleFormulaLogger();
                    break;
            }

            return logger;
        }
        #endregion
    }
}
