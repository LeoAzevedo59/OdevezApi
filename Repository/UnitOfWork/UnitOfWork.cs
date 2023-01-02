using Odevez.Repository.DataConnector;
using System.Data;

namespace Odevez.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IDbConnector dbConnector)
        {
            DbConnector = dbConnector;
        }

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
