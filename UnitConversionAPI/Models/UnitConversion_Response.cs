namespace UnitConversionAPI.Models
{
    /// <summary>
    /// Represents the result returned to the API caller
    /// after the conversion has been completed.
    /// </summary>
    public class UnitConversion_Response
    {
        /// <summary>
        /// The original numeric value received in the request.
        /// </summary>
        public decimal OriginalValue { get; set; }

        /// <summary>
        /// The source unit used in the conversion.
        /// </summary>
        public string FromUnit { get; set; } = string.Empty;

        /// <summary>
        /// The destination unit requested by the caller.
        /// </summary>
        public string ToUnit { get; set; } = string.Empty;

        /// <summary>
        /// The calculated value expressed in the destination unit.
        /// </summary>
        public decimal ConvertedValue { get; set; }
    }
}
