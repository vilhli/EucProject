namespace Web.Contracts.User;
public sealed record class UserDto
{
	public Guid UserId { get; set; }
	public required string FirstName { get; set; }

	public required string LastName { get; set; }

	public DateTime BirthDate { get; set; }

	public required string Email { get; set; }

	public NationalityTypeDto Nationality { get; set; }
}
