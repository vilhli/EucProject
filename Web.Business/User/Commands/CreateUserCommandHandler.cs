using Mapster;
using MediatR;
using Web.Business.Common.Interfaces;
using Web.Business.User.Models;
using Web.Contracts.User;

namespace Web.Business.User.Commands;

public sealed class CreateUserCommandHandler(IAppDbContext appDbContext) : IRequestHandler<CreateUserCommand>
{
	public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
	{
		UserModel user = request.Adapt<UserModel>();

		appDbContext.Users.Add(user);

		await appDbContext.SaveChangesAsync(cancellationToken);
	}
}

