namespace UnitConversionAPI.Services
{
    /// <summary>
    /// Defines the contract for unit conversion operations.
    ///
    /// The controller depends on this interface instead of
    /// directly depending on the implementation class.
    /// </summary>
    public interface IUnitConversionService
    {
        /// <summary>
        /// Converts a numeric value from one supported unit
        /// to another unit belonging to the same category.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="fromUnit">The source unit name.</param>
        /// <param name="toUnit">The destination unit name.</param>
        /// <returns>The converted numeric value.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when a unit is unsupported or the units
        /// belong to different categories.
        /// </exception>
        decimal Convert(
                decimal value,
                string fromUnit,
                string toUnit);
    }
}
