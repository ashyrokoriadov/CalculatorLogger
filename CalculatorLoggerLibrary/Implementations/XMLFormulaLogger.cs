using System;
using System.Xml.Linq;
using System.Text;

using CalculatorLoggerLibrary.Interfaces;
using CalculatorLoggerLibrary.Models;
using CalculatorLoggerLibrary.Models.Operations;
using CalculatorLoggerLibrary.Static;


namespace CalculatorLoggerLibrary.Implementations
{
    /// <summary>
    /// A class used to log calculations as an XML.
    /// </summary>
    public sealed class XMLFormulaLogger : IFormulaLogger
    {
        private int position = 0;
        private object _lockObject = new object();
        private XDocument XmlLogDocument;

        /// <summary>
        /// A constructor for XMLFormulaLogger class implementing IFormulaLogger and IDataManager interfaces.
        /// </summary>
        public XMLFormulaLogger()
        {
            XmlLogDocument = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("XmlLogger")
                );
        }
       
        /// <summary>
        /// A method logs simple mathematical operation.
        /// </summary>
        /// <param name="op">A mathematical operator</param>
        /// <param name="result">A result of calculations</param>
        /// <param name="values">Values that were used in a calculation</param>
        public void LogOperation(MathOperator op, CalculationUnit result, params CalculationUnit[] values)
        {
            StringBuilder formulaText = new StringBuilder();
            StringBuilder formulaResolutionText = new StringBuilder();

            FormulaHelper.BuildOperationFormula(ref formulaText, ref formulaResolutionText, op, values);

            lock (_lockObject)
            {
                string datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                XElement formulaElement = new XElement("Formula",
                       new XElement("FormulaText", new XAttribute("position", position.ToString()), new XAttribute("time", datetime), formulaText.ToString()));
                position++;

                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                XElement formulaResolution = new XElement("FormulaResolution", new XAttribute("position", position.ToString()), new XAttribute("time", datetime), formulaResolutionText.ToString());
                formulaElement.Add(formulaResolution);
                position++;

                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                XElement formulaResultName = new XElement("FormulaResultName", new XAttribute("position", position.ToString()), new XAttribute("time", datetime), result.ItemDescription);
                formulaElement.Add(formulaResultName);
                position++;

                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                XElement formulaResultValue = new XElement("FormulaResultValue", new XAttribute("position", position.ToString()), new XAttribute("time", datetime), result.Item.ToString());
                formulaElement.Add(formulaResultValue);
                position++;

                XmlLogDocument.Element("XmlLogger").Add(formulaElement);
            }

            /*
             * Operation xml log:
             *  <Formula>
             *      <FormulaText position="1" time="2017-11-22 08:39:15.299">name1 + name2 + name3 + name4</FormulaText>
             *      <FormulaResolution position="2" time="2017-11-22 08:39:15.315">(-10.5) + 12.5 + 13.8 + (-9.4)</FormulaResolution>
             *      <FormulaResultName position="3" time="2017-11-22 08:39:15.316">SomeTestValue</FormulaResultName>
             *      <FormulaResultValue position="4" time="2017-11-22 08:39:15.316">6.4</FormulaResultValue>
             *  </Formula>
             */
        }


      
      

        /// <summary>
        /// A method log a statistical formula.
        /// </summary>
        /// <param name="op">A statistical operator that was used in a calculation</param>
        /// <param name="result">A result of evaliuation of a statistical expression</param>
        /// <param name="values">Values that were evaluated with using of a statistical expression</param>
        public void LogStatisticalFormula(StatisticalOperator op, CalculationUnit result, params CalculationUnit[] values)
        {
            StringBuilder elementsText = new StringBuilder();

            FormulaHelper.BuildStatisticalElements(ref elementsText, values);

            lock (_lockObject)
            {
                string datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                XElement formulaElement = new XElement("Formula",
                       new XElement("Elements", new XAttribute("position", position.ToString()), new XAttribute("time", datetime), elementsText.ToString()));
                position++;

                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                XElement condition = new XElement("Condition", new XAttribute("position", position.ToString()), new XAttribute("time", datetime), OperatorConverter.StatisticOperatorToString(op));
                formulaElement.Add(condition);
                position++;

                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                XElement formulaResultName = new XElement("FormulaResultName", new XAttribute("position", position.ToString()), new XAttribute("time", datetime), result.ItemDescription);
                formulaElement.Add(formulaResultName);
                position++;

                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                XElement FormulaResultValue = new XElement("FormulaResultValue", new XAttribute("position", position.ToString()), new XAttribute("time", datetime), result.Item.ToString());
                formulaElement.Add(FormulaResultValue);
                position++;

                XmlLogDocument.Element("XmlLogger").Add(formulaElement);
            }

            /*
             * Max/min/avg formula xml log:
             * <Formula>
             *      <Elements position="1" time="2017-11-22 08:39:15.299">name1=value1, name2=value2, ..., nameN=valueN</Elements>
             *      <Condition position="2" time="2017-11-22 08:39:15.315">SELECT MAX (SELECT MIN, SELECT AVERAGE)</Condition>
             *      <FormulaResultName position="3" time="2017-11-22 08:39:15.316">SomeStandardName</FormulaResultName>
             *      <FormulaResultValue position="4" time="2017-11-22 08:39:15.316">value2</FormulaResultValue>
             *  </Formula>
             */
        }
    }
}
