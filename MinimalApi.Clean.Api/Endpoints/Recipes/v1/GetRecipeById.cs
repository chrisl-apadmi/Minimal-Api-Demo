using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Clean.Api.DI;
using MinimalApi.Clean.Application.Common.Exception;
using MinimalApi.Clean.Application.Recipes.Query.GetRecipeById;

namespace MinimalApi.Clean.Api.Endpoints.Recipes.v1.GetRecipeById;

public class GetRecipeById : IEndpoint
{
    private sealed record Recipe(Guid Id, string Name);
    private sealed record GetRecipeByIdResponse(Recipe Recipes);

    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("recipes/{id:guid}", Handler)
            .MapToApiVersion(1)
            .HasDeprecatedApiVersion(1)
            .WithName("Get recipes by id v1")
            .WithSummary("Get recipe given an id")
            .WithDescription("Gets one recipe")
            .WithTags("Recipes");
    }

    private static async Task<Results<Ok<GetRecipeByIdResponse>, NotFound, ProblemHttpResult>> Handler(
        [FromServices]IMediator mediator,
        [FromRoute] Guid id)
    {
        try
        {
            var result = await mediator.Send(new GetRecipeByIdQuery(id));
            var response = new GetRecipeByIdResponse(new Recipe(result.Id, result.Name));
            return TypedResults.Ok(response);
        }
        catch (NotFoundException e)
        {
            return TypedResults.NotFound();
        }
        catch (Exception ex)
        {
            return TypedResults.Problem(
                ex.StackTrace,
                ex.Message,
                StatusCodes.Status500InternalServerError);
        }


    }
}