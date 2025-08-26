using FluentValidation;
using Havit.Blazor.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Web.Contracts.Interfaces;

namespace WebUI.Client;

class Program
{
	static async Task Main(string[] args)
	{
		WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

		builder.Services.AddAppWebAssemblyServices(builder.Configuration);

		builder.Services.AddHxServices();
		builder.Services.AddHxMessenger();

		builder.Services.AddValidatorsFromAssembly(typeof(IAppValidator).Assembly);

		builder.Services.AddLocalization();

		var app = builder.Build();

		await app.UseCustomLocalization();

		await app.RunAsync();
	}
}
