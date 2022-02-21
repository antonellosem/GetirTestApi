using GetirTestApi.Application.Resources;

using FluentValidation;

namespace GetirTestApi.Application.DTOs.V1.Validators
{
    public class CreateAccountRequestValidator : AbstractValidator<CreateAccountRequestDto>
    {
        public CreateAccountRequestValidator(IValidationService validationService)
        {
            ArgValidator.NotNull(validationService, nameof(validationService));

            RuleFor(request => request.User)
                .NotEmpty()
                .WithMessage(ValidationMessages.MISSING_USER);

            RuleFor(request => request.IBAN)
                .NotEmpty()
                .WithMessage(ValidationMessages.MISSING_IBAN);

            RuleFor(request => request.CustomerId)
                .NotNull()
                .WithMessage(ValidationMessages.MISSING_CUSTOMER_ID);

            When(request => request.CustomerId.HasValue, () =>
            {
                RuleFor(request => request.CustomerId)
                    .MustAsync((request, cancellation) => validationService.IsValidCustomerId(request.Value))
                    .WithMessage(ValidationMessages.INVALID_CUSTOMER_ID);
            });

            When(request => !string.IsNullOrWhiteSpace(request.IBAN), () =>
            {
                RuleFor(request => request.IBAN)
                    .Must(request => validationService.IsValidIBAN(request.Trim()))
                    .WithMessage(ValidationMessages.INVALID_CUSTOMER_IBAN);
            });
        }
    }
}
