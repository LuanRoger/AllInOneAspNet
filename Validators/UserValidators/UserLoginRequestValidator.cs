using AllInOneAspNet.Models.UserModels;
using FluentValidation;

namespace AllInOneAspNet.Validators.UserValidators;

public class UserLoginRequestValidator : AbstractValidator<UserLoginRequestModel>
{
    public UserLoginRequestValidator()
    {
        RuleFor(model => model.username)
            .NotNull()
            .NotEmpty();
        RuleFor(model => model.password)
            .NotNull()
            .NotEmpty();
    }
}