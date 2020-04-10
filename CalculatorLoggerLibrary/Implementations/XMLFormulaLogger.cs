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
        /// A method logs a resolving of <c>SingleCondition</c>.
        /// </summary>
        /// <param name="sc">A single condition to be resolved</param>
        /// <param name="newValueName">A name of a single condition result to be used in calculations</param>
        public void LogCondition(SingleCondition sc, string newValueName)
        {
            StringBuilder formulaText = new StringBuilder();
            StringBuilder formulaResolutionText = new StringBuilder();

            formulaText.AppendFormat("{0} {1} {2} ? {3} : {4}", sc.ValueToCompare.ItemDescription, OperatorConverter.LogicOperatorToString(sc.ConditionOperator), sc.Sample.ItemDescription, sc.ValueIfTrue, sc.ValueIfFalse);
            formulaResolutionText.AppendFormat("{0} {1} {2} = {3}", sc.ValueToCompare.Item, OperatorConverter.LogicOperatorToString(sc.ConditionOperator), sc.Sample.Item, sc.ConditionResult.ToString());

            XElement conditionElement = XmlLogCodition(sc, newValueName, formulaText, formulaResolutionText);
            if(conditionElement !=null)
                XmlLogDocument.Element("XmlLogger").Add(conditionElement);

            /*
             * Simple condition resolution xml log:
             * <Condition>
             *  <Formula position="1" time="2017-11-22 08:39:15.299">name1 "condition operator" valueToCompare ? VALUE-IF-TRUE : VALUE-IF-FALSE</Formula>
             *  <Resolution position="2" time="2017-11-22 08:39:15.315">11.5 < 0 = [Result]</Resolution>
             *  <ResultName position="3" time="2017-11-22 08:39:15.316">SomeName</ResultName>
             *  <ResultValue position="4" time="2017-11-22 08:39:15.316">VALUE-IF-FALSE</ResultValue>
             * </Condition>
             */
        }

        /// <summary>
        /// A method logs a resolving of <c>MultiCondition</c>.
        /// </summary>
        /// <param name="mc">A multi condition to be resolved</param>
        public void LogMultiCondition(MultiCondition mc)
        {
            lock (_lockObject)
            {
                string datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                XElement multiConditionElement = new XElement("MultiCondition",
                       new XElement("ValueIfTrue", new XAttribute("position", position.ToString()), new XAttribute("time", datetime), mc.ValueIfTrue));
                position++;

                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                XElement ValueIfFalse = new XElement("ValueIfFalse",
                    new XAttribute("position", position.ToString()), new XAttribute("time", datetime), mc.ValueIfFalse);
                multiConditionElement.Add(ValueIfFalse);
                position++;

                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                XElement LogicalOperator = new XElement("LogicalOperator",
                    new XAttribute("position", position.ToString()), new XAttribute("time", datetime), mc.LogicOperator.ToString().ToUpper());
                multiConditionElement.Add(LogicalOperator);
                position++;

                XElement conditions = new XElement("Conditions");
                int counter = 1;
                foreach (SingleCondition simpleCondition in mc.SingleConditions)
                {
                    LogCondition(simpleCondition, string.Format("TemporaryValue{0}", counter.ToString()), conditions);
                    counter++;
                }
                multiConditionElement.Add(conditions);

                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                XElement ResultName = new XElement("ResultName",
                    new XAttribute("position", position.ToString()), new XAttribute("time", datetime), mc.ConditionResult.ToString());
                multiConditionElement.Add(ResultName);
                position++;

                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                XElement ResultValue = new XElement("ResultValue",
                    new XAttribute("position", position.ToString()), new XAttribute("time", datetime), mc.ConditionValue);
                multiConditionElement.Add(ResultValue);
                position++;

                XmlLogDocument.Element("XmlLogger").Add(multiConditionElement);
            }

            /*  Multi condition resolution xml log:
             *  <MultiCondition>
             *      <ValueIfTrue position="1" time="2017-11-22 08:39:15.298">TRUE-VALUE</ValueIfTrue>
             *      <ValueIfFalse position="2" time="2017-11-22 08:39:15.298">FALSE-VALUE</ValueIfFalse>
             *      <LogicalOperator position="3" time="2017-11-22 08:39:15.298">AND</LogicalOperator>
             *      <Conditions>
             *          <Condition>
             *              <Formula position="4" time="2017-11-22 08:39:15.299">name1 "condition operator" valueToCompare</Formula>
             *              <Resolution position="5" time="2017-11-22 08:39:15.315">11.5 < 0 = [Result]</Resolution>
             *              <ResultName position="6" time="2017-11-22 08:39:15.316">SomeName</ResultName>
             *              <ResultValue position="7" time="2017-11-22 08:39:15.316">VALUE-IF-FALSE</ResultValue>
             *          </Condition>
             *          <Condition>
             *              <Formula position="4" time="2017-11-22 08:39:15.299">name1 "condition operator" valueToCompare</Formula>
             *              <Resolution position="5" time="2017-11-22 08:39:15.315">11.5 > 0 = [Result]</Resolution>
             *              <ResultName position="6" time="2017-11-22 08:39:15.316">SomeName</ResultName>
             *              <ResultValue position="7" time="2017-11-22 08:39:15.316">VALUE-IF-FALSE</ResultValue>
             *          </Condition>
             *          <Condition>
             *              <Formula position="8" time="2017-11-22 08:39:15.299">name1 "condition operator" valueToCompare</Formula>
             *              <Resolution position="9" time="2017-11-22 08:39:15.315">11.5 == 0 = [Result]</Resolution>
             *              <ResultName position="10" time="2017-11-22 08:39:15.316">SomeName</ResultName>
             *              <ResultValue position="11" time="2017-11-22 08:39:15.316">VALUE-IF-FALSE</ResultValue>
             *          </Condition>
             *      </Conditions>
             *      <ResultName position="10" time="2017-11-22 08:39:15.319">False</ResultName>
             *      <ResultValue position="11" time="2017-11-22 08:39:15.319">FALSE-VALUE</ResultValue>
             *  </MultiCondition>
             */
        }

        /// <summary>
        /// A method logs a switch resolving.
        /// </summary>
        /// <param name="mc">An array of multi conditions that were used to create a switch</param>
        /// <param name="result">A result returned be a switch resolving</param>
        public void LogSwitch(MultiCondition[] mc, CalculationUnit result)
        {
            lock (_lockObject)
            {
                XElement switchElement = new XElement("Switch");

                XElement multiConditionsElement = new XElement("MultiConditions");
                foreach (MultiCondition multiCondition in mc)
                {
                    LogMultiCondition(multiCondition, multiConditionsElement);
                }
                switchElement.Add(multiConditionsElement);

                string datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);

                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                XElement ResultName = new XElement("ResultName",
                    new XAttribute("position", position.ToString()), new XAttribute("time", datetime), result.ItemDescription);
                switchElement.Add(ResultName);
                position++;

                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                XElement ResultValue = new XElement("ResultValue",
                    new XAttribute("position", position.ToString()), new XAttribute("time", datetime), result.Item.ToString());
                switchElement.Add(ResultValue);
                position++;

                XmlLogDocument.Element("XmlLogger").Add(switchElement);
            }

            /*Switch statement resolution xml log:             * 
             *  <Switch>
             *      <MultiConditions>
             *          <MultiCondition>...</MultiCondition>
             *          <MultiCondition>...</MultiCondition>
             *          <MultiCondition>...</MultiCondition>
             *      </MultiConditions>
             *      <ResultName position="10" time="2017-11-22 08:39:15.319">SomeStandardName</ResultName>
             *      <ResultValue position="11" time="2017-11-22 08:39:15.319">SOME-VALUE</ResultValue>
             *  </Switch>
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

       

          
       

        #region Private Methods
        private void LogCondition(SingleCondition sc, string newValueName, XElement element)
        {
            StringBuilder formulaText = new StringBuilder();
            StringBuilder formulaResolutionText = new StringBuilder();

            formulaText.AppendFormat("{0} {1} {2} ? {3} : {4}", sc.ValueToCompare.ItemDescription, OperatorConverter.LogicOperatorToString(sc.ConditionOperator), sc.Sample.ItemDescription, sc.ValueIfTrue, sc.ValueIfFalse);
            formulaResolutionText.AppendFormat("{0} {1} {2} = {3}", sc.ValueToCompare.Item, OperatorConverter.LogicOperatorToString(sc.ConditionOperator), sc.Sample.Item, sc.ConditionResult.ToString());

            XElement conditionElement = XmlLogCodition(sc, newValueName, formulaText, formulaResolutionText);
            if (conditionElement != null)
                element.Add(conditionElement);
        }

        private void LogMultiCondition(MultiCondition mc, XElement element)
        {
            XElement multiConditionElement = XmlLogMultiCondition(mc);
            if (multiConditionElement != null)
                element.Add(multiConditionElement);
        }

        private XElement XmlLogCodition(SingleCondition sc, string newValueName, StringBuilder formulaText, StringBuilder formulaResolutionText)
        {
            lock (_lockObject)
            {
                string datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                XElement conditionElement = new XElement("Condition",
                       new XElement("Formula", new XAttribute("position", position.ToString()), new XAttribute("time", datetime), formulaText.ToString()));
                position++;

                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                XElement Resolution = new XElement("Resolution",
                    new XAttribute("position", position.ToString()), new XAttribute("time", datetime), formulaResolutionText.ToString());
                conditionElement.Add(Resolution);
                position++;

                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                XElement ResultName = new XElement("ResultName",
                    new XAttribute("position", position.ToString()), new XAttribute("time", datetime), newValueName);
                conditionElement.Add(ResultName);
                position++;

                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                XElement ResultValue = new XElement("ResultValue",
                    new XAttribute("position", position.ToString()), new XAttribute("time", datetime), sc.ConditionValue.ToString());
                conditionElement.Add(ResultValue);
                position++;

                return conditionElement;
            }
        }

        private XElement XmlLogMultiCondition(MultiCondition mc)
        {
            lock (_lockObject)
            {
                string datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                XElement multiConditionElement = new XElement("MultiCondition",
                       new XElement("ValueIfTrue", new XAttribute("position", position.ToString()), new XAttribute("time", datetime), mc.ValueIfTrue));
                position++;

                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                XElement ValueIfFalse = new XElement("ValueIfFalse",
                    new XAttribute("position", position.ToString()), new XAttribute("time", datetime), mc.ValueIfFalse);
                multiConditionElement.Add(ValueIfFalse);
                position++;

                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                XElement LogicalOperator = new XElement("LogicalOperator",
                    new XAttribute("position", position.ToString()), new XAttribute("time", datetime), mc.LogicOperator.ToString().ToUpper());
                multiConditionElement.Add(LogicalOperator);
                position++;

                XElement conditions = new XElement("Conditions");
                int counter = 1;
                foreach (SingleCondition simpleCondition in mc.SingleConditions)
                {
                    LogCondition(simpleCondition, string.Format("TemporaryValue{0}", counter.ToString()), conditions);
                    counter++;
                }
                multiConditionElement.Add(conditions);

                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                XElement ResultName = new XElement("ResultName",
                    new XAttribute("position", position.ToString()), new XAttribute("time", datetime), mc.ConditionResult.ToString());
                multiConditionElement.Add(ResultName);
                position++;

                datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                XElement ResultValue = new XElement("ResultValue",
                    new XAttribute("position", position.ToString()), new XAttribute("time", datetime), mc.ConditionValue);
                multiConditionElement.Add(ResultValue);
                position++;

                return multiConditionElement;
            }
        }
        #endregion
    }
}
