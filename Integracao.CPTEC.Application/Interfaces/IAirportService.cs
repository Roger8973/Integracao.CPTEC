using Integracao.CPTEC.Application.DTOs;

namespace Integracao.CPTEC.Application.Interfaces
{
    public interface IAirportService
    {
        Task<Response> GetAllAirportWeatherForecasts();
        Task<Response> GetAirportWeatherForecast(string icaoCode);
    }
}
