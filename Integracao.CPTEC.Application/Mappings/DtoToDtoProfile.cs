using AutoMapper;
using Integracao.CPTEC.Application.DTOs;

namespace Integracao.CPTEC.Application.Mappings
{
    public class DtoToDtoProfile : Profile
    {
        public DtoToDtoProfile()
        {
            CreateMap<GetAllWeatherForecastAirportDto, AirportWeatherForecastDto>();
        }
    }
}
