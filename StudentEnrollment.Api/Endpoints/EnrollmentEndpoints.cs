using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using StudentEnrollment.Api.DTOs.Enrollment;
using StudentEnrollment.Api.Filters;
using StudentEnrollment.Data;
using StudentEnrollment.Data.Contracts;

namespace StudentEnrollment.Api.Endpoints;

public static class EnrollmentEndpoints
{
    public static void MapEnrollmentEndpoints(this IEndpointRouteBuilder routes)
    {
        RouteGroupBuilder group = routes.MapGroup("/api/Enrollment")
                                        .WithTags(nameof(Enrollment))
                                        .AddEndpointFilter<LoggingFilter>();

        group.MapGet("/", async (IEnrollmentRepository repository, IMapper mapper) =>
        {
            List<Enrollment> courses = await repository.GetAllAsync();

            return mapper.Map<List<EnrollmentDto>>(courses);
        })
        .WithName("GetAllEnrollments")
        .WithOpenApi()
        .Produces<List<Enrollment>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async (int id, IEnrollmentRepository repository, IMapper mapper) =>
        {
            return await repository.GetAsync(id)
                is Enrollment model
                    ? Results.Ok(mapper.Map<List<EnrollmentDto>>(model))
                    : Results.NotFound();
        })
        .WithName("GetEnrollmentById")
        .WithOpenApi()
        .Produces<Enrollment>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id}", [Authorize(Roles = "Administrator")] async (int id, EnrollmentDto enrollmentDto, IEnrollmentRepository repository, IMapper mapper) =>
        {
            bool enrollmentExists = await repository.Exists(id);

            if (!enrollmentExists)
            {
                return Results.NotFound();
            }

            Enrollment enrollment = mapper.Map<Enrollment>(enrollmentDto);
            await repository.UpdateAsync(enrollment);

            return Results.NoContent();
        })
        .AddEndpointFilter<ValidationFilter<EnrollmentDto>>()
        .WithName("UpdateEnrollment")
        .WithOpenApi()
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", [Authorize(Roles = "Administrator")] async (EnrollmentDto enrollmentDto, IEnrollmentRepository repository, IMapper mapper) =>
        {
            Enrollment enrollment = mapper.Map<Enrollment>(enrollmentDto);

            await repository.AddAsync(enrollment);

            return Results.Created($"/api/Enrollment/{enrollment.Id}", enrollment);
        })
        .AddEndpointFilter<ValidationFilter<CreateEnrollmentDto>>()
        .WithName("CreateEnrollment")
        .WithOpenApi()
        .Produces<Enrollment>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", [Authorize(Roles = "Administrator")] async (int id, IEnrollmentRepository repository) =>
        {
            return await repository.DeleteAsync(id) ? Results.NoContent() : Results.NotFound();
        })
        .WithName("DeleteEnrollment")
        .WithOpenApi()
        .Produces<Enrollment>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
