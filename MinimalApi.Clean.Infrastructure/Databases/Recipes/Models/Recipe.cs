using System.ComponentModel.DataAnnotations;

namespace MinimalApi.Clean.Infrastructure.Databases.Recipes.Models;

public sealed class Recipe
{
    [Key]
    public required Guid Id { get; init; }
    [MaxLength(100)]
    public required string Name { get; set; }
    [MaxLength(500)]
    public required string Ingredients { get; set; }
    [MaxLength(500)]
    public required string Steps { get; set; }
    public required DateTimeOffset DateCreated { get; init; }
    public required DateTimeOffset DateModified { get; set; }
}