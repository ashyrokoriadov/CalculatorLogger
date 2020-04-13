using System.Linq;

namespace CalculatorLoggerLibrary.Models.Operations
{
    /// <summary>
    /// A class resolving several multiple conditions.
    /// </summary>
    public class Switch
    {
        /// <summary>
        /// A value to be used as string identifier for a switch result.
        /// </summary>
        public string SwitchResultName { get; protected set; }

        /// <summary>
        /// A value to be returned after switch resolving.
        /// </summary>
        public decimal SwitchValue { get; protected set; }
    }
}
