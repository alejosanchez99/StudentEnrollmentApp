using FluentValidation;
using Microsoft.AspNetCore.Identity;
using StudentEnrollment.Api.DTOs;
using StudentEnrollment.Api.DTOs.Authentication;
using StudentEnrollment.Api.Filters;
using StudentEnrollment.Api.Services;

namespace StudentEnrollment.Api.Endpoints;

public static class AuthenticationEndpoints
{
    public static void MapAuthenticationEndpoints(this IEndpointRouteBuilder routes)
    {
        RouteGroupBuilder group = routes.MapGroup("/api")
                                        .WithTags("Authentication")
                                        .AddEndpointFilter<LoggingFilter>();

        group.MapPost("/login/", async (LoginDto loginDto, IAuthManager authManager) =>
        {
            AuthResponseDto response = await authManager.Login(loginDto);
            if (response == null)
            {
                return Results.Unauthorized();
            }

            return Results.Ok(response);
        })
        .AddEndpointFilter<ValidationFilter<LoginDto>>()
        .AllowAnonymous()
        .WithName("Login")
        .WithOpenApi()
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized);


        group.MapPost("/register/", async (RegisterDto registerDto, IAuthManager authManager) =>
        {
            IEnumerable<IdentityError> response = await authManager.Register(registerDto);

            if (!response.Any())
            {
                return Results.Ok();
            }

            List<ErrorResponseDto> errors = (from error in response
                                             select new ErrorResponseDto
                                             {
                                                 Code = error.Code,
                                                 Description = error.Description
                                             }).ToList();
            return Results.BadRequest(errors);
        })
        .AddEndpointFilter<ValidationFilter<RegisterDto>>()
        .AllowAnonymous()
        .WithName("Register")
        .WithOpenApi()
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
