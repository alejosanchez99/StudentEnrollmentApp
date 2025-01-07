using FluentValidation;

namespace StudentEnrollment.Api.DTOs.Course;

public class CreateCourseDto
{
    public string Title { get; set; }
    public int Credits { get; set; }
}

public class CreateCourseDtoValidator : AbstractValidator<CreateCourseDto>
{
    public CreateCourseDtoValidator()
    {
        RuleFor(createCourse => createCourse.Title)
            .NotEmpty();
        RuleFor(createCourse => createCourse.Credits)
            .GreaterThan(0);
    }
}