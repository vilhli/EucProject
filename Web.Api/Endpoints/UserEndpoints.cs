using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Business.User.Queries;
using Web.Contracts.Common;
using Web.Contracts.User;

namespace Web.Api.Endpoints;

public static class UserEndpoints
{
	public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder builder)
	{
		var group = builder.MapGroup("/users").WithTags("Users");
		group.MapGet("/", GetUsersAsync);
		group.MapPost("/", CreateUserAsync);
		group.MapGet($"/download/{{userId:Guid}}", DownloadUserAsync);
		return builder;
	}

	private static async Task<GetUsersDto> GetUsersAsync([FromServices] ISender sender)
	{
		return await sender.Send(new GetUsersQuery());
	}

	private static async Task<IResult> CreateUserAsync([FromServices] ISender sender, [FromBody] CreateUserCommand command)
	{
		await sender.Send(command);
		return Results.Accepted();
	}

	public static async Task<FileDto> DownloadUserAsync(
		HttpContext httpContext,
		[FromRoute] Guid userId,
		[FromServices] ISender sender,
		CancellationToken cancellationToken)
	{
		return await sender.Send(new GetUserJsonQuery(userId), cancellationToken);
	}
}
