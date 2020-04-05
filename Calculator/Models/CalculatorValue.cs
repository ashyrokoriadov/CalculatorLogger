namespace Calculator.Models
{
    public class CalculatorValue
    {
        public decimal Value { get; set; }
        public string Name { get; set; }

        public CalculatorValue() { }

        public CalculatorValue(decimal value)
        {
            Value = value;
        }

        public CalculatorValue(decimal value, string name)
        {
            Value = value;
            Name = name;
        }

        public static CalculatorValue Empty() => new CalculatorValue(0.0M, "NULL");
    }
}
