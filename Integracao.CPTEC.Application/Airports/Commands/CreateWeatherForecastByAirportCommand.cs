using Integracao.CPTEC.Domain.Entities;
using MediatR;

namespace Integracao.CPTEC.Application.Airports.Commands
{
    public class CreateWeatherForecastByAirportCommand : IRequest<AirportWeatherForecast>
    {
        public CreateWeatherForecastByAirportCommand(string iCAOCode)
        {
            ICAOCode = iCAOCode;
        }

        public string ICAOCode { get; set; }
    }
}
