using EucProject.Resources;
using Havit.Blazor.Components.Web.Bootstrap;
using Web.Contracts.Common;
using Web.Contracts.User;

namespace WebUI.Client.Components.Selects;

public sealed class NationalitySelect : HxSelect<NationalityTypeDto?, CodeListDto<NationalityTypeDto>>
{
	public NationalitySelect()
	{
		ValueSelector = x => x.Key;
		TextSelector = x => x.Text;
		Label = AppRes.Nationality;
		Nullable = true;
		NullDataText = AppRes.ChooseNationality;
		Data =
		[
			new(NationalityTypeDto.Czechia, AppRes.CzechRepublic),
			new(NationalityTypeDto.Slovakia, AppRes.SlovakRepublic),
			new(NationalityTypeDto.Italian, AppRes.Italian),
			new(NationalityTypeDto.American, AppRes.American),
			new(NationalityTypeDto.German, AppRes.German),
		];
	}
}
