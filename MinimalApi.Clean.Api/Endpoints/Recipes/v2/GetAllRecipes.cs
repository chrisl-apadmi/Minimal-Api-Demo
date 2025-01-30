﻿using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Clean.Api.DI;
using MinimalApi.Clean.Application.Recipes.Query.GetAllRecipes;

namespace MinimalApi.Clean.Api.Endpoints.Recipes.v2;

public class GetAllRecipes : IEndpoint
{
    private sealed record Recipe(Guid Id, string Name, string Ingredients);
    private sealed record GetAllRecipesResponse(List<Recipe> Recipes);

    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("recipes", Handler)
            .MapToApiVersion(2)
            .WithName("Get all recipes v2")
            .WithSummary("Get all recipes")
            .WithDescription("Adds a recipe to the collection")
            .WithTags("Recipes");
    }

    private static async Task<Ok<GetAllRecipesResponse>> Handler(
        [FromServices]IMediator mediator)
    {
        var result = await mediator.Send(new GetAllRecipesQuery());

        var mappedResult = new List<Recipe>();
        foreach (var recipe in result)
        {
            mappedResult.Add(new Recipe(recipe.Id, recipe.Name, recipe.Ingredients));
        }
        var response = new GetAllRecipesResponse(mappedResult);

        return TypedResults.Ok(response);
    }
}