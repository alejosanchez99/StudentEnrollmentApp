using AutoMapper;
using StudentEnrollment.Api.DTOs.Student;
using StudentEnrollment.Data;
using StudentEnrollment.Data.Contracts;

namespace StudentEnrollment.Api.Endpoints;

public static class StudentEndpoints
{
    public static void MapStudentEndpoints(this IEndpointRouteBuilder routes)
    {
        RouteGroupBuilder group = routes.MapGroup("/api/Student").WithTags(nameof(Student));

        group.MapGet("/", async (IStudentRepository repository, IMapper mapper) =>
        {
            List<Student> students = await repository.GetAllAsync();

            return mapper.Map<List<StudentDto>>(students);
        })
        .WithName("GetAllStudents")
        .WithOpenApi()
        .Produces<List<Student>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async (int id, IStudentRepository repository, IMapper mapper) =>
        {
            return await repository.GetAsync(id) is Student model
                    ? Results.Ok(mapper.Map<StudentDto>(model))
                    : Results.NotFound();
        })
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
        .WithName("GetStudentDetailsById")
        .WithOpenApi()
        .Produces<Student>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id}", async (int id, StudentDto studentDto, IStudentRepository repository, IMapper mapper) =>
        {
            bool studentExists = await repository.Exists(id);

            if (!studentExists)
            {
                return Results.NotFound();
            }

            Student student = mapper.Map<Student>(studentDto);
            await repository.UpdateAsync(student);

            return Results.NoContent();
        })
        .WithName("UpdateStudent")
        .WithOpenApi()
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", async (StudentDto studentDto, IStudentRepository repository, IMapper mapper) =>
        {
            Student student = mapper.Map<Student>(studentDto);

            await repository.AddAsync(student);

            return Results.Created($"/api/Student/{student.Id}", student);
        })
        .WithName("CreateStudent")
        .WithOpenApi()
        .Produces<Student>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", async (int id, IStudentRepository repository) =>
        {
            return await repository.DeleteAsync(id) ? Results.NoContent() : Results.NotFound();
        })
        .WithName("DeleteStudent")
        .WithOpenApi()
        .Produces<Student>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
