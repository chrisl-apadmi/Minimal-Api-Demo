using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Clean.Application.Common.Entity;
using MinimalApi.Clean.Application.Common.Exception;
using MinimalApi.Clean.Application.Recipes;
using MinimalApi.Clean.Infrastructure.Databases.Recipes.Models;
using MinimalApi.Clean.Infrastructure.Extensions;
using ApplicationRecipe = MinimalApi.Clean.Application.Recipes.Entities.Recipe;

namespace MinimalApi.Clean.Infrastructure.Databases.Recipes;

public class RecipesRepository : IRecipesRepository
{
    private readonly RecipeDbContext _recipeDbContext;

    public RecipesRepository(RecipeDbContext recipeDbContext)
    {
        _recipeDbContext = recipeDbContext;
        _recipeDbContext.Database.EnsureDeleted();
        _recipeDbContext.Database.EnsureCreated();
        _recipeDbContext.Seed();
    }

    public virtual async Task<ApplicationRecipe> AddRecipe(string name, string ingredients, string steps, CancellationToken cancellationToken)
    {
        var recipe = new Recipe
        {
            Id = Guid.CreateVersion7(),
            Name = name,
            Ingredients = ingredients,
            Steps = steps,
            DateCreated = DateTimeOffset.UtcNow,
            DateModified = DateTimeOffset.UtcNow
        };

        await _recipeDbContext.Recipes.AddAsync(recipe, cancellationToken);
        await _recipeDbContext.SaveChangesAsync(cancellationToken);

        var result = await _recipeDbContext.Recipes
            .Where(x => x.Id == recipe.Id)
            .AsNoTracking()
            .FirstAsync(cancellationToken);

        return new ApplicationRecipe(
            Id: result.Id,
            Name: result.Name,
            Ingredients: result.Ingredients,
            Steps: result.Steps);
    }

    public async Task<ApplicationRecipe> GetRecipeById(Guid id, CancellationToken cancellationToken)
    {
        var recipe = await _recipeDbContext.Recipes
            .Where(x => x.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        NotFoundException.ThrowIfNull(recipe, EntityType.Recipe);

        var result = new ApplicationRecipe(
            Id: recipe!.Id,
            Name: recipe.Name,
            Ingredients: string.Empty,
            Steps:string.Empty
            );

        return result;
    }

    public async Task<List<ApplicationRecipe>> GetRecipes(CancellationToken cancellationToken)
    {
        var recipes = await _recipeDbContext.Recipes
            .Take(10)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var result = new List<ApplicationRecipe>();

        foreach (var recipe in recipes)
        {
            result.Add(new ApplicationRecipe(
                Id: recipe.Id,
                Name: recipe.Name,
                Ingredients: recipe.Ingredients,
                Steps: recipe.Steps));
        }

        return result;
    }

    public async Task<bool> UpdateRecipe(Guid id, string name, CancellationToken cancellationToken)
    {
        try
        {
            var recipe = await _recipeDbContext.Recipes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            NotFoundException.ThrowIfNull(recipe, EntityType.Recipe);

            recipe!.Name = name;
            recipe.DateModified = DateTimeOffset.UtcNow;

            _recipeDbContext.Recipes.Update(recipe);
            await _recipeDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (NotFoundException)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> DeleteRecipe(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var recipe = await _recipeDbContext.Recipes.SingleAsync(x => x.Id == id, cancellationToken);
            _recipeDbContext.Recipes.Remove(recipe);
            await _recipeDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }
}