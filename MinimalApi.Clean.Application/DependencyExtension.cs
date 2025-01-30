using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MinimalApi.Clean.Application.Recipes;

namespace MinimalApi.Clean.Application;

public static class DependencyExtension
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }
}