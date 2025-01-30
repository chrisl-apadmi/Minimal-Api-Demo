using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinimalApi.Clean.Application.Recipes;
using MinimalApi.Clean.Infrastructure.Databases.Recipes;

namespace MinimalApi.Clean.Infrastructure;

public static class DependencyExtension
{
    public static WebApplicationBuilder AddDatabases(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddDbContext<RecipeDbContext>(optionsBuilder =>
        {
            optionsBuilder.UseSqlite(builder.Configuration.GetConnectionString("EmployeesDbConnectionString"));
        }, ServiceLifetime.Singleton);

        return builder;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IRecipesRepository, RecipesRepository>();
        return services;
    }
}