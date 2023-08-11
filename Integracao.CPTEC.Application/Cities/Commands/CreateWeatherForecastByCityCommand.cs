using Integracao.CPTEC.Domain.Entities;
using MediatR;

namespace Integracao.CPTEC.Application.Cities.Commands
{
    public class CreateWeatherForecastByCityCommand : IRequest<CityWeatherForecast>
    {
        public int CityId { get; set; }
    }
}
