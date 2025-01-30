using MediatR;
using MinimalApi.Clean.Application.Recipes.Entities;

namespace MinimalApi.Clean.Application.Recipes.Query.GetRecipeById;

public sealed record GetRecipeByIdQuery(Guid Id) : IRequest<Recipe>;