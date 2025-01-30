using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace MinimalApi.Clean.Api.DI;

public static class DependencyExtension
{
    public static IServiceCollection AddAPIServiceDependencies(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Singleton);
        return services;
    }
}