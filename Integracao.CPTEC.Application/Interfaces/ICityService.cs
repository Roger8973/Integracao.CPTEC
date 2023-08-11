namespace Integracao.CPTEC.Application.Interfaces
{
    public interface ICityService
    {
        Task<Response> GetCityByNome(string cityName);
        Task<Response> GetWeatherForecastByCity(int cityId); 
    }
}
