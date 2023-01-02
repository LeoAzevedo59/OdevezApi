using Odevez.Repository.DataConnector;
using Odevez.Repository.Repositorys.Interfaces;

namespace Odevez.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IDbConnector DbConnector { get; }

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
