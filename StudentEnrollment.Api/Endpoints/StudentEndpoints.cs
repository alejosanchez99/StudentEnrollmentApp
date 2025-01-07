using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using StudentEnrollment.Api.DTOs.Student;
using StudentEnrollment.Api.Filters;
using StudentEnrollment.Api.Services;
using StudentEnrollment.Data;
using StudentEnrollment.Data.Contracts;

namespace StudentEnrollment.Api.Endpoints;

public static class StudentEndpoints
{
    public static void MapStudentEndpoints(this IEndpointRouteBuilder routes)
    {
        RouteGroupBuilder group = routes.MapGroup("/api/Student")
                                        .WithTags(nameof(Student))
                                        .AddEndpointFilter<LoggingFilter>();

        group.MapGet("/", async (IStudentRepository repository, IMapper mapper) =>
        {
            List<Student> students = await repository.GetAllAsync();

            return mapper.Map<List<StudentDto>>(students);
        })
        .AllowAnonymous()
        .WithName("GetAllStudents")
        .WithOpenApi()
        .Produces<List<Student>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async (int id, IStudentRepository repository, IMapper mapper) =>
        {
            return await repository.GetAsync(id) is Student model
                    ? Results.Ok(mapper.Map<StudentDto>(model))
                    : Results.NotFound();
        })
        .AllowAnonymous()
        .WithName("GetStudentById")
        .WithOpenApi()
        .Produces<Student>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/GetDetails/{id}", async (int id, IStudentRepository repository, IMapper mapper) =>
        {
            return await repository.GetStudentDetails(id) is Student model
                    ? Results.Ok(mapper.Map<StudentDetailsDto>(model))
                    : Results.NotFound();
        })
        .AllowAnonymous()
        .WithName("GetStudentDetailsById")
        .WithOpenApi()
        .Produces<Student>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id}", [Authorize(Roles = "Administrator")] async (int id, StudentDto studentDto, IStudentRepository repository, IMapper mapper, IFileUpload fileUpload) =>
        {
            bool studentExists = await repository.Exists(id);

            if (!studentExists)
            {
                return Results.NotFound();
            }

            Student student = mapper.Map<Student>(studentDto);

            if (studentDto.ProfilePicture != null)
            {
                student.Picture = fileUpload.UploadStudentFile(studentDto.ProfilePicture, studentDto.OriginalFileName);
            }

            await repository.UpdateAsync(student);

            return Results.NoContent();
        })
        .AddEndpointFilter<ValidationFilter<StudentDto>>()
        .WithName("UpdateStudent")
        .WithOpenApi()
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", [Authorize(Roles = "Administrator")] async (CreateStudentDto studentDto, IStudentRepository repository, IMapper mapper, IFileUpload fileUpload) =>
        {
            Student student = mapper.Map<Student>(studentDto);

            student.Picture = fileUpload.UploadStudentFile(studentDto.ProfilePicture, studentDto.OriginalFileName);

            await repository.AddAsync(student);

            return Results.Created($"/api/Student/{student.Id}", student);
        })
        .AddEndpointFilter<ValidationFilter<CreateStudentDto>>()
        .WithName("CreateStudent")
        .WithOpenApi()
        .Produces<Student>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", [Authorize(Roles = "Administrator")] async (int id, IStudentRepository repository) =>
        {
            return await repository.DeleteAsync(id) ? Results.NoContent() : Results.NotFound();
        })
        .WithName("DeleteStudent")
        .WithOpenApi()
        .Produces<Student>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
