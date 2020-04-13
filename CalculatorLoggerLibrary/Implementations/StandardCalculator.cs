using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using CalculatorLoggerLibrary.Interfaces;
using CalculatorLoggerLibrary.Models;
using CalculatorLoggerLibrary.Models.Operations;

namespace CalculatorLoggerLibrary.Implementations
{
    /// <summary>
    /// A class used to perform calculations with logging functionality.
    /// </summary>
    public sealed class StandardCalculator : IEnumerable, ICalculator
    {
        private Dictionary<string, decimal> _dictionary = new Dictionary<string, decimal>();
        private IFormulaLogger _formulaLogger;

        /// <summary>
        /// A constructor for a StandardCalculator class implementing ICalculator, IDataManager, IEnumerable interfaces.
        /// </summary>
        /// <param name="FormulaLogger">A class implementing IFormulaLogger interface.</param>
        public StandardCalculator(IFormulaLogger FormulaLogger) 
        {
            _formulaLogger = FormulaLogger;
        }

        /// <summary>
        /// An indexer to return a value in an internal collection of a Calculator class by name.
        /// </summary>
        /// <param name="index">A string name(index) of a value stored in an internal collection of a Calculator class</param>
        /// <returns>Returns a value from a Calculator collection's implementation corresponding to an index.</returns>
        public decimal this[string index]
        {
            get
            {
                return _dictionary[index];
            }
            set
            {
                _dictionary[index] = value;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        /// <summary>
        /// Returns an instance of <c>IFormulaLogger</c> object.
        /// </summary>
        public IFormulaLogger FormulaLogger
        {
            get { return _formulaLogger; }
        }

        /// <summary>
        /// A method adds an element to a calculation data collection.
        /// </summary>
        /// <param name="cu">An element to be added to a calculation data collection.</param>
        public void Add(CalculationUnit cu)
        {
            if (cu != null)
            {
                _dictionary.Add(cu.ItemDescription, cu.Item);
            }
        }

        /// <summary>
        /// A method removes an element from a calculation data collection.
        /// </summary>
        /// <param name="cu">An element to be removed from a calculation data collection.</param>
        public void Remove(CalculationUnit cu)
        {
            if (cu != null)
            {
                bool IsValidItem = _dictionary.ContainsKey(cu.ItemDescription);
                if (IsValidItem)
                {
                    _dictionary.Remove(cu.ItemDescription);
                }
            }
        }

        /// <summary>
        /// A method removes an element from a calculation data collection.
        /// </summary>
        /// <param name="index">An index of an element to be removed from a calculation data collection.</param>
        public void Remove(string index)
        {
            if (!string.IsNullOrEmpty(index))
            {
                {
                    bool IsValidItem = _dictionary.ContainsKey(index);
                    if (IsValidItem)
                    {
                        _dictionary.Remove(index);
                    }
                }
            }
        }

        /// <summary>
        /// Adds all elements in an internal collection passed as string indetifiers to the method.
        /// </summary>
        /// <param name="resultValueName">A name of result to be used in further calculations</param>
        /// <param name="values">String indetifiers in an internal collection to be used in a calculation</param>
        /// <returns>A sum of all values with passed string indetifiers</returns>
        public CalculationUnit Add(string resultValueName, params string[] values)
        {
            List<CalculationUnit> valuesList = new List<CalculationUnit>();
            decimal result = 0M;

            if (values != null)
            {
                foreach (string item in values)
                {
                    decimal value = 0M;
                    bool Exists = false;
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        Exists = _dictionary.TryGetValue(item, out value);
                    }

                    if (Exists)
                        result += value;

                    CalculationUnit cu = new CalculationUnit(value, item);
                    valuesList.Add(cu);
                }
            }
            CalculationUnit ResultCalcUnit = new CalculationUnit(result, resultValueName != null ? resultValueName : "SumResult");

            _formulaLogger.LogOperation(MathOperator.Add, ResultCalcUnit, valuesList.ToArray());

            return ResultCalcUnit;
        }

        /// <summary>
        /// Subtracts all elements in an internal collection passed as string indetifiers to the method.
        /// </summary>
        /// <param name="resultValueName">A name of result to be used in further calculations</param>
        /// <param name="values">String indetifiers in an internal collection to be used in a calculation</param>
        /// <returns>A  difference of all values with passed string indetifiers</returns>
        public CalculationUnit Subtract(string resultValueName, params string[] values)
        {
            List<CalculationUnit> valuesList = new List<CalculationUnit>();

            decimal result = 0M;
            decimal value = 0M;

            if (values != null)
            {
                bool Exists = _dictionary.TryGetValue(values[0], out value);
                if (Exists)
                    result += value;

                CalculationUnit firstElement = new CalculationUnit(value, values[0]);
                valuesList.Add(firstElement);

                foreach (string item in values)
                {
                    if (Array.IndexOf(values, item) != 0)
                    {
                        value = 0M;
                        Exists = false;
                        if (!string.IsNullOrWhiteSpace(item))
                        {
                            Exists = _dictionary.TryGetValue(item, out value);
                        }

                        if (Exists)
                            result -= value;

                        CalculationUnit cu = new CalculationUnit(value, item);
                        valuesList.Add(cu);
                    }
                }
            }
            CalculationUnit ResultCalcUnit = new CalculationUnit(result, resultValueName != null ? resultValueName: "SubtractResult");

            _formulaLogger.LogOperation(MathOperator.Subtract, ResultCalcUnit, valuesList.ToArray());

            return ResultCalcUnit; 
        }

        /// <summary>
        /// Multiples all elements in an internal collection passed as string indetifiers to the method.
        /// </summary>
        /// <param name="resultValueName">A name of result to be used in further calculations</param>
        /// <param name="values">String indetifiers in an internal collection to be used in a calculation</param>
        /// <returns>A product of all values with passed string indetifiers</returns>
        public CalculationUnit Multiple(string resultValueName, params string[] values)
        {
            List<CalculationUnit> valuesList = new List<CalculationUnit>();

            decimal result = 1M;

            if (values != null)
            {
                foreach (string item in values)
                {
                    decimal value = 1M;
                    bool Exists = false;
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        Exists = _dictionary.TryGetValue(item, out value);
                    }

                    if (Exists)
                        result *= value;

                    CalculationUnit cu = new CalculationUnit(value, item);
                    valuesList.Add(cu);
                }
            }
            CalculationUnit ResultCalcUnit = new CalculationUnit(result, resultValueName != null ? resultValueName : "ProductResult");

            _formulaLogger.LogOperation(MathOperator.Multiplication, ResultCalcUnit, valuesList.ToArray());

            return ResultCalcUnit; 
        }

        /// <summary>
        /// Divides all elements in an internal collection passed as string indetifiers to the method.
        /// </summary>
        /// <param name="resultValueName">A name of result to be used in further calculations.</param>
        /// <param name="dividend">A dividend.</param>
        /// <param name="divisor">A divisor.</param>
        /// <returns>A quotient of all values with passed string indetifiers</returns>
        public CalculationUnit Divide(string resultValueName, string dividend, string divisor)
        {
            List<CalculationUnit> valuesList = new List<CalculationUnit>();

            decimal dividendValue = 0M;
            if (!string.IsNullOrEmpty(dividend))
            {
                bool dividendExists = _dictionary.TryGetValue(dividend, out dividendValue);
            }
            CalculationUnit dividendCalcUnit = new CalculationUnit(dividendValue, dividend);
            valuesList.Add(dividendCalcUnit);

            decimal divisorValue = 0M;
            if (!string.IsNullOrEmpty(divisor))
            {
                bool divisorExists = _dictionary.TryGetValue(divisor, out divisorValue);
            }
            CalculationUnit divisorCalcUnit = new CalculationUnit(divisorValue, divisor);
            valuesList.Add(divisorCalcUnit);

            resultValueName = resultValueName != null ? resultValueName : "QuotientResult";

            if (divisorValue != 0M)
            {
                decimal result = dividendValue / divisorValue;
                CalculationUnit ResultCalcUnit = new CalculationUnit(result, resultValueName);
                _formulaLogger.LogOperation(MathOperator.Division, ResultCalcUnit, valuesList.ToArray());
                return ResultCalcUnit;
            }
            else
            {
                CalculationUnit ResultCalcUnit = new CalculationUnit(0M, resultValueName);
                _formulaLogger.LogOperation(MathOperator.Division, ResultCalcUnit, valuesList.ToArray());
                return ResultCalcUnit;
            }
        }

        /// <summary>
        /// Gets a maximum of all elements in an internal collection passed as string indetifiers to the method.
        /// </summary>
        /// <param name="resultValueName">A name of result to be used in further calculations</param>
        /// <param name="values">String indetifiers in an internal collection to be used in a calculation</param>
        /// <returns>A maximum of all values with passed string indetifiers</returns>
        public CalculationUnit Max(string resultValueName, params string[] values)
        {
            return StatisticalOperation(resultValueName, GetMax, StatisticalOperator.Max, values);
        }

        /// <summary>
        /// Gets a minimum of all elements in an internal collection passed as string indetifiers to the method.
        /// </summary>
        /// <param name="resultValueName">A name of result to be used in further calculations</param>
        /// <param name="values">String indetifiers in an internal collection to be used in a calculation</param>
        /// <returns>A minimum of all values with passed string indetifiers</returns>
        public CalculationUnit Min(string resultValueName, params string[] values)
        {
            return StatisticalOperation(resultValueName, GetMin, StatisticalOperator.Min, values);
        }

        /// <summary>
        /// Gets an average of all elements in an internal collection passed as string indetifiers to the method.
        /// </summary>
        /// <param name="resultValueName">A name of result to be used in further calculations</param>
        /// <param name="values">String indetifiers in an internal collection to be used in a calculation</param>
        /// <returns>An average of all values with passed string indetifiers</returns>
        public CalculationUnit Average(string resultValueName, params string[] values)
        {
            return StatisticalOperation(resultValueName, GetAverage, StatisticalOperator.Avg, values);
        }

      

      
        
       

        #region Private Methods
        private decimal GetMax(CalculationUnit[] data)
        {
            return data.Select(i => i.Item).Max();
        }

        private decimal GetMin(CalculationUnit[] data)
        {
            return data.Select(i => i.Item).Min();
        }

        private decimal GetAverage(CalculationUnit[] data)
        {
            return data.Select(i => i.Item).Average();
        }
        
        private CalculationUnit StatisticalOperation(string resultValueName, Func<CalculationUnit[], decimal> function, StatisticalOperator op, params string[] values)
        {
            List<CalculationUnit> valuesList = new List<CalculationUnit>();

            if (values != null)
            {
                foreach (string item in values)
                {
                    decimal value = 0M;
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        _dictionary.TryGetValue(item, out value);
                        CalculationUnit cu = new CalculationUnit(value, item);
                        valuesList.Add(cu);
                    }
                }
            }

            decimal resultValue = 0;

            if (valuesList.Count > 0)
            {
                resultValue = function(valuesList.ToArray());
            }

            CalculationUnit ResultCalcUnit = new CalculationUnit(resultValue, resultValueName != null ? resultValueName : "StatisticFunctionResult");
            _formulaLogger.LogStatisticalFormula(op, ResultCalcUnit, valuesList.ToArray());

            return ResultCalcUnit;
        }
        #endregion
    }
}
