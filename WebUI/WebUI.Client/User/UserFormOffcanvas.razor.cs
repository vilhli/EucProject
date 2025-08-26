using Havit.Blazor.Components.Web;
using Havit.Blazor.Components.Web.Bootstrap;
using Microsoft.AspNetCore.Components;
using Web.Contracts;
using Web.Contracts.User;

namespace WebUI.Client.User;
public partial class UserFormOffcanvas(IApiClient apiClient, IHxMessengerService messengerService)
{
	[Parameter]
	public EventCallback OnFormValidSubmit { get; set; }

	private readonly string formId = "px" + Guid.NewGuid().ToString("N");
	private HxOffcanvas? offcanvas;

	private CreateUserCommand model = new();

	public async Task ShowAsync()
	{
		if (offcanvas is not null)
		{
			await offcanvas.ShowAsync();
		}
	}
	private void GetBirthDateFromIdNumber()
	{
		if (model.IdentificationNumber?.Length > 6)
		{
			string idStr = model.IdentificationNumber;

			try
			{
				int year = int.Parse(idStr.Substring(0, 2));
				int month = int.Parse(idStr.Substring(2, 2));
				int day = int.Parse(idStr.Substring(4, 2));

				if (month > 50) month -= 50; // Female adjustment

				// Determine century
				int fullYear = (year >= 54) ? 1900 + year : 2000 + year;

				model.BirthDate = new DateTime(fullYear, month, day);
			}
			catch
			{
				model.BirthDate = null;
			}
		}
		else
		{
			model.BirthDate = null;
		}
	}

	private async Task HandleFormSubmit()
	{
		if (offcanvas is null)
		{
			return;
		}

		try
		{
			await apiClient.CreateUserAsync(model);
			await OnFormValidSubmit.InvokeAsync();
			await offcanvas.HideAsync();
		}
		catch (Exception ex)
		{
			messengerService.AddError(ex.Message);
		}
	}

	private void ResetForm()
	{
		model = new();
	}
}