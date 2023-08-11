using Dapper;
using Integracao.CPTEC.Domain.Entities;
using Integracao.CPTEC.Domain.Interfaces;
using Integracao.CPTEC.Infra.Data.UnitOfWorck;
using System.Data;

namespace Integracao.CPTEC.Infra.Data.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbConnection _connection;

        public CityRepository(IUnitOfWork unitOfWork, 
                             IDbConnection connection)
        {
            _unitOfWork = unitOfWork;
            _connection = connection;
        }

        public async Task<int> GetCityId(string city, string state)
        {
            return await _connection.QueryFirstOrDefaultAsync<int>(@"SELECT CityId FROM Cities WHERE City = @City AND State = @State", 
                                                                   new { City = city, State = state }, _unitOfWork.GetTransaction());
        }

        public async Task<int> CreateCityWeatherForecast(CityWeatherForecast cityWeatherForecast)
        {
            return await _connection.QuerySingleAsync<int>(@"INSERT INTO Cities (
                                                                City,
                                                                State,
                                                                Updated)
                                                                OUTPUT INSERTED.CityID
                                                                VALUES(
                                                                @City,
                                                                @State,
                                                                @Updated)", 
                                                                new { cityWeatherForecast.City, cityWeatherForecast.State, cityWeatherForecast.Updated }, _unitOfWork.GetTransaction());
        }

        public async Task CreateClimate(Climate climate)
        {
            await _connection.ExecuteAsync(@"INSERT INTO Climates (
                                                CityId,
                                                Date,
                                                Condition,
                                                ConditionDescription,
                                                MinimumTemperature,
                                                MaximumTemperature,
                                                UvIndex)
                                                VALUES (
                                                @CityId,
                                                @Date,
                                                @Condition,
                                                @ConditionDescription,
                                                @MinimumTemperature,
                                                @MaximumTemperature,
                                                @UvIndex)", climate, _unitOfWork.GetTransaction());
        }
    }
}
