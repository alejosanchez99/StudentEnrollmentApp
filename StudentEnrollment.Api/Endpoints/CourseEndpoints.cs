using AutoMapper;
using StudentEnrollment.Api.DTOs.Course;
using StudentEnrollment.Data;
using StudentEnrollment.Data.Contracts;

namespace StudentEnrollment.Api.Endpoints;

public static class CourseEndpoints
{
    public static void MapCourseEndpoints(this IEndpointRouteBuilder routes)
    {
        RouteGroupBuilder group = routes.MapGroup("/api/Course").WithTags(nameof(Course));

        group.MapGet("/", async (ICourseRepository repository, IMapper mapper) =>
        {
            List<Course> courses = await repository.GetAllAsync();

            return mapper.Map<List<CourseDto>>(courses);
        })
        .WithName("GetAllCourses")
        .WithOpenApi()
        .Produces<List<Course>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async (int id, ICourseRepository repository, IMapper mapper) =>
        {
            return await repository.GetAsync(id)
                    is Course model
                    ? Results.Ok(mapper.Map<CourseDto>(model))
                    : Results.NotFound();
        })
        .WithName("GetCourseById")
        .WithOpenApi()
        .Produces<Course>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
        
        group.MapGet("/GetDetails/{id}", async (int id, ICourseRepository repository, IMapper mapper) =>
        {
            return await repository.GetCourseDetails(id)
                    is Course model
                    ? Results.Ok(mapper.Map<CourseDetailsDto>(model))
                    : Results.NotFound();
        })
        .WithName("GetCourseDetailsById")
        .WithOpenApi()
        .Produces<Course>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id}", async (int id, CourseDto courseDto, ICourseRepository repository, IMapper mapper) =>
        {
            bool courseExists = await repository.Exists(id);

            if (!courseExists)
            {
                return Results.NotFound();
            }

            Course course = mapper.Map<Course>(courseDto);
            await repository.UpdateAsync(course);

            return Results.NoContent();
        })
        .WithName("UpdateCourse")
        .WithOpenApi()
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", async (CreateCourseDto courseDto, ICourseRepository repository, IMapper mapper) =>
        {
            Course course = mapper.Map<Course>(courseDto);

            await repository.AddAsync(course);

            return Results.Created($"/api/Course/{course.Id}", course);
        })
        .WithName("CreateCourse")
        .WithOpenApi()
        .Produces<Course>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", async (int id, ICourseRepository repository) =>
        {
            return await repository.DeleteAsync(id) ? Results.NoContent() : Results.NotFound();
        })
        .WithName("DeleteCourse")
        .WithOpenApi()
        .Produces<Course>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
