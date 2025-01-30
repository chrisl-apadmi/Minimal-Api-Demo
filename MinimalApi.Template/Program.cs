var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(opt => opt.SwaggerEndpoint("/openapi/v1.json", "swagger demo"));
}

app.UseHttpsRedirection();

var summaries = new List<string>
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Count)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast");

app.MapPost("/weatherforecast", (List<string> newSummaries) =>
    {
        summaries = newSummaries;
        return summaries;
    })
    .WithName("PostWeatherForecast");

app.MapPut("/weatherforecast", (string summary) => { summaries.Add(summary); })
    .WithName("PutWeatherForecast");

app.MapDelete("/weatherforecast", (string summary) =>
    {
        summaries.Remove(summary);
        return summaries;
    })
    .WithName("DeleteWeatherForecast");

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}