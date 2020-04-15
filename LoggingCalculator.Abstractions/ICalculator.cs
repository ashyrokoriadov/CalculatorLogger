using LoggingCalculator.AbstractionsAndModels.Aggregations;
using LoggingCalculator.AbstractionsAndModels.Arithmetic;

namespace LoggingCalculator.AbstractionsAndModels
{
    public interface ICalculator<T> : IArithmeticCalculator<T>, IAggregationCalculator<T>
    {
    }
}
