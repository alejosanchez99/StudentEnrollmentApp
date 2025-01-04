using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Api.Configurations;
using StudentEnrollment.Api.Endpoints;
using StudentEnrollment.Data;
using StudentEnrollment.Data.Contracts;
using StudentEnrollment.Data.Repositories;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("StudentEnrollmentDbConnection")!;

builder.Services.AddDbContext<StudentEnrollmentDbContext>(options =>
{
    options.UseSqlServer(connection);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy => policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});

builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.MapStudentEndpoints();
app.MapEnrollmentEndpoints();
app.MapCourseEndpoints();

app.Run();