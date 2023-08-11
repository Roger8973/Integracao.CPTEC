using Integracao.CPTEC.Domain.Entities;

namespace Integracao.CPTEC.Domain.Interfaces
{
    public interface ILogRepository
    {
        Task SaveLog(LogErro logErro);
    }
}
