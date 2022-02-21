using System;

using GetirTestApi.Application.Resources;

using FluentValidation;

namespace GetirTestApi.Application.DTOs.V1.Validators
{
    public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequestDto>
    {
        public CreateCustomerRequestValidator(IValidationService validationService)
        {
            ArgValidator.NotNull(validationService, nameof(validationService));

            RuleFor(request => request.User)
                .NotEmpty()
                .WithMessage(ValidationMessages.MISSING_USER);

            RuleFor(request => request.FirstName)
                .NotEmpty()
                .WithMessage(ValidationMessages.MISSING_CUSTOMER_FIRSTNAME);

            RuleFor(request => request.LastName)
                .NotEmpty()
                .WithMessage(ValidationMessages.MISSING_CUSTOMER_LASTNAME);

            RuleFor(request => request.NationalId)
                .NotEmpty()
                .WithMessage(ValidationMessages.MISSING_CUSTOMER_NID);

            RuleFor(request => request.Birthdate)
                .NotNull()
                .WithMessage(ValidationMessages.MISSING_CUSTOMER_BDATE);

            When(request => !string.IsNullOrWhiteSpace(request.NationalId), () =>
            {
                RuleFor(request => request.NationalId)
                    .MustAsync((request, cancellation) => validationService.IsValidNationalId(request.Trim()))
                    .WithMessage(ValidationMessages.INVALID_CUSTOMER_NID);
            });

            When(request => request.Birthdate.HasValue, () =>
            {
                RuleFor(request => request.Birthdate)
                    .Must(request => request.Value <= DateTime.UtcNow.AddYears(-18))
                    .WithMessage(ValidationMessages.INVALID_CUSTOMER_BDATE);
            });
        }
    }
}
