using EucProject.Resources;
using Havit.Blazor.Components.Web.Bootstrap;
using Web.Contracts.Common;
using Web.Contracts.User;

namespace WebUI.Client.Components.Selects;

public sealed class GenderSelect : HxSelect<GenderTypeDto?, CodeListDto<GenderTypeDto>>
{
	public GenderSelect()
	{
		ValueSelector = x => x.Key;
		TextSelector = x => x.Text;
		Label = AppRes.Gender;
		Nullable = true;
		NullDataText = AppRes.ChooseGender;
		Data =
		[
			new(GenderTypeDto.Male, AppRes.Male),
			new(GenderTypeDto.Female, AppRes.Female),
			new(GenderTypeDto.Other, AppRes.Others),
		];
	}
}
