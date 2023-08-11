using Integracao.CPTEC.Domain.Entities;
using Integracao.CPTEC.Domain.Interfaces;
using Dapper;
using System.Data;
using Integracao.CPTEC.Infra.Data.UnitOfWorck;

namespace Integracao.CPTEC.Infra.Data.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbConnection _connection;

        public AirportRepository(IDbConnection connection, 
                                 IUnitOfWork unitOfWork)
        {
            _connection = connection;
            _unitOfWork = unitOfWork;
        }

        public async Task Create(AirportWeatherForecast airportWeatherForecast)
        {
            await _connection.ExecuteAsync(@"INSERT INTO AirporWeatherForecast
                                            (Moisture, 
                                            Visibility, 
                                            IcaoCode, 
                                            AtmosphericPressure, 
                                            Wind, 
                                            WindDirection, 
                                            Condition, 
                                            ConditionDescription, 
                                            Temperature, 
                                            Updated) 
                                            VALUES ( 
                                            @Moisture, 
                                            @Visibility, 
                                            @ICAOCode, 
                                            @AtmosphericPressure,
                                            @Wind,
                                            @WindDirection,
                                            @Condition,
                                            @ConditionDescription,
                                            @Temperature,
                                            @Updated)", airportWeatherForecast, _unitOfWork.GetTransaction());
        }
    }
}
