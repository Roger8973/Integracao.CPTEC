using AutoMapper;
using Integracao.CPTEC.Application.DTOs;
using Integracao.CPTEC.Domain.Entities;

namespace Integracao.CPTEC.Application.Mappings
{
    public class DomainToDtoProfile : Profile
    {
        public DomainToDtoProfile()
        {
            CreateMap<CityWeatherForecast, CityWeatherForecastDto>();
            CreateMap<AirportWeatherForecast, AirportWeatherForecastDto>();
            CreateMap<Climate, ClimateDto>();             
            CreateMap<City, CityDto>();
        }
    }
}
