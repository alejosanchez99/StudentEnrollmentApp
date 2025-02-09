﻿using FluentValidation;
using StudentEnrollment.Api.DTOs.Course;
using StudentEnrollment.Api.DTOs.Student;

namespace StudentEnrollment.Api.DTOs.Enrollment;

public class EnrollmentDto : CreateEnrollmentDto
{
    public virtual CourseDto Course { get; set; }
    public virtual StudentDto Student { get; set; }
}

public class EnrollmentDtoValidator : AbstractValidator<EnrollmentDto>
{
    public EnrollmentDtoValidator()
    {
        Include(new CreateEnrollmentDtoValidator());
    }
}