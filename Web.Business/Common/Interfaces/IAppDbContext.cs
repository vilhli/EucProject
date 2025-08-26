using Microsoft.EntityFrameworkCore;
using Web.Business.User.Models;

namespace Web.Business.Common.Interfaces;
public interface IAppDbContext
{
	DbSet<UserModel> Users { get; }

	Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
