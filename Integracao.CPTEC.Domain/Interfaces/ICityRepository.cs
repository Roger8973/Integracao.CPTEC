using Integracao.CPTEC.Domain.Entities;

namespace Integracao.CPTEC.Domain.Interfaces
{
    public interface ICityRepository
    {
        Task<int> GetCityId(string city, string state);
        Task<int> CreateCityWeatherForecast(CityWeatherForecast cityWeatherForecast);
        Task CreateClimate(Climate climate);
    }
}
