using Integracao.CPTEC.Application.DTOs;
using Refit;

namespace Integracao.CPTEC.Application.Services.HttpService.Interfaces
{
    public interface IAirportApi
    {
        [Get("/clima/aeroporto/{icaoCode}")]
        Task<ApiResponse<AirportWeatherForecastDto>> GetWeatherForecastAirport(string icaoCode);

        [Get("/clima/capital")]
        Task<ApiResponse<IEnumerable<GetAllWeatherForecastAirportDto>>> GetAllAirportWeatherForecasts();
    }
}
