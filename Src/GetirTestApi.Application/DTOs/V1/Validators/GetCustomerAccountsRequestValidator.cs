using GetirTestApi.Application.Resources;

using FluentValidation;

namespace GetirTestApi.Application.DTOs.V1.Validators
{
    public class GetCustomerAccountsRequestValidator : AbstractValidator<GetCustomerAccountsRequestDto>
    {
        public GetCustomerAccountsRequestValidator(IValidationService validationService)
        {
            ArgValidator.NotNull(validationService, nameof(validationService));

            RuleFor(request => request.CustomerId)
                .NotNull()
                .WithMessage(ValidationMessages.MISSING_CUSTOMER_ID);

            When(request => request.CustomerId.HasValue, () =>
            {
                RuleFor(request => request.CustomerId)
                    .MustAsync((request, cancellation) => validationService.IsValidCustomerId(request.Value))
                    .WithMessage(ValidationMessages.INVALID_CUSTOMER_ID);
            });
        }
    }
}
