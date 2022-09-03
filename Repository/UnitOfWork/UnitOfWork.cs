using Odevez.Repository.DataConnector;
using Odevez.Repository.Repositorys;
using Odevez.Repository.Repositorys.Interfaces;
using System.Data;

namespace Odevez.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IClientRepository _clientRepository;

        public UnitOfWork(IDbConnector dbConnector)
        {
            DbConnector = dbConnector;
        }

        public IClientRepository ClientRepository => _clientRepository ?? (_clientRepository = new ClientRepository(DbConnector));
        public IDbConnector DbConnector { get; }

        public void BeginTransaction()
        {
            if (DbConnector.dbConnection.State == ConnectionState.Open)
                DbConnector.dbTransaction = DbConnector.dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void CommitTransaction()
        {
            if (DbConnector.dbConnection.State == ConnectionState.Open)
                DbConnector.dbTransaction.Commit();
        }

        public void RollbackTransaction()
        {
            if (DbConnector.dbConnection.State == ConnectionState.Open)
                DbConnector.dbTransaction.Rollback();
        }
    }
}
