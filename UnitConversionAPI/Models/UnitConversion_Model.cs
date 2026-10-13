using System.ComponentModel.DataAnnotations;

namespace UnitConversionAPI.Models
{
    /// <summary>
    /// Represents a conversion request received from the API caller.
    ///
    /// Example:
    /// {
    ///     "value": 10,
    ///     "fromUnit": "meter",
    ///     "toUnit": "feet"
    /// }
    /// </summary>
    public class UnitConversion_Model
    {
        /// <summary>
        /// Specifies the unit from which the value should be converted.
        ///
        /// Required validation prevents requests without a source unit.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string FromUnit { get; set; } = string.Empty;

        /// <summary>
        /// Specifies the target unit for the conversion.
        ///
        /// Required validation prevents requests without a target unit.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string ToUnit { get; set; } = string.Empty;

        /// <summary>
        /// The numeric value supplied by the caller.
        ///
        /// Decimal supports fractional values, such as 1.75 meters
        /// or 2.5 kilograms.
        /// </summary>
        public decimal Value { get; set; }
    }
}
