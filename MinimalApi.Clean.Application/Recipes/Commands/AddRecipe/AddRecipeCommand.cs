using MediatR;

namespace MinimalApi.Clean.Application.Recipes.Commands.AddRecipe;

public sealed record AddRecipeCommand(string Name, string Ingredients, string Steps) : IRequest<Guid>;