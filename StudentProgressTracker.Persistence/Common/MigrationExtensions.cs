using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentProgressTracker.Persistence.Persistence;

namespace StudentProgressTracker.Persistence.Common;

public static class MigrationExtensions
{
    public static void ApplyMigration(this IApplicationBuilder applicationBuilder)
    {
        using var scop = applicationBuilder.ApplicationServices.CreateScope();
        var context = scop.ServiceProvider.GetRequiredService<StudentContext>();
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }
}
