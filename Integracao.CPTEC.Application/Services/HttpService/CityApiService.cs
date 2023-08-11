using Integracao.CPTEC.Application.DTOs;
using Integracao.CPTEC.Application.Services.HttpService.Interfaces;
using Microsoft.Extensions.Configuration;
using Refit;

namespace Integracao.CPTEC.Application.Services.HttpService
{
    public interface ICityApiService
    {
        Task<ApiResponse<IEnumerable<CityDto>>> GetCityByName(string cityName);
        Task<ApiResponse<CityWeatherForecastDto>> GetWeatherForecastByCity(int cityId);
    }
    public class CityApiService : ICityApiService
    {
        private readonly IConfiguration _configuration;
        private readonly ICityApi _cityApi;

        public CityApiService(IConfiguration configuration)
        {
            _configuration = configuration;
            _cityApi = RestService.For<ICityApi>(_configuration.GetSection("APIs:BrasilApi").Value);
        }

        public async Task<ApiResponse<IEnumerable<CityDto>>> GetCityByName(string cityName)
        => await _cityApi.GetCityByName(cityName);

        public async Task<ApiResponse<CityWeatherForecastDto>> GetWeatherForecastByCity(int cityId)
        => await _cityApi.GetWeatherForecastByCity(cityId);
    }
}
