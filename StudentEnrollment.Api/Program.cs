using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Data;
using StudentEnrollment.Api.Endpoints;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("StudentEnrollmentDbConnection")!;

builder.Services.AddDbContext<StudentEnrollmentDbContext>(options =>
{
    options.UseSqlServer(connection);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy => policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

app.UseHttpsRedirection();
app.UseCors("AllowAll");


app.MapGet("/courses", async (StudentEnrollmentDbContext context) =>
{
    return await context.Courses.ToListAsync();
});

app.MapGet("/courses/{id}", async (StudentEnrollmentDbContext context, int id) =>
{
    return await context.Courses.FindAsync(id) is Course course ? Results.Ok(course) : Results.NotFound();
});

app.MapPost("/courses", async (StudentEnrollmentDbContext context, Course course) =>
{
    await context.Courses.AddAsync(course);
    await context.SaveChangesAsync();

    return Results.Created($"/courses/{course.Id}", course);
});

app.MapPut("/courses/{id}", async (StudentEnrollmentDbContext context, Course course, int id) =>
{
    bool courseExists = await context.Courses.AnyAsync(course => course.Id == id);
    if (!courseExists)
    {
        return Results.NotFound();
    }

    context.Update(course);
    await context.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/courses/{id}", async (StudentEnrollmentDbContext context, int id) =>
{
    Course? course = await context.Courses.FindAsync(id);
    if (course == null)
    {
        return Results.NotFound();
    }

    context.Remove(course);
    await context.SaveChangesAsync();

    return Results.NoContent();
});

app.MapStudentEndpoints();

app.MapEnrollmentEndpoints();

app.Run();