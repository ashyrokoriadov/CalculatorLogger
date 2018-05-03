using System.Collections.Generic;
using System.Linq;

namespace CalculatorLoggerLibrary.Models.Operations
{
    /// <summary>
    /// A class to represent and to resolve bands.
    /// </summary>
    public class Band
    {
        private Dictionary<decimal, decimal> bands;

        private CalculationUnit valueToCompare;

        /// <summary>
        /// A value to be evaluated within a band.
        /// </summary>
        public CalculationUnit ValueToCompare
        {
            get
            {
                return valueToCompare;
            }
            set
            {
                valueToCompare = value;
                if(bands.Count>0)
                    SetResult();
            }
        }

        /// <summary>
        /// A band name
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// A maximum value of a band used to define a result value
        /// </summary>
        public decimal ResultMaxValue { get; protected set; }

        /// <summary>
        /// A band result value
        /// </summary>
        public decimal ResultValue { get; protected set; }

        /// <summary>
        /// A Band class constructor, a band values collection is empty and you have to invoke method SetBand to fill it.
        /// </summary>
        /// <param name="name">A band name</param>
        /// <seealso cref="Band.SetBand(Dictionary{decimal, decimal})"/>
        public Band(string name) : this(name, new Dictionary<decimal, decimal>()) { }

        /// <summary>
        /// A Band class constructor.
        /// </summary>
        /// <param name="name">A band name</param>
        /// <param name="bands">A band's collection</param>
        public Band(string name, Dictionary<decimal, decimal> bands)
        {
            Name = name;
            this.bands = bands;
        }

        /// <summary>
        /// A method sets a band collection.
        /// </summary>
        /// <param name="bands">A band collection to be passed to a <c>Band</c> object</param>
        public void SetBand(Dictionary<decimal, decimal> bands)
        {
            this.bands = bands;
            if (valueToCompare != null)
                SetResult();
        }

        /// <summary>
        /// A method resolves a band.
        /// </summary>
        public void SetResult()
        {
            KeyValuePair<decimal, decimal> result = bands.Count > 0 ?
                                                   bands.Where(x=>GetComparisonResult(x))
                                                   .Select(x => new KeyValuePair<decimal, decimal>(x.Key, x.Value))
                                                   .FirstOrDefault()
                                                   : new KeyValuePair<decimal, decimal>(-1M, -1M);
            ResultMaxValue = result.Key;
            ResultValue = result.Value;
        }

        bool GetComparisonResult(KeyValuePair<decimal, decimal> data)
        {
            if (ValueToCompare !=null)
                return data.Key > ValueToCompare.Item ? true : false;

            return false;
        }
    }
}
