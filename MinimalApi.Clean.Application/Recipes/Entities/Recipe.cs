namespace MinimalApi.Clean.Application.Recipes.Entities;

public sealed record Recipe(Guid Id, string Name, string Ingredients, string Steps);