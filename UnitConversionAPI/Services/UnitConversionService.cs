using Microsoft.Extensions.Options;
using UnitConversionAPI.Configuration;

namespace UnitConversionAPI.Services
{
    /// <summary>
    /// Implements the unit conversion logic using factors
    /// and offsets loaded from appsettings.json.
    ///
    /// Keeping this logic in a service separates business
    /// rules from the API controller.
    /// </summary>
    public class UnitConversionService : IUnitConversionService
    {
        // Holds the configuration containing supported categories
        // and their unit definitions.
        private readonly UnitConversionOptions _options;

        /// <summary>
        /// Receives the configured conversion rules through
        /// ASP.NET Core dependency injection.
        /// </summary>
        /// <param name="options">
        /// Strongly typed configuration bound to UnitConversion
        /// in appsettings.json.
        /// </param>
        public UnitConversionService(
            IOptions<UnitConversionOptions> options)
        {
            _options = options.Value;
        }

        /// <summary>
        /// Converts a value from a source unit to a destination unit.
        ///
        /// Conversion happens in two stages:
        /// 1. Convert the source value into its category's base unit.
        /// 2. Convert the base value into the destination unit.
        ///
        /// This approach avoids writing separate formulas for
        /// every possible pair of units.
        /// </summary>
        public decimal Convert(
            decimal value,
            string fromUnit,
            string toUnit)
        {
            // Find the source and destination units in the configuration.
            // FindUnit also identifies the category for each unit.
            var source = FindUnit(fromUnit);
            var target = FindUnit(toUnit);

            // Prevent invalid conversions between unrelated categories,
            // such as meters to kilograms or Celsius to feet.
            if (!string.Equals(
                    source.Category,
                    target.Category,
                    StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException(
                    $"Cannot convert '{fromUnit}' to '{toUnit}'. " +
                    "Both units must belong to the same category.");
            }

            // Stage 1: Convert the source value into the base unit.
            //
            // Formula:
            // BaseValue = (Value * SourceFactor) + SourceOffset
            //
            // Examples:
            // Feet -> meters
            // Celsius -> kelvin
            var baseValue =
                (value * source.Definition.Factor)
                + source.Definition.Offset;

            // Absolute zero is the lowest physically valid temperature.
            // Kelvin is the base unit, so a negative base value is invalid.
            if (source.Category.Equals(
                    "Temperature",
                    StringComparison.OrdinalIgnoreCase)
                && baseValue < 0)
            {
                throw new ArgumentException(
                    "Temperature cannot be below absolute zero.");
            }

            // Stage 2: Convert the base value into the destination unit.
            //
            // Formula:
            // Result = (BaseValue - TargetOffset) / TargetFactor
            //
            // This supports both ordinary units and temperature units
            // that require an offset.
            var result =
                (baseValue - target.Definition.Offset)
                / target.Definition.Factor;

            // Return the final converted value to the caller.
            return result;
        }

        /// <summary>
        /// Searches all configured categories for a specified unit.
        ///
        /// Returns both the category name and its unit definition.
        /// Throws an exception when the unit is not configured.
        /// </summary>
        /// <param name="unitName">The unit name to search for.</param>
        /// <returns>
        /// A tuple containing the category name and unit definition.
        /// </returns>
        private (
            string Category, UnitConversionAPI.Configuration.UnitDefinition Definition) FindUnit(string unitName)
        {
            // Iterate through each category configured in appsettings.json.
            foreach (var category in _options.Categories)
            {
                // Look for the unit inside the current category.
                // The Units dictionary uses case-insensitive lookup.
                if (category.Value.Units.TryGetValue(
                    unitName,
                    out var definition))
                {
                    // A conversion factor must be positive because
                    // the reverse conversion divides by this factor.
                    if (definition.Factor <= 0)
                    {
                        throw new InvalidOperationException(
                            $"Invalid conversion factor for '{unitName}'.");
                    }

                    // Return the category and its matching unit definition.
                    return (category.Key, definition);
                }
            }

            // The requested unit was not found in any category.
            throw new ArgumentException(
                $"Unit '{unitName}' is not supported.");
        }
    }
}
