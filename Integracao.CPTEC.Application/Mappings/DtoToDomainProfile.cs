using AutoMapper;
using Integracao.CPTEC.Application.DTOs;
using Integracao.CPTEC.Domain.Entities;

namespace Integracao.CPTEC.Application.Mappings
{
    public class DtoToDomainProfile : Profile
    {
        public DtoToDomainProfile()
        {
            CreateMap<CityDto, City>();

            CreateMap<AirportWeatherForecastDto, AirportWeatherForecast>()
                 .ForMember(dest => dest.Errors, opt => opt.Ignore());

            CreateMap<CityWeatherForecastDto, CityWeatherForecast>()
                 .ForMember(dest => dest.Errors, opt => opt.Ignore());

            CreateMap<ClimateDto, Climate>()
                .ForMember(dest => dest.CityId, opt => opt.Ignore())
                 .ForMember(dest => dest.Errors, opt => opt.Ignore())
                 .ConstructUsing(src => new Climate(DateTime.Parse(src.Date), src.Condition, src.ConditionDescription, src.MinimumTemperature, src.MaximumTemperature, src.UvIndex));
        }
    }
}
