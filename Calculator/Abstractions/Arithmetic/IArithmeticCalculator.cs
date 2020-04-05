namespace Calculator.Abstractions.Arithmetic
{
    public interface IArithmeticCalculator<T> : IAdder<T>, ISubtractor<T>, IMultiplier<T>, IDivider<T>
    { }
}
