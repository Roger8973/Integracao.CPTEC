using Integracao.CPTEC.Application.DTOs;
using Refit;

namespace Integracao.CPTEC.Application.Services.HttpService.Interfaces
{
    public interface ICityApi
    {
        [Get("/cidade/{cityName}")]
        Task<ApiResponse<IEnumerable<CityDto>>> GetCityByName(string cityName);

        [Get("/clima/previsao/{cityId}")]
        Task<ApiResponse<CityWeatherForecastDto>> GetWeatherForecastByCity(int cityId);
    }
}
