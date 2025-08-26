using System.Net;
using System.Net.Http.Json;
using Web.Contracts;
using Web.Contracts.Common;
using Web.Contracts.User;

namespace WebUI.Client.Common;
public class ApiClient(HttpClient httpClient) : IApiClient
{
	private static readonly string ApiPrefix = "api/";

	public class ApiException : Exception;

	public async Task<GetUsersDto> GetUsersAsync(CancellationToken cancellationToken = default)
	{
		var users = await httpClient.GetFromJsonAsync<GetUsersDto>(ApiPrefix + "users", cancellationToken) ?? throw new ApiException();

		return users;
	}

	public async Task CreateUserAsync(CreateUserCommand user, CancellationToken cancellationToken = default)
	{
		var response = await httpClient.PostAsJsonAsync(ApiPrefix + "users", user, cancellationToken);

		if (!response.IsSuccessStatusCode)
		{
			Console.WriteLine($"Failed to post user. Status code: {response.StatusCode}");
		}
	}

	public async Task<FileDto> DownloadUserAsync(Guid userId, CancellationToken cancellationToken = default)
	{
		return await httpClient.GetFromJsonAsync<FileDto>(ApiPrefix + $"users/download/{userId}", cancellationToken: cancellationToken) ?? throw new ApiException();
	}
}
