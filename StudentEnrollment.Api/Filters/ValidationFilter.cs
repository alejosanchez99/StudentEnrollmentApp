
using FluentValidation;
using FluentValidation.Results;

namespace StudentEnrollment.Api.Filters;

public class ValidationFilter<T>(IValidator<T> validator) : IEndpointFilter where T : class
{
    private readonly IValidator<T> validator = validator;

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        T objectToValidate = context.GetArgument<T>(0);

        ValidationResult validationResult = await validator.ValidateAsync(objectToValidate);

        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.ToDictionary());
        }

        return await next(context);
    }
}

