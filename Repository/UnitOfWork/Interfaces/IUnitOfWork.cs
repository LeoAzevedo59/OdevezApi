using Odevez.Repository.DataConnector;
using Odevez.Repository.Repositorys.Interfaces;

namespace Odevez.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IClientRepository ClientRepository { get; }
        public IUserRepository UserRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IProductRepository ProductRepository { get; }

        public IDbConnector DbConnector { get; set; }


        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
