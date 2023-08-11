using Integracao.CPTEC.Application;
using Integracao.CPTEC.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Integracao.CPTEC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        /// <summary>
        /// List of cities in Brazil.
        /// </summary>
        /// <remarks>
        /// Get city data by name.
        /// </remarks>
        /// <param name="cityName">Name of a city in Brazil</param>
        /// <returns></returns>
        /// <response code = "200">Returns data for a city in Brazil.</response>
        /// <response code = "400">Returns that user parameters are incorrect.</response>
        /// <response code = "404">Returns no city found</response>
        /// <response code = "500">Returns that an unexpected error occurred.</response>
        [ProducesResponseType(typeof(SuccessResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [HttpGet("getcitybynome")]
        public async Task<IActionResult> GetCityByNome([FromQuery] string cityName)
        {
            var response = await _cityService.GetCityByNome(cityName);

            if (response.StatusCode == HttpStatusCode.OK)
                return Ok(response);

            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Weather forecast for a city.
        /// </summary>
        /// <remarks>
        /// Get the weather forecast for a city.
        /// </remarks>
        /// <param name="cityId">City code.</param>
        /// <returns></returns>
        /// <response code = "200">Returns the weather forecast for a city in Brazil</response>
        /// <response code = "400">Returns that user parameters are incorrect.</response>
        /// <response code = "404">Returns no weather forecast for an city found.</response>
        /// <response code = "422">Returns error in entity validations.</response>
        /// <response code = "500">Returns that an unexpected error occurred.</response>
        [ProducesResponseType(typeof(SuccessResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [ProducesResponseType(typeof(ErrorResponse), 422)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [HttpGet("getweatherforecastbycity")]
        public async Task<IActionResult> GetWeatherForecastByCity([FromQuery] int cityId)
        {
            var response = await _cityService.GetWeatherForecastByCity(cityId);

            if (response.StatusCode == HttpStatusCode.OK)
                return Ok(response);

            return StatusCode((int)response.StatusCode, response);
        }
    }
}
