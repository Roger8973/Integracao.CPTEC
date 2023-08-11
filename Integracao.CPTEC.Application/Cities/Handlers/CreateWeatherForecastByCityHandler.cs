using AutoMapper;
using Integracao.CPTEC.Application.Cities.Commands;
using Integracao.CPTEC.Application.Excpetions;
using Integracao.CPTEC.Application.Services.HttpService;
using Integracao.CPTEC.Domain.Entities;
using Integracao.CPTEC.Domain.Interfaces;
using Integracao.CPTEC.Infra.Data.UnitOfWorck;
using MediatR;
using System.Net;

namespace Integracao.CPTEC.Application.Cities.Handlers
{
    public class CreateWeatherForecastByCityHandler : IRequestHandler<CreateWeatherForecastByCityCommand, CityWeatherForecast>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICityRepository _cityRepository;
        private readonly ICityApiService _cityApiService;
        private readonly IMapper _mapper;

        public CreateWeatherForecastByCityHandler(IUnitOfWork unitOfWork,
                                                  ICityRepository cityRepository,
                                                  ICityApiService cityApiService,
                                                  IMapper mapper)

        {
            _unitOfWork = unitOfWork;
            _cityRepository = cityRepository;
            _cityApiService = cityApiService;
            _mapper = mapper;
        }

        public async Task<CityWeatherForecast> Handle(CreateWeatherForecastByCityCommand request, CancellationToken cancellationToken)
        {
            var response = await _cityApiService.GetWeatherForecastByCity(request.CityId);

            if (response.IsSuccessStatusCode)
            {
               var cityWeatherForecast = _mapper.Map<CityWeatherForecast>(response.Content);

                await CreateCityWeatherForecast(cityWeatherForecast);

                return cityWeatherForecast;
            }
            else 
                throw new ExternalApiException("Unable to establish a connection with the external service.", response.StatusCode != HttpStatusCode.NotFound ? HttpStatusCode.BadGateway : HttpStatusCode.NotFound);

        }

        public async Task CreateCityWeatherForecast(CityWeatherForecast cityWeatherForecast)
        {
            try
            {
                _unitOfWork.Begin();

                int cityId = await _cityRepository.GetCityId(cityWeatherForecast.City, cityWeatherForecast.State);

                if (cityId == 0)
                {
                    cityId = await _cityRepository.CreateCityWeatherForecast(cityWeatherForecast);

                    await CreateClimate(cityWeatherForecast, cityId);
                }
                else
                {
                    await CreateClimate(cityWeatherForecast, cityId);
                }

                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback(); throw;
            }
        }

        private async Task CreateClimate(CityWeatherForecast cityWeatherForecast, int cityId)
        {
            foreach (var climate in cityWeatherForecast.ListClimates)
            {
                climate.SetCityId(cityId);
                climate.SetDate(DateTime.Now);

                await _cityRepository.CreateClimate(climate);
            }
        }
    }
}
