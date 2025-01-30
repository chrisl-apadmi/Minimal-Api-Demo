using MediatR;

namespace MinimalApi.Clean.Application.Recipes.Commands.DeleteRecipe;

public sealed record DeleteRecipeCommand(Guid Id) : IRequest<bool>;