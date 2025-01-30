using Microsoft.EntityFrameworkCore;
using MinimalApi.Clean.Infrastructure.Databases.Recipes.Models;

namespace MinimalApi.Clean.Infrastructure.Databases.Recipes;

public class RecipeDbContext(DbContextOptions<RecipeDbContext> options) : DbContext(options)
{
    public DbSet<Recipe> Recipes { get; set; }
}