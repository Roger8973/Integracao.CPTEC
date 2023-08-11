using Integracao.CPTEC.Infra.Data.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Integracao.CPTEC.Infra.IoC
{
    public static class DependecyInjectionJWT
    {
        public static IServiceCollection AddInfrastructureJWT(this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddDbContext<RefreshTokenContext>(options => options.UseInMemoryDatabase("RT"));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<RefreshTokenContext>().AddDefaultTokenProviders();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://refreshtoken.test",
                    ValidAudience = "Integracao.CPTEC.API"
                };
            });

            services.AddAuthorization();

            return services;
        }
    }
}
