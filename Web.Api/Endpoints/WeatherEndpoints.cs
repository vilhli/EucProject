namespace Web.Api.Endpoints;

public static class WeatherEndpoints
{
	public static IEndpointRouteBuilder MapWeatherEndpoints(this IEndpointRouteBuilder builder)
	{
		//var group = builder.MapGroup("/weathers").WithTags("Weathers");
		var summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		builder.MapGet("/weatherforecast", (HttpContext httpContext) =>
		{
			var forecast = Enumerable.Range(1, 5).Select(index =>
				new WeatherForecast
				{
					Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
					TemperatureC = Random.Shared.Next(-20, 55),
					Summary = summaries[Random.Shared.Next(summaries.Length)]
				})
				.ToArray();
			return forecast;
		})
		.WithTags("Weathers")
		.WithName("GetWeatherForecast");

		return builder;
	}

	public class WeatherForecast
	{
		public DateOnly Date { get; set; }

		public int TemperatureC { get; set; }

		public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

		public string? Summary { get; set; }
	}
}
