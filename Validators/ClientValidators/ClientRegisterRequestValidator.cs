using AllInOneAspNet.Models.ClientModels;
using FluentValidation;

namespace AllInOneAspNet.Validators.ClientValidators;

public class ClientRegisterRequestValidator : AbstractValidator<ClientRegisterRequestModel>
{
    public ClientRegisterRequestValidator()
    {
        RuleFor(model => model.username)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100);
    }
}