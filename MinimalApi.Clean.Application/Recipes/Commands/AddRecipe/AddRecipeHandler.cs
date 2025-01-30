using MediatR;

namespace MinimalApi.Clean.Application.Recipes.Commands.AddRecipe;

public sealed class AddRecipeCommandHandler(IRecipesRepository RecipesRepo) : IRequestHandler<AddRecipeCommand, Guid>
{
    public async Task<Guid> Handle(AddRecipeCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var result = await RecipesRepo.AddRecipe(request.Name, request.Ingredients, request.Steps, cancellationToken);

        return result.Id;
    }
}