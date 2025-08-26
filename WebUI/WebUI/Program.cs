using Havit.Blazor.Components.Web;
using WebUI.Client;
using WebUI.Components;

namespace WebUI;

public class Program
{
	public static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		builder.AddServiceDefaults();
		builder.Services.AddAppWebAssemblyServices(builder.Configuration);

		// Add services to the container.
		builder.Services.AddRazorComponents()
			.AddInteractiveWebAssemblyComponents();

		builder.Services.AddHxServices();
		builder.Services.AddHxMessenger();

		var app = builder.Build();

		app.MapDefaultEndpoints();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseWebAssemblyDebugging();
		}
		else
		{
			app.UseExceptionHandler("/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();

		app.UseAntiforgery();

		app.MapStaticAssets();
		app.MapRazorComponents<App>()
			.AddInteractiveWebAssemblyRenderMode()
			.AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

		await app.RunAsync();
	}
}
