using MediatR;

namespace MinimalApi.Clean.Application.Recipes.Commands.DeleteRecipe;

public sealed class DeleteRecipeCommandHandler(IRecipesRepository RecipesRepo) : IRequestHandler<DeleteRecipeCommand, bool>
{
    public async Task<bool> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var result = await RecipesRepo.DeleteRecipe(request.Id, cancellationToken);

        return result;
    }
}