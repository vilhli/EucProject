using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.Business.Common.Interfaces;
using Web.Infrastracture.Data;

namespace Web.Infrastracture;
public static class DependencyInjection
{
	public static void AddAppInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<AppDbContext>(options =>
			options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

		services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
	}
}
