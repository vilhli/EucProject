using EucProject.Resources;
using Havit.Blazor.Components.Web;
using Havit.Blazor.Components.Web.Bootstrap;
using Microsoft.JSInterop;
using Web.Contracts;
using Web.Contracts.User;

namespace WebUI.Client.User;
public partial class UserPage(IApiClient apiClient, IJSRuntime jSRuntime, IHxMessengerService messengerService)
{
	private UserFormOffcanvas? userFormOffcanvas;
	private HxGrid<UserDto>? grid;

	private async Task<GridDataProviderResult<UserDto>> GetGridData(GridDataProviderRequest<UserDto> request)
	{
		var response = await apiClient.GetUsersAsync(request.CancellationToken);

		return new GridDataProviderResult<UserDto>()
		{
			Data = response.Data,
			TotalCount = response.Data.Count,
		};
	}

	private async Task RefreshDataAsync()
	{
		if (grid is not null)
		{
			await grid.RefreshDataAsync();
		}
	}

	private async Task HandleShowUserForm()
	{
		if (userFormOffcanvas is not null)
		{
			await userFormOffcanvas.ShowAsync();
		}
	}

	private async Task HandleDownloadUser(Guid userId)
	{
		try
		{
			var result = await apiClient.DownloadUserAsync(userId);
			var base64 = Convert.ToBase64String(result.Content);

			await jSRuntime.InvokeVoidAsync("downloadFile", result.FileName, "application/json", base64);
		}
		catch (Exception ex)
		{
			messengerService.AddError(ex.Message);
			return;
		}
	}

	private static string NationalityDisplayValue(NationalityTypeDto nationalityType)
	{
		return nationalityType switch
		{
			NationalityTypeDto.Italian => AppRes.Italian,
			NationalityTypeDto.Czechia => AppRes.CzechRepublic,
			NationalityTypeDto.Slovakia => AppRes.SlovakRepublic,
			NationalityTypeDto.German => AppRes.German,
			NationalityTypeDto.American => AppRes.American,
			_ => throw new IndexOutOfRangeException(),
		};
	}
}