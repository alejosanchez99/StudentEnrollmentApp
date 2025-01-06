using FluentValidation;

namespace StudentEnrollment.Api.DTOs.Student;

public class StudentDto : CreateStudentDto
{
    public string Id { get; set; }

    public class StudentDtoValidator : AbstractValidator<StudentDto>
    {
        public StudentDtoValidator()
        {
            Include(new CreateStudentDtoValidator());
        }
    }
}

