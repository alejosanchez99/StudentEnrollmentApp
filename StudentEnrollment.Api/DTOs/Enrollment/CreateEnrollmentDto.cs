using FluentValidation;

namespace StudentEnrollment.Api.DTOs.Enrollment;

public class CreateEnrollmentDto
{
    public int CourseId { get; set; }
    public int StudentId { get; set; }
}

public class CreateEnrollmentDtoValidator : AbstractValidator<CreateEnrollmentDto>
{
    public CreateEnrollmentDtoValidator()
    {
        RuleFor(createEnrollment => createEnrollment.CourseId)
            .NotEmpty();
        RuleFor(createEnrollment => createEnrollment.StudentId)
            .NotEmpty();
    }
}