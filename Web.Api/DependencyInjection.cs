namespace Web.Api;
public static class DependencyInjection
{
	public static void AddAppApiServices(this IServiceCollection services)
	{
		services.AddCors(options =>
		{
			options.AddPolicy("AppPolicy", policy =>
				policy.AllowAnyOrigin()
					  .AllowAnyHeader()
					  .AllowAnyMethod());
		});

		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();
	}
}