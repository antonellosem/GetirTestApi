using System;
using System.Reflection;

using Microsoft.EntityFrameworkCore;

using GetirTestApi.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddEntityFrameworkSqlServer().AddDbContext<OperationsContext>(options =>
            {
                options.UseSqlServer(connectionString,
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(OperationsContext).GetTypeInfo().Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
            }, ServiceLifetime.Transient);

            services.AddScoped<IOperationsRepository, OperationsRepository>();

            return services;
        }
    }
}
