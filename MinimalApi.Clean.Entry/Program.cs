using Asp.Versioning;
using Asp.Versioning.Conventions;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Clean.Api.DI;
using MinimalApi.Clean.Application;
using MinimalApi.Clean.Infrastructure;
using MinimalApi.Clean.Infrastructure.Databases.Recipes;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi("v1");
builder.Services.AddOpenApi("v2");

builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1);
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = new UrlSegmentApiVersionReader();
    opt.UnsupportedApiVersionStatusCode = 404;
}).AddApiExplorer(opt =>
{
    opt.GroupNameFormat = "'v'V";
    opt.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEndpoints();

builder.Services.AddProblemDetails();
builder.Services.AddApiVersioning();
builder.Services.AddRouting();

builder.Services.AddApplicationDependencies();
builder.Services.AddAPIServiceDependencies();
builder.Services.AddRepositories();
builder.AddDatabases();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

var versions = app.NewApiVersionSet()
    .HasApiVersion(1)
    .HasApiVersion(2)
    .ReportApiVersions()
    .Build();

var routeGroupBuilder = app.MapGroup("v{version:apiVersion}/api")
    .WithApiVersionSet(versions);

app.MapEndpoints(routeGroupBuilder);

await app.RunAsync();