using System;

namespace CalculatorLoggerLibrary.Models
{
    /// <summary>
    /// Basic unit to be used in calculation. 
    /// </summary>
    /// <remarks>
    /// It encapsulates 2 properties to represent value and its name.
    /// </remarks>
    public class CalculationUnit : IComparable
    {
        /// <summary>
        /// A constructor to create <c>CalculationUnit</c> object.
        /// </summary>
        /// <seealso cref="CalculationUnit(decimal, string)"/>
        /// <example>
        /// <code>
        /// CalculationUnit calculationUnit1 = new CalculationUnit();
        /// </code>
        /// </example>
        public CalculationUnit(){}

        /// <summary>
        /// A constructor to create <c>CalculationUnit</c> object.
        /// </summary>
        /// <param name="item">A value of <c>CalculationUnit</c> object to be used in calculations</param>
        /// <param name="itemDescription">A description of the value stored in <c>CalculationUnit</c> object</param>
        /// <seealso cref="CalculationUnit()"/>
        /// <example>
        /// <code>
        /// CalculationUnit calculationUnit1 = new CalculationUnit(-9.8M, "TestValueName");
        /// </code>
        /// </example>
        public CalculationUnit(decimal item, string itemDescription):this()
        {
            Item = item;
            ItemDescription = itemDescription;
        }

        /// <summary>
        /// A value of <c>CalculationUnit</c> object to be used in calculations
        /// </summary>
        public decimal Item { get; set; }

        /// <summary>
        /// A description of the value stored in <c>CalculationUnit</c> object
        /// </summary>
        public string ItemDescription { get; protected set; }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">An object to be compared with current instance</param>
        /// <returns>An integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.</returns>
        /// <exception cref="System.ArgumentException">Thrown when a supplid obj parameter is not of type <c>CalculationUnit</c>.</exception>
        public int CompareTo(object obj)
        {
            if (obj is CalculationUnit)
            {
                CalculationUnit compareToObject = (CalculationUnit)obj;
                if (Item > compareToObject.Item)
                {
                    return 1;
                }
                else if (Item < compareToObject.Item)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                throw new ArgumentException("Supplied object is not of CalculationUnit type.");
            }
        }        

        public static bool operator >(CalculationUnit cu1, CalculationUnit cu2)
        {
            return cu1.Item >cu2.Item;
        }

        public static bool operator <(CalculationUnit cu1, CalculationUnit cu2)
        {
            return cu1.Item < cu2.Item;
        }

        public static bool operator >=(CalculationUnit cu1, CalculationUnit cu2)
        {
            return cu1.Item >= cu2.Item || cu1.Item == cu2.Item;
        }

        public static bool operator <=(CalculationUnit cu1, CalculationUnit cu2)
        {
            return cu1.Item <= cu2.Item || cu1.Item == cu2.Item;
        }        
    }
}
