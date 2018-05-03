namespace CalculatorLoggerLibrary.Interfaces
{
    /// <summary>
    /// An interface containing functionality declaration for getting log data from a logger.
    /// </summary>
    public interface IDataManager
    {
        /// <summary>
        /// A method gets data from a logger.
        /// </summary>
        /// <returns>An object representing logged data.</returns>
        object GetLogData();
    }
}
