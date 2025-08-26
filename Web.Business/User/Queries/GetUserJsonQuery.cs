using Mapster;
using MediatR;
using System.Text;
using Web.Business.Common.Exceptions;
using Web.Business.Common.Interfaces;
using Web.Business.User.Models;
using Web.Contracts.Common;
using Web.Contracts.User;

namespace Web.Business.User.Queries;

public sealed class GetUserJsonQuery(Guid userId) : IRequest<FileDto>
{
	public Guid UserId { get; } = userId;

	public sealed class Handler(
		IJsonService jsonService,
		IAppDbContext appDbContext) : IRequestHandler<GetUserJsonQuery, FileDto>
	{
		public async Task<FileDto> Handle(GetUserJsonQuery request, CancellationToken cancellationToken)
		{
			UserModel? user = await appDbContext.Users.FindAsync(request.UserId, cancellationToken)
				?? throw new NotFoundException(nameof(UserModel));

			var userDto = user.Adapt<UserDto>();

			var jsonUser = jsonService.SerializeObject(userDto);

			return new FileDto
			{
				Content = Encoding.UTF8.GetBytes(jsonUser),
				FileName = $"{user.FirstName}_{user.BirthDate.ToShortDateString()}.json"
			};
		}
	}
}

