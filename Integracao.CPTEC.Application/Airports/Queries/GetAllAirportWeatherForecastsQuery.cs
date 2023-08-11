using Integracao.CPTEC.Domain.Entities;
using MediatR;

namespace Integracao.CPTEC.Application.Airports.Queries
{
    public class GetAllAirportWeatherForecastsQuery : IRequest<IEnumerable<AirportWeatherForecast>> { }  
}
