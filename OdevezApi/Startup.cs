using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Odevez.API.Dependency_Injection;
using Odevez.Business;
using Odevez.Business.GenericMapping;
using Odevez.Business.Interfaces;
using Odevez.Repository.DataConnector;
using Odevez.Repository.Repositorys;
using Odevez.Repository.Repositorys.Interfaces;
using Odevez.Repository.UnitOfWork;

namespace OdevezApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(GenericMapping));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OdevezApi", Version = "v1" });
            });

            string connectionString = Configuration.GetConnectionString("default");
            services.AddScoped<IDbConnector>(db => new SqlConnector(connectionString));

            var dependency = new DependencyInjectionGeneric();
            dependency.Injection(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OdevezApi v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
