namespace Web.Api.Endpoints;

public static class EndpoinsBase
{
	public static IEndpointRouteBuilder MapAppEndpoints(this IEndpointRouteBuilder builder)
	{
		var group = builder.MapGroup("/api");
		group.MapUserEndpoints();
		group.MapWeatherEndpoints();

		return builder;
	}
}
