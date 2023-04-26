using AllInOneAspNet.Models.UserModels;
using FluentValidation;

namespace AllInOneAspNet.Validators.UserValidators;

public class UserSigninRequestValidator : AbstractValidator<UserSigninRequestModel>
{
    public UserSigninRequestValidator()
    {
        RuleFor(model => model.username)
            .NotNull()
            .NotEmpty();
        RuleFor(model => model.password)
            .NotNull()
            .NotEmpty();
        RuleFor(model => model.email)
            .EmailAddress();
    }
}