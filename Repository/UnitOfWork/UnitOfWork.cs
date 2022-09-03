using Odevez.Repository.DataConnector;
using Odevez.Repository.Repositorys;
using Odevez.Repository.Repositorys.Interfaces;
using System.Data;

namespace Odevez.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IClientRepository _clientRepository;

        public IClientRepository ClientRepository => _clientRepository ?? (_clientRepository = new ClientRepository(DbConnector));

        public IUserRepository UserRepository => throw new System.NotImplementedException();

        public IOrderRepository OrderRepository => throw new System.NotImplementedException();

        public IProductRepository ProductRepository => throw new System.NotImplementedException();

        public IDbConnector DbConnector { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

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
