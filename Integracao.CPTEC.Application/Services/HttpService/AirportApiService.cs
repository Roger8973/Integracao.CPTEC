using Integracao.CPTEC.Application.DTOs;
using Integracao.CPTEC.Application.Services.HttpService.Interfaces;
using Microsoft.Extensions.Configuration;
using Refit;

namespace Integracao.CPTEC.Application.Services.HttpService
{
    public interface IAirportApiService
    {
        Task<ApiResponse<IEnumerable<GetAllWeatherForecastAirportDto>>> GetAllAirportWeatherForecasts();
        Task<ApiResponse<AirportWeatherForecastDto>> GetAirportWeatherForecast(string icaoCode);
    }
    public class AirportApiService : IAirportApiService
    {
        private readonly IConfiguration _configuration;
        private readonly IAirportApi _airportApi;

        public AirportApiService(IConfiguration configuration)
        {
            _configuration = configuration;
            _airportApi = RestService.For<IAirportApi>(_configuration.GetSection("APIs:BrasilApi").Value);
        }

        public async Task<ApiResponse<IEnumerable<GetAllWeatherForecastAirportDto>>> GetAllAirportWeatherForecasts()
        => await _airportApi.GetAllAirportWeatherForecasts();

        public async Task<ApiResponse<AirportWeatherForecastDto>> GetAirportWeatherForecast(string icaoCode)
        => await _airportApi.GetWeatherForecastAirport(icaoCode);
    }
}
