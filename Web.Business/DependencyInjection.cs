using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Web.Business.Common.Behaviours;
using Web.Business.Common.Interfaces;
using Web.Business.Common.Services;
using Web.Contracts.Interfaces;

namespace Web.Business;
public static class DependencyInjection
{
	public static void AddAppBusinessServices(this IServiceCollection services)
	{
		services.AddValidatorsFromAssembly(typeof(IAppValidator).Assembly);

		services.AddMediatR(cfg =>
		{
			cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
			cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
			cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
		});

		services.AddMapster();

		services.AddScoped<IJsonService, JsonService>();
	}
}
