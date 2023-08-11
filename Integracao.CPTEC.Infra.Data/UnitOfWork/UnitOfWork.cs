using System.Data;

namespace Integracao.CPTEC.Infra.Data.UnitOfWorck
{
    public interface IUnitOfWork : IDisposable
    {
        void Begin();
        void Commit();
        void Rollback();
        IDbTransaction GetTransaction();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _dbConnection;
        private IDbTransaction _transaction;

        public UnitOfWork(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void Begin()
        {
            if (_dbConnection.State == ConnectionState.Closed)
            {
                _dbConnection.Open();
            }
            _transaction = _dbConnection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction?.Commit();
            _transaction = null;
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction = null;
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _transaction = null;
        }

        public IDbTransaction GetTransaction()
        {
          return _transaction;
        }
    }
}
