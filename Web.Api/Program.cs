
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Web.Api.Endpoints;
using Web.Business;
using Web.Business.Common.Exceptions;
using Web.Infrastracture;

namespace Web.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        builder.Services.AddAppApiServices();
		builder.Services.AddAppBusinessServices();
		builder.Services.AddAppInfrastructureServices(builder.Configuration);

        var app = builder.Build();

		app.UseCors("AppPolicy");

		app.UseExceptionHandler(errorApp =>
		{
			errorApp.Run(async context =>
			{
				context.Response.ContentType = "application/json";

				var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
				var exception = exceptionHandlerPathFeature?.Error;

				if (exception is ValidationException validationException)
				{
					var errorResponse = new
					{
						error = "Validation failed",
						details = validationException.Errors.Select(x => new
						{
							x.PropertyName,
							x.ErrorMessage,
						})
					};

					context.Response.StatusCode = StatusCodes.Status400BadRequest;
					await context.Response.WriteAsJsonAsync(errorResponse);
				}
				else if (exception is NotFoundException notFoundException)
				{
					var errorResponse = new
					{
						error = "Not found",
						details = notFoundException.Message,
					};

					context.Response.StatusCode = StatusCodes.Status404NotFound;
					await context.Response.WriteAsJsonAsync(errorResponse);
				}
				else
				{
					var errorResponse = new
					{
						error = "An unexpected error occurred.",
						details = exception?.Message
					};

					context.Response.StatusCode = StatusCodes.Status500InternalServerError;
					await context.Response.WriteAsJsonAsync(errorResponse);
				}
			});
		});

		app.MapDefaultEndpoints();
        app.MapAppEndpoints();

        if (app.Environment.IsDevelopment())
        {
			app.UseSwagger();
			app.UseSwaggerUI();
			//app.UseDeveloperExceptionPage();
		}

		app.UseHttpsRedirection();

        app.Run();
    }
}
