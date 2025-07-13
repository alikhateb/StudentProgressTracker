using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using StudentProgressTracker.Core.Application.Courses;
using StudentProgressTracker.Core.Application.Students;
using StudentProgressTracker.Domain.Repositories;

namespace StudentProgressTracker.Core;

public static class DependencyInjection
{
    public static void AddCore(this IServiceCollection service)
    {
        service.RegisterServices();

        service.RegisterAddMediator();

        service.RegisterFluentValidator();
    }

    private static void RegisterServices(this IServiceCollection service)
    {
        service.AddScoped<ICoursesRepository, CoursesRepository>();
        service.AddScoped<IStudentsRepository, StudentsRepository>();
    }

    private static void RegisterAddMediator(this IServiceCollection service)
    {
        service.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
    }

    private static void RegisterFluentValidator(this IServiceCollection service)
    {
        service.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
