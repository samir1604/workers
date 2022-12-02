using FluentValidation;

using Workers.Contracts.Authentication;

namespace Workers.Api.Validations;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest> {
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email).EmailAddress().NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty().MinimumLength(3).MaximumLength(30);
    }
}