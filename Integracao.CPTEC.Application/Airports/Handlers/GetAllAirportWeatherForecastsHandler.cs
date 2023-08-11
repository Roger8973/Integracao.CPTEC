using AutoMapper;
using Integracao.CPTEC.Application.Airports.Queries;
using Integracao.CPTEC.Application.DTOs;
using Integracao.CPTEC.Application.Excpetions;
using Integracao.CPTEC.Application.Services.HttpService;
using Integracao.CPTEC.Domain.Entities;
using MediatR;

namespace Integracao.CPTEC.Application.Airports.Handlers
{
    public class GetAllAirportWeatherForecastsHandler : IRequestHandler<GetAllAirportWeatherForecastsQuery, IEnumerable<AirportWeatherForecast>>
    {
        private readonly IAirportApiService _airportApiService;
        private readonly IMapper _mapper;

        public GetAllAirportWeatherForecastsHandler(IAirportApiService airportApiService,
                                                    IMapper mapper)
        {
            _airportApiService = airportApiService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AirportWeatherForecast>> Handle(GetAllAirportWeatherForecastsQuery request, CancellationToken cancellationToken)
        {
            var response = await _airportApiService.GetAllAirportWeatherForecasts();

            return response.IsSuccessStatusCode ?
                   _mapper.Map<IEnumerable<AirportWeatherForecast>>(_mapper.Map<IEnumerable<AirportWeatherForecastDto>>(response.Content))
                   : throw new ExternalApiException("Unable to establish a connection with the external service.", response.StatusCode);
        }
    }
}
