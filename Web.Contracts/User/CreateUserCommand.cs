using EucProject.Resources;
using FluentValidation;
using MediatR;
using Web.Contracts.Interfaces;

namespace Web.Contracts.User;
public sealed record class CreateUserCommand : IRequest
{
	public string? FirstName { get; set; }

	public string? LastName { get; set; }

	public string? IdentificationNumber { get; set; }

	public bool HasIdentificationNumber { get; set; }

	public DateTime? BirthDate { get; set; }

	public GenderTypeDto? Gender { get; set; }

	public string? Email { get; set; }

	public NationalityTypeDto? Nationality { get; set; }

	public bool HasGrpc { get; set; }

	public class Validator : AbstractValidator<CreateUserCommand>, IAppValidator
	{
		public Validator()
		{
			RuleFor(x => x.FirstName)
				.NotEmpty()
				.Matches("^[a-zA-Z0-9]*$").WithMessage(AppRes.NameValidCharacters);

			RuleFor(x => x.LastName)
				.NotEmpty()
				.Matches("^[a-zA-Z0-9]*$").WithMessage(AppRes.NameValidCharacters);

			RuleFor(x => x.IdentificationNumber)
				.NotEmpty()
				.Matches("^\\d{6}(?:/\\d{4}|\\d{4})?$")
				.WithMessage(AppRes.CzechIdentificationNumber)
				.When(x => !x.HasIdentificationNumber)
				.WithMessage(AppRes.IdentificationNumberMandatory);

			RuleFor(x => x.BirthDate)
				.NotEmpty();

			RuleFor(x => x.Gender)
				.NotEmpty();

			RuleFor(x => x.Email)
				.NotEmpty()
				.EmailAddress();

			RuleFor(x => x.Nationality)
				.NotEmpty();

			RuleFor(x => x.HasGrpc)
				.Equal(true).WithMessage(AppRes.GrpcMandatory);

		}
	}
}
