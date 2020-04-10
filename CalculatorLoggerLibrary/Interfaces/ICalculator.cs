namespace CalculatorLoggerLibrary.Interfaces
{
    /// <summary>
    /// An interface to be implemented by a class that contains all calculations.
    /// </summary>
    public interface ICalculator : IStatisticalOperation, IConditionResolver
    {
        /// <summary>
        /// Returns an instance of <c>IFormulaLogger</c> object.
        /// </summary>
        IFormulaLogger FormulaLogger 
        {
            get;
        }

        /// <summary>
        /// An indexer to return a value in an internal collection of a Calculator class by name.
        /// </summary>
        /// <param name="index">A string name(index) of a value stored in an internal collection of a Calculator class</param>
        /// <returns>Returns a value from a Calculator collection's implementation corresponding to an index.</returns>
        decimal this[string index]
        {
            get; set;
        }    
    }
}
