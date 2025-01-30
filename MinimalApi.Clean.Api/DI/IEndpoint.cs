using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Routing;

namespace MinimalApi.Clean.Api.DI;

internal interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder routeBuilder);
}