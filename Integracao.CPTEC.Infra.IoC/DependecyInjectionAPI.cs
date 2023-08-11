using AutoMapper;
using Integracao.CPTEC.Application.Interfaces;
using Integracao.CPTEC.Application.Services;
using Integracao.CPTEC.Application.Services.HttpService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;
using System.Data;
using Integracao.CPTEC.Infra.Data.UnitOfWorck;
using Integracao.CPTEC.Domain.Interfaces;
using Integracao.CPTEC.Infra.Data.Repositories;
using Integracao.CPTEC.Application.Services.LogService;
using Microsoft.IdentityModel.Logging;
using Microsoft.EntityFrameworkCore;
using Integracao.CPTEC.Application.Mappings;

namespace Integracao.CPTEC.Infra.IoC
{
    public static class DependecyInjectionAPI
    {
        public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services,
                                                              IConfiguration configuration)
        {

            services.AddScoped<IDbConnection>(db => new SqlConnection(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICityApiService, CityApiService>();
            services.AddScoped<IAirportService, AirportService>();
            services.AddScoped<IAirportApiService, AirportApiService>();
            services.AddScoped<ILogService, LogService>();

            services.AddScoped<IAirportRepository, AirportRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ILogRepository, LogRepository>();

            services.AddMemoryCache();
            services.AddJwksManager().PersistKeysInMemory().UseJwtValidation();
            IdentityModelEventSource.ShowPII = true;



            services.AddAutoMapper(typeof(DomainToDtoProfile));
            services.AddAutoMapper(typeof(DtoToDomainProfile));

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.Load("Integracao.CPTEC.Application"));
            });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DtoToDomainProfile>();
                cfg.AddProfile<DomainToDtoProfile>();
                cfg.AddProfile<DtoToDtoProfile>();
            });
            config.AssertConfigurationIsValid();


            return services;
        }
    }
}
