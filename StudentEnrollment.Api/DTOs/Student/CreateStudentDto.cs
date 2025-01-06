using FluentValidation;

namespace StudentEnrollment.Api.DTOs.Student;

public class CreateStudentDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string IdNumber { get; set; }
    public byte[] ProfilePicture { get; set; }
    public string OriginalFileName { get; set; }
}

public class CreateStudentDtoValidator : AbstractValidator<CreateStudentDto>
{
    public CreateStudentDtoValidator()
    {
        RuleFor(createStudent => createStudent.FirstName)
            .NotEmpty();
        RuleFor(createStudent => createStudent.LastName)
            .NotEmpty();
        RuleFor(createStudent => createStudent.DateOfBirth)
            .NotEmpty();
        RuleFor(createStudent => createStudent.IdNumber)
            .NotEmpty();
        RuleFor(createStudent => createStudent.OriginalFileName)
            .NotNull()
            .When(createStudent => createStudent.ProfilePicture != null);
    }
}