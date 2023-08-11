using Dapper;
using Integracao.CPTEC.Domain.Entities;
using Integracao.CPTEC.Domain.Interfaces;
using Integracao.CPTEC.Infra.Data.UnitOfWorck;
using System.Data;

namespace Integracao.CPTEC.Infra.Data.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbConnection _connection;

        public LogRepository(IUnitOfWork unitOfWork, 
                             IDbConnection connection)
        {
            _unitOfWork = unitOfWork;
            _connection = connection;
        }

        public async Task SaveLog(LogErro logErro)
        {
            await _connection.ExecuteAsync(@"INSERT INTO LogErro (
                                             Description,
                                             StackTrace,
                                             DateHour)
                                             Values (
                                             @Description,
                                             @StackTrace,
                                             @DateHour)", logErro, _unitOfWork.GetTransaction());

            _unitOfWork.Commit();   
        }
    }
}
