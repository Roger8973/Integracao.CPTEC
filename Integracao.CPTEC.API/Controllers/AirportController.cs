using Integracao.CPTEC.Application;
using Integracao.CPTEC.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Integracao.CPTEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AirportController : ControllerBase
    {
        private readonly IAirportService _airportService;

        public AirportController(IAirportService airportService)
        {
            _airportService = airportService;
        }

        /// <summary>
        /// Weather forecast for all airports.
        /// </summary>
        /// <remarks>
        /// Get the weather forecast for all airports in Brazil.
        /// </remarks>
        /// <returns></returns>
        /// <response code = "200">Returns the weather forecast for all airports.</response>
        /// <response code = "404">Returns no weather forecast for all airports found.</response>
        /// <response code = "500">Returns that an unexpected error occurred.</response>
        [ProducesResponseType(typeof(SuccessResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [HttpGet("getallairportweatherforecasts")]
        public async Task<IActionResult> GetAllAirportWeatherForecasts()
        {
            var response = await _airportService.GetAllAirportWeatherForecasts();

            if (response.StatusCode == HttpStatusCode.OK) 
                return Ok(response);

            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Weather forecast for an airport.
        /// </summary>
        /// <remarks>
        /// Get the weather forecast for an airport.
        /// </remarks>
        /// <param name="icaoCode">Brazil airport code.</param>
        /// <returns></returns>
        /// <response code = "200">Returns the weather forecast for an airport.</response>
        /// <response code = "400">Returns that user parameters are incorrect.</response>
        /// <response code = "404">Returns no weather forecast for an airport found.</response>
        /// <response code = "422">Returns error in entity validations.</response>
        /// <response code = "500">Returns that an unexpected error occurred.</response>
        [ProducesResponseType(typeof(SuccessResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [ProducesResponseType(typeof(ErrorResponse), 422)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [HttpGet("getweatherforecastairport")]
        public async Task<IActionResult> GetWeatherForecastAirport([FromQuery] [Required] string icaoCode)
        {
            var response = await _airportService.GetAirportWeatherForecast(icaoCode);

            if (response.StatusCode == HttpStatusCode.OK)
                return Ok(response);

            return StatusCode((int)response.StatusCode, response);
        }
    }
}
