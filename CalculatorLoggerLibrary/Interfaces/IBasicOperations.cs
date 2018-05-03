using CalculatorLoggerLibrary.Models;

namespace CalculatorLoggerLibrary.Interfaces
{
    /// <summary>
    /// An interface containing basic mathematical operations.
    /// </summary>
    public interface IBasicOperations
    {
        /// <summary>
        /// Adds all elements in an internal collection passed as string indetifiers to the method.
        /// </summary>
        /// <param name="resultValueName">A name of result to be used in further calculations</param>
        /// <param name="values">String indetifiers in an internal collection to be used in a calculation</param>
        /// <returns>A sum of all values with passed string indetifiers</returns>
        CalculationUnit Add(string resultValueName, params string[] values);

        /// <summary>
        /// Subtracts all elements in an internal collection passed as string indetifiers to the method.
        /// </summary>
        /// <param name="resultValueName">A name of result to be used in further calculations</param>
        /// <param name="values">String indetifiers in an internal collection to be used in a calculation</param>
        /// <returns>A  difference of all values with passed string indetifiers</returns>
        CalculationUnit Subtract(string resultValueName, params string[] values);

        /// <summary>
        /// Multiples all elements in an internal collection passed as string indetifiers to the method.
        /// </summary>
        /// <param name="resultValueName">A name of result to be used in further calculations</param>
        /// <param name="values">String indetifiers in an internal collection to be used in a calculation</param>
        /// <returns>A product of all values with passed string indetifiers</returns>
        CalculationUnit Multiple(string resultValueName, params string[] values);

        /// <summary>
        /// Divides all elements in an internal collection passed as string indetifiers to the method.
        /// </summary>
        /// <param name="resultValueName">A name of result to be used in further calculations.</param>
        /// <param name="dividend">A dividend.</param>
        /// <param name="divisor">A divisor.</param>
        /// <returns>A quotient of all values with passed string indetifiers</returns>
        CalculationUnit Divide(string resultValueName, string dividend, string divisor);
    }
}
