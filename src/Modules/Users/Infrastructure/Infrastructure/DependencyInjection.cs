using Application.Interfaces;
using DataAccess.Context;
using DataAccess.Repositories;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUsersModule(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING"); 
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("DB_CONNECTION_STRING environment variable is not set.");
            }

            services.AddDbContext<SaphyreContext>(options =>
                options.UseNpgsql(connectionString, npgsqlOptions =>
                {
                    npgsqlOptions.CommandTimeout(15);
                    npgsqlOptions.EnableRetryOnFailure();
                })
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()                
            );

            return services;
        }
    }
}
