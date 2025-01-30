using MediatR;
using MinimalApi.Clean.Application.Recipes.Entities;
using MinimalApi.Clean.Application.Recipes.Query.GetAllRecipes;

namespace MinimalApi.Clean.Application.Recipes.Query.GetRecipeById;

public class GetRecipeByIdHandler(IRecipesRepository recipesRepo) : IRequestHandler<GetRecipeByIdQuery, Recipe>
{
    public async Task<Recipe> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await recipesRepo.GetRecipeById(request.Id, cancellationToken);
        return result;
    }
}