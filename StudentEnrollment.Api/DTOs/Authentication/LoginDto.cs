using FluentValidation;

namespace StudentEnrollment.Api.DTOs.Authentication;

public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(login => login.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(login => login.Password)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(20);
    }
}
