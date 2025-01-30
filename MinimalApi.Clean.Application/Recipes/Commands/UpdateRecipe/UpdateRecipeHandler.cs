using MediatR;

namespace MinimalApi.Clean.Application.Recipes.Commands.UpdateRecipe;

public sealed class UpdateRecipeCommandHandler(IRecipesRepository recipesRepo) : IRequestHandler<UpdateRecipeCommand, bool>
{
    public async Task<bool> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var result = await recipesRepo.UpdateRecipe(request.Id, request.Name, cancellationToken);

        return result;
    }
}