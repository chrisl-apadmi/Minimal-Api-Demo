using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Clean.Api.DI;
using MinimalApi.Clean.Application.Recipes.Commands.AddRecipe;
using MinimalApi.Clean.Application.Recipes.Commands.DeleteRecipe;

namespace MinimalApi.Clean.Api.Endpoints.Recipes.v1;

public sealed class DeleteRecipe : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapDelete("recipes/{id:Guid}", Handler)
            .MapToApiVersion(1)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("Delete Recipe")
            .WithSummary("Delete Recipe")
            .WithDescription("Delete a recipe from the database")
            .WithTags("Recipes");
    }

    private static async Task<NoContent> Handler(
        [FromServices]IMediator mediator,
        [FromRoute]Guid id)
    {
        var command = new DeleteRecipeCommand(id);

        _ = await mediator.Send(command);

        return TypedResults.NoContent();
    }
}