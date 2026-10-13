namespace UnitConversionAPI.Configuration
{
    /// <summary>
    /// Represents a category of units that can be converted
    /// into one another, such as meters and feet.
    /// </summary>
    public class UnitCategoryOptions
    {
        /// <summary>
        /// Defines the common base unit used for conversions.
        ///
        /// Example: meter for Length, kilogram for Weight,
        /// and kelvin for Temperature.
        /// </summary>
        public string BaseUnit { get; set; } = string.Empty;

        /// <summary>
        /// Stores the units available within this category.
        ///
        /// The key is the unit name and the value contains
        /// its conversion factor and offset.
        ///
        /// Case-insensitive lookup allows "Meter", "METER",
        /// and "meter" to refer to the same unit.
        /// </summary>
        public Dictionary<string, UnitDefinition> Units
        {
            get;
            set;
        } = new(StringComparer.OrdinalIgnoreCase);
    }
}
