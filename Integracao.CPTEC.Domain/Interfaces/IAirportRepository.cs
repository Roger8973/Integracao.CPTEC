using Integracao.CPTEC.Domain.Entities;

namespace Integracao.CPTEC.Domain.Interfaces
{
    public interface IAirportRepository
    {
        Task Create(AirportWeatherForecast airportWeatherForecast);
    }
}
