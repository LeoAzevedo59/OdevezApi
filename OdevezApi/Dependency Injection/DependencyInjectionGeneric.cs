using Microsoft.Extensions.DependencyInjection;
using Odevez.Business;
using Odevez.Business.Interfaces;
using Odevez.Repository.Repositorys;
using Odevez.Repository.Repositorys.Interfaces;
using Odevez.Repository.UnitOfWork;

namespace Odevez.API.Dependency_Injection
{
    public class DependencyInjectionGeneric
    {
        public void Injection(IServiceCollection services)
        {
            #region UoW

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion

            #region Business

            services.AddScoped<IClientBusiness, ClientBusiness>();

            #endregion

            #region Repository

            services.AddScoped<IClientRepository, ClientRepository>();

            #endregion
        }
    }
}
