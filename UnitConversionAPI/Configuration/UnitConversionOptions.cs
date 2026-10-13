namespace UnitConversionAPI.Configuration
{
    /// <summary>
    /// Represents the complete unit conversion configuration
    /// loaded from the UnitConversion section in appsettings.json.
    ///
    /// Categories contain groups such as Length, Weight,
    /// and Temperature.
    /// </summary>
    public class UnitConversionOptions
    {
        
        /// <summary>
        /// Stores all configured unit categories.
        ///
        /// The dictionary key is the category name, such as
        /// "Length" or "Weight".
        ///
        /// OrdinalIgnoreCase allows category names to be
        /// compared without case sensitivity.
        /// </summary>
        public Dictionary<string, UnitCategoryOptions> Categories
        {
            get;
            set;
        } = new(StringComparer.OrdinalIgnoreCase);
    }
}
