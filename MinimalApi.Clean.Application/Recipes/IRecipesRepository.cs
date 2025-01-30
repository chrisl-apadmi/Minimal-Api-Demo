using MinimalApi.Clean.Application.Recipes.Entities;

namespace MinimalApi.Clean.Application.Recipes;

public interface IRecipesRepository
{
    Task<Recipe> AddRecipe(string name, string ingredients, string steps, CancellationToken cancellationToken);
    Task<Recipe> GetRecipeById(Guid id, CancellationToken cancellationToken);
    Task<List<Recipe>> GetRecipes(CancellationToken cancellationToken);
    Task<bool> UpdateRecipe(Guid id, string name, CancellationToken cancellationToken);
    Task<bool> DeleteRecipe(Guid id, CancellationToken cancellationToken);
}