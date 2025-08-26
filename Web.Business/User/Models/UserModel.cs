
namespace Web.Business.User.Models;
public class UserModel
{
	public Guid UserId { get; set; }
	public required string FirstName { get; set; }

	public required string LastName { get; set; }

	public string? IdentificationNumber { get; set; }

	public DateTime BirthDate { get; set; }

	public int Gender { get; set; }

	public required string Email { get; set; }

	public int Nationality { get; set; }

	public bool HasGrpc { get; set; }
}
