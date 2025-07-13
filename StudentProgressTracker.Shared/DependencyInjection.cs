using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using StudentProgressTracker.Shared.CurrentUser;
using StudentProgressTracker.Shared.MediatorPipeLine;

namespace StudentProgressTracker.Shared;

public static class DependencyInjection
{
    public static void AddShared(this IServiceCollection service)
    {
        service.AddHttpContextAccessor();

        service.AddSwaggerGen(option => option.EnableAnnotations());

        service.RegisterServices();

        service.RegisterAddMediator();
    }

    private static void RegisterServices(this IServiceCollection service)
    {
        service.AddScoped<ICurrentUserService, CurrentUserService>();
    }

    private static void RegisterAddMediator(this IServiceCollection service)
    {
        service.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            //cfg.AddOpenRequestPreProcessor(typeof(ValidationProcessor<>));
        });
    }
}
