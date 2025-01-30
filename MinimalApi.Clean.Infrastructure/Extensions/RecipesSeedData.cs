using Bogus;
using MinimalApi.Clean.Infrastructure.Databases.Recipes;
using MinimalApi.Clean.Infrastructure.Databases.Recipes.Models;

namespace MinimalApi.Clean.Infrastructure.Extensions;

public static class RecipesSeedData
{

    public static RecipeDbContext Seed(this RecipeDbContext dbContext)
    {
        string[] recipeNames = {
            "Spaghetti Carbonara",
            "Chicken Alfredo",
            "Beef Stroganoff",
            "Shrimp Scampi",
            "Vegetable Stir Fry",
            "Lemon Garlic Salmon",
            "Mushroom Risotto",
            "Classic Cheeseburger",
            "BBQ Pulled Pork",
            "Grilled Cheese Sandwich",
            "Caesar Salad",
            "Clam Chowder",
            "Tandoori Chicken",
            "Chicken Biryani",
            "Pad Thai",
            "Sushi Rolls",
            "Margherita Pizza",
            "French Onion Soup",
            "Chocolate Brownies",
            "Apple Pie"
        };
        var index = 0;

        var recipes = new Faker<Recipe>()
            .RuleFor(x => x.Id, _ => Guid.CreateVersion7())
            .RuleFor(x => x.Name, r =>
            {
                if (index >= 20)
                {
                    return "";
                }
                var recipe = recipeNames[index];
                index++;
                return recipe;
            })
            .RuleFor(x => x.Ingredients, "")
            .RuleFor(x => x.Steps, "")
            .RuleFor(x => x.DateCreated, DateTimeOffset.UtcNow)
            .RuleFor(x => x.DateModified, DateTimeOffset.UtcNow)
            .Generate(20);

        var @default = new Recipe
        {
            Id = new Guid("123e4567-e89b-12d3-a456-426614174000"),
            Name = "Test Recipe",
            Steps = "",
            Ingredients = "",
            DateCreated = DateTimeOffset.UtcNow,
            DateModified = DateTimeOffset.UtcNow
        };

        recipes.Add(@default);
        dbContext.AddRange(recipes);
        dbContext.SaveChangesAsync();

        return dbContext;
    }
}