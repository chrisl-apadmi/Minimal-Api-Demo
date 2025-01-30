using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Clean.Api.DI;
using MinimalApi.Clean.Api.Filters.Validation;
using MinimalApi.Clean.Application.Recipes.Commands.UpdateRecipe;

namespace MinimalApi.Clean.Api.Endpoints.Recipes.v1;

public sealed record UpdateRecipeRequest(string Name);

public sealed class UpdateRecipe : IEndpoint
{

    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPut("recipes/{id}", Handler)
            .MapToApiVersion(1)
            .AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory)
            .Accepts<UpdateRecipeRequest>("application/json")
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesValidationProblem()
            .WithName("Update Recipe")
            .WithSummary("Update Recipe")
            .WithDescription("Update a recipe name")
            .WithTags("Recipes");
    }

    private static async Task<Results<Ok, BadRequest>> Handler(
        [FromServices] IMediator mediator,
        [FromRoute] Guid id,
        [Validate][FromBody] UpdateRecipeRequest request)
    {
        var command = new UpdateRecipeCommand(id, request.Name);

        var result = await mediator.Send(command);

        return result ? TypedResults.Ok() : TypedResults.BadRequest();
    }
}

public class UpdateRecipeValidator : AbstractValidator<UpdateRecipeRequest>
{
    public UpdateRecipeValidator()
    {
        RuleFor(r => r.Name).MaximumLength(50);
    }
}