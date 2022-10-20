using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Odevez.API.Dependency_Injection;
using Odevez.Business.Business;
using Odevez.Business.GenericMapping;
using Odevez.Repository.DataConnector;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Linq;
using System.Text;

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

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Description = "Beared token",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.OperationFilter<SecurityRequirementsOperationFilter>();

                var xmlApi = Path.Combine(AppContext.BaseDirectory, $"{ typeof(Startup).Assembly}");
            });

            var authSettingsSection = Configuration.GetSection("AuthSettings");
            services.Configure<AuthSettings>(authSettingsSection);

            var authSettings = authSettingsSection.Get<AuthSettings>();
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.Secret));

            services.AddAuthentication(x =>
           {
               x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           })
                .AddPolicyScheme("a3xZF9D8SM7Pxy8DTqY2a84M9aLKU3UG", "Authorization Bearrer or AcessToken", options =>
                {
                    options.ForwardDefaultSelector = context =>
                    {
                        if (context.Request.Headers["Acess-Token"].Any())
                            return "Acess-Token";

                        return JwtBearerDefaults.AuthenticationScheme;
                    };
                }).AddJwtBearer(x =>
               {
                   x.TokenValidationParameters = new TokenValidationParameters()
                   {
                       ValidateIssuer = true,
                       ValidIssuer = "a3xZF9D8SM7Pxy8DTqY2a84M9aLKU3UG",
                       ValidateAudience = false,
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = key,

                       //Validação se o token é válido
                       ValidateLifetime = true,
                       RequireExpirationTime = true,
                   };
               });

            string connectionString = Configuration.GetConnectionString("default");
            services.AddScoped<IDbConnector>(db => new SqlConnector(connectionString));

            var dependency = new DependencyInjectionGeneric();
            dependency.Injection(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OdevezApi v1"));

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
