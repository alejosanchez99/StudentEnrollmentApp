using FluentValidation;

namespace StudentEnrollment.Api.DTOs.Authentication;

public class RegisterDto : LoginDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
}

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        Include(new LoginDtoValidator());

        RuleFor(register => register.FirstName)
            .NotEmpty();
        RuleFor(register => register.LastName)
            .NotEmpty();
        RuleFor(register => register.DateOfBirth)
            .Must((dateOfBirth) =>
            {
                return !dateOfBirth.HasValue || dateOfBirth.Value < DateTime.Now;
            });
    }
}