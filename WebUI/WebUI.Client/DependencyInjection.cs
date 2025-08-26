using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Globalization;
using Web.Contracts;
using WebUI.Client.Common;

namespace WebUI.Client;

public static class DependencyInjection
{
	public static void AddAppWebAssemblyServices(this IServiceCollection services, IConfiguration configuration)
	{
		string apiUrl = configuration["Infrastructure:ApiUrl"] ?? throw new ArgumentNullException("Infrastructure:ApiUrl");

		services.AddHttpClient<IApiClient, ApiClient>(client =>
		{
			client.BaseAddress = new(apiUrl);
		});

		services.AddBlazoredLocalStorage();
	}

	public static async Task UseCustomLocalization(this WebAssemblyHost app)
	{
		const string defaultCulture = "en-US";
		var localeStorage = app.Services.GetRequiredService<ILocalStorageService>();
		var result = await localeStorage.GetItemAsStringAsync(LocalStorageKeys.CultureKey);
		var culture = CultureInfo.GetCultureInfo(result ?? defaultCulture);

		if (result is null)
		{
			await localeStorage.SetItemAsStringAsync(LocalStorageKeys.CultureKey, defaultCulture);
		}

		CultureInfo.DefaultThreadCurrentCulture = culture;
		CultureInfo.DefaultThreadCurrentUICulture = culture;
	}
}
