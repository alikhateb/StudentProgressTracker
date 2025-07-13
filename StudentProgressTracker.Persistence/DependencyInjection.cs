using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentProgressTracker.Persistence.Common;
using StudentProgressTracker.Persistence.Persistence;

namespace StudentProgressTracker.Persistence;

public static class DependencyInjection
{
    public static void AddPersistence(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddScoped<AuditableInterceptor>();

        service.AddDbContext(configuration);
    }

    private static void AddDbContext(this IServiceCollection service, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnection");

        service.AddDbContext<StudentContext>(
            (serviceProvider, option) =>
            {
                option.UseSqlServer(connectionString: connectionString);
                option.UseStronglyTypeConverters();
                option.AddInterceptors(serviceProvider.GetRequiredService<AuditableInterceptor>());
            }
        );
    }
}
