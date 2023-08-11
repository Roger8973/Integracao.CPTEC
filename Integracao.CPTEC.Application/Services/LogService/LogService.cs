using Integracao.CPTEC.Domain.Entities;
using Integracao.CPTEC.Domain.Interfaces;

namespace Integracao.CPTEC.Application.Services.LogService
{
    public interface ILogService
    {
        Task SaveLog(Exception exception);
    }
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task SaveLog(Exception exception)
        {
            await _logRepository.SaveLog(new LogErro
            {
                Description = exception.Message,
                StackTrace = exception.StackTrace,
                DateHour = DateTime.Now
            });
        }
    }
}
