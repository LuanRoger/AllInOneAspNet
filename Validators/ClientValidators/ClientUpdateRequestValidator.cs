using AllInOneAspNet.Models.ClientModels;
using FluentValidation;

namespace AllInOneAspNet.Validators.ClientValidators;

public class ClientUpdateRequestValidator : AbstractValidator<ClientUpdateRequestModel>
{
    public ClientUpdateRequestValidator()
    {
        RuleFor(model => model.username)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100);
    }
}