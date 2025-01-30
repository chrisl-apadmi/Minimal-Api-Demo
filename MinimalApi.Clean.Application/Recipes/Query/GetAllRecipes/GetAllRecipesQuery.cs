using MediatR;
using MinimalApi.Clean.Application.Recipes.Entities;

namespace MinimalApi.Clean.Application.Recipes.Query.GetAllRecipes;

public sealed record GetAllRecipesQuery() : IRequest<List<Recipe>>;