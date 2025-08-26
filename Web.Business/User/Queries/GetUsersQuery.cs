using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Business.Common.Interfaces;
using Web.Contracts.User;

namespace Web.Business.User.Queries;

public sealed class GetUsersQuery : IRequest<GetUsersDto>
{
	public sealed class Handler(IAppDbContext appDbContext) : IRequestHandler<GetUsersQuery, GetUsersDto>
	{
		public async Task<GetUsersDto> Handle(GetUsersQuery request, CancellationToken cancellationToken)
		{
			var users = await appDbContext.Users.ToListAsync(cancellationToken);

			var result = users.Adapt<List<UserDto>>();

			return new GetUsersDto
			{
				Data = result,
			};
		}
	}
}

