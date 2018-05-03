using CalculatorLoggerLibrary.Models;
using CalculatorLoggerLibrary.Models.Operations;

namespace CalculatorLoggerLibrary.Interfaces
{
    /// <summary>
    /// An interface containing statistical operations.
    /// </summary>
    public interface IStatisticalOperation
    {
        /// <summary>
        /// Gets a maximum of all elements in an internal collection passed as string indetifiers to the method.
        /// </summary>
        /// <param name="resultValueName">A name of result to be used in further calculations</param>
        /// <param name="values">String indetifiers in an internal collection to be used in a calculation</param>
        /// <returns>A maximum of all values with passed string indetifiers</returns>
        CalculationUnit Max(string resultValueName, params string[] values);

        /// <summary>
        /// Gets a minimum of all elements in an internal collection passed as string indetifiers to the method.
        /// </summary>
        /// <param name="resultValueName">A name of result to be used in further calculations</param>
        /// <param name="values">String indetifiers in an internal collection to be used in a calculation</param>
        /// <returns>A minimum of all values with passed string indetifiers</returns>
        CalculationUnit Min(string resultValueName, params string[] values);

        /// <summary>
        /// Gets an average of all elements in an internal collection passed as string indetifiers to the method.
        /// </summary>
        /// <param name="resultValueName">A name of result to be used in further calculations</param>
        /// <param name="values">String indetifiers in an internal collection to be used in a calculation</param>
        /// <returns>An average of all values with passed string indetifiers</returns>
        CalculationUnit Average(string resultValueName, params string[] values);
    }
}
