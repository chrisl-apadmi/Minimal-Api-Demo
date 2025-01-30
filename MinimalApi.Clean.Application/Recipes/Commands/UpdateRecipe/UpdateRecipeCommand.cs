using MediatR;
using MinimalApi.Clean.Application.Recipes.Entities;

namespace MinimalApi.Clean.Application.Recipes.Commands.UpdateRecipe;

public sealed record UpdateRecipeCommand(Guid Id, string Name) : IRequest<bool>;