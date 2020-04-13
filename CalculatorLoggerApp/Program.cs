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
