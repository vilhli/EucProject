using Microsoft.EntityFrameworkCore;
using Web.Business.Common.Interfaces;
using Web.Business.User.Models;

namespace Web.Infrastracture.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
	public DbSet<UserModel> Users => Set<UserModel>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<UserModel>(entity =>
		{
			entity.ToTable("User");

			// PK with DEFAULT NEWSEQUENTIALID()
			entity.HasKey(e => e.UserId);
			entity.Property(e => e.UserId)
				  .HasColumnType("uniqueidentifier")
				  .HasDefaultValueSql("newsequentialid()")
				  .ValueGeneratedOnAdd();

			// nvarchar(255) NOT NULL
			entity.Property(e => e.FirstName)
				  .IsRequired()
				  .IsUnicode(true)
				  .HasMaxLength(255);

			entity.Property(e => e.LastName)
				  .IsRequired()
				  .IsUnicode(true)
				  .HasMaxLength(255);

			// nvarchar(255) NULL
			entity.Property(e => e.IdentificationNumber)
				  .IsUnicode(true)
				  .HasMaxLength(255);

			// datetime2 NOT NULL
			entity.Property(e => e.BirthDate)
				  .IsRequired()
				  .HasColumnType("datetime2");

			// int NOT NULL (enum -> int)
			entity.Property(e => e.Gender)
				  .IsRequired()
				  .HasConversion<int>();

			entity.Property(e => e.Nationality)
				  .IsRequired()
				  .HasConversion<int>();

			// bit NOT NULL
			entity.Property(e => e.HasGrpc)
				  .IsRequired()
				  .HasColumnType("bit");

			// nvarchar(255) NOT NULL for Email
			entity.Property(e => e.Email)
				  .IsRequired()
				  .IsUnicode(true)
				  .HasMaxLength(255);

		});
	}
}
