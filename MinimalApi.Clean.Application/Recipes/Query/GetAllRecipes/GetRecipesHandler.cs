using MediatR;
using MinimalApi.Clean.Application.Recipes.Entities;

namespace MinimalApi.Clean.Application.Recipes.Query.GetAllRecipes;

public class GetAllRecipesHandler(IRecipesRepository recipesRepo) : IRequestHandler<GetAllRecipesQuery, List<Recipe>>
{
    public async Task<List<Recipe>> Handle(GetAllRecipesQuery request, CancellationToken cancellationToken)
    {
        var result = await recipesRepo.GetRecipes(cancellationToken);
        return result;
    }
}