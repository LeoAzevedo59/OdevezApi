using Microsoft.Extensions.DependencyInjection;
using Odevez.Business;
using Odevez.Business.Business;
using Odevez.Business.Business.Interfaces;
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
            services.AddScoped<IAutenticarBusiness, AutenticarBusiness>();
            services.AddScoped<IUsuarioBusiness, UsuarioBusiness>();
            services.AddScoped<ICarteiraBusiness, CarteiraBusiness>();

            #endregion

            #region Repository

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IAutenticarRepository, AutenticarRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ICarteiraRepository, CarteiraRepository>();

            #endregion
        }
    }
}
