using AutoMapper;
using Integracao.CPTEC.Application.Airports.Commands;
using Integracao.CPTEC.Application.Excpetions;
using Integracao.CPTEC.Application.Services.HttpService;
using Integracao.CPTEC.Domain.Entities;
using Integracao.CPTEC.Domain.Interfaces;
using Integracao.CPTEC.Infra.Data.UnitOfWorck;
using MediatR;
using System.Net;

namespace Integracao.CPTEC.Application.Airports.Handlers
{
    public class CreateWeatherForecastByAirportHandler : IRequestHandler<CreateWeatherForecastByAirportCommand, AirportWeatherForecast>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAirportRepository _airportRepository;
        private readonly IAirportApiService _airportApiService;
        private readonly IMapper _mapper;

        public CreateWeatherForecastByAirportHandler(IUnitOfWork unitOfWork,
                                                    IAirportRepository airportRepository,
                                                    IAirportApiService airportApiService, 
                                                    IMapper mapper)

        {
            _unitOfWork = unitOfWork;
            _airportRepository = airportRepository;
            _airportApiService = airportApiService;
            _mapper = mapper;

        }

        public async Task<AirportWeatherForecast> Handle(CreateWeatherForecastByAirportCommand request, CancellationToken cancellationToken)
        {
            var response = await _airportApiService.GetAirportWeatherForecast(request.ICAOCode.ToUpper().Trim());

            if (response.IsSuccessStatusCode && response.Content != null)
            {
                var weatherForecastAirPort = _mapper.Map<AirportWeatherForecast>(response.Content);

                weatherForecastAirPort.ValidadeState();

                await CreateAirPortWeatherForecast(weatherForecastAirPort);

                return weatherForecastAirPort;
            }
            else
                throw new ExternalApiException("Unable to establish a connection with the external service.", response.Content != null ? response.StatusCode : HttpStatusCode.BadGateway);
        }

        public async Task CreateAirPortWeatherForecast(AirportWeatherForecast airportWeatherForecast)
        {
            try
            {      
                _unitOfWork.Begin();

                await _airportRepository.Create(airportWeatherForecast);

                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback(); throw;
            }
        }
    }
}
