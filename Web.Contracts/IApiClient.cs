using Web.Contracts.Common;
using Web.Contracts.User;

namespace Web.Contracts;
public interface IApiClient
{
	Task CreateUserAsync(CreateUserCommand user, CancellationToken cancellationToken = default);
	Task<FileDto> DownloadUserAsync(Guid userId, CancellationToken cancellationToken = default);
	Task<GetUsersDto> GetUsersAsync(CancellationToken cancellationToken = default);
}