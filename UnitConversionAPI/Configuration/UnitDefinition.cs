namespace UnitConversionAPI.Configuration
{
    /// <summary>
    /// Defines how a unit is converted to its category's
    /// common base unit.
    /// </summary>
    public class UnitDefinition
    {
        /// <summary>
        /// Multiplication factor used to convert the unit
        /// into the common base unit.
        ///
        /// Example: 1 foot = 0.3048 meters.
        /// </summary>
        public decimal Factor { get; set; }

        /// <summary>
        /// Value added after applying the factor.
        ///
        /// This is required for units with different zero
        /// points, such as Celsius and Fahrenheit.
        /// </summary>
        public decimal Offset { get; set; }
    }
}
