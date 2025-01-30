using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Clean.Api.DI;
using MinimalApi.Clean.Application.Recipes.Commands.AddRecipe;

namespace MinimalApi.Clean.Api.Endpoints.Recipes.v1;

public sealed class AddRecipe : IEndpoint
{
    private sealed record AddRecipeModel(string Name, string Ingredients, string Steps);
    private sealed record AddRecipeRequest(AddRecipeModel Recipe);

    private sealed record AddRecipeResponse(Guid Id);

    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost("recipes", Handler)
            .MapToApiVersion(1)
            .Accepts<AddRecipeRequest>("application/json")
            .Produces<AddRecipeResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("Add Recipe")
            .WithSummary("Add Recipe")
            .WithDescription("Adds a recipe to the system")
            .WithTags("Recipes");
    }

    private static async Task<Results<Ok<AddRecipeResponse>, BadRequest>> Handler(
        [FromServices]IMediator mediator,
        [FromBody]AddRecipeRequest request)
    {
        var recipe = request.Recipe;

        if (string.IsNullOrEmpty(recipe.Steps))
        {
            return TypedResults.BadRequest();
        }

        var command = new AddRecipeCommand(recipe.Name, recipe.Ingredients, recipe.Steps);
        var result = await mediator.Send(command);
        var response = new AddRecipeResponse(result);

        return TypedResults.Ok(response);
    }
}