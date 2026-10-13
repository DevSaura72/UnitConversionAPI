using Microsoft.AspNetCore.Mvc;
using UnitConversionAPI.Models;
using UnitConversionAPI.Services;

namespace UnitConversionAPI.Controllers
{
    /// <summary>
    /// Exposes HTTP endpoints for converting measurement units.
    ///
    /// The API controller accepts JSON requests and returns
    /// JSON responses to clients.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UnitConversionController : ControllerBase
    {
        // Service responsible for all conversion calculations.
        private readonly IUnitConversionService _conversionService;

        /// <summary>
        /// Receives the conversion service through dependency injection.
        /// </summary>
        public UnitConversionController(
            IUnitConversionService conversionService)
        {
            _conversionService = conversionService;
        }

        /// <summary>
        /// Converts a numeric value from one unit to another.
        ///
        /// Endpoint: POST /api/UnitConversion/convert
        ///
        /// The request body contains the value, source unit,
        /// and destination unit.
        /// </summary>
        /// <param name="request">Conversion details supplied by the caller.</param>
        /// <returns>
        /// HTTP 200 with the conversion result, or HTTP 400
        /// when the conversion request is invalid.
        /// </returns>
        [HttpPost("convert")]
        [ProducesResponseType(typeof(UnitConversion_Response),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UnitConversion_Response> Convert(
            [FromBody] UnitConversion_Model request)
        {
            try
            {
                // Delegate the conversion calculation to the service.
                // The controller does not need to know the conversion formula.
                var convertedValue = _conversionService.Convert(
                    request.Value,
                    request.FromUnit,
                    request.ToUnit);

                // Prepare a structured response containing the original
                // request details and the calculated value.
                var response = new UnitConversion_Response
                {
                    OriginalValue = request.Value,
                    FromUnit = request.FromUnit,
                    ToUnit = request.ToUnit,
                    ConvertedValue = convertedValue
                };

                // Return HTTP 200 OK with the result as JSON.
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                // Return HTTP 400 when a unit is unsupported,
                // categories do not match, or a temperature is invalid.
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
