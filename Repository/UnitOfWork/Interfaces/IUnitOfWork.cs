using Odevez.Repository.DataConnector;
using Odevez.Repository.Repositorys.Interfaces;

namespace Odevez.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IClientRepository ClientRepository { get; }
        public IDbConnector DbConnector { get; }

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
