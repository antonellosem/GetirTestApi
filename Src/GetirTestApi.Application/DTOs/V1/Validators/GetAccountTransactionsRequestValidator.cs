using GetirTestApi.Application.Resources;

using FluentValidation;

namespace GetirTestApi.Application.DTOs.V1.Validators
{
    public class GetAccountTransactionsRequestValidator : AbstractValidator<GetAccountTransactionsRequestDto>
    {
        public GetAccountTransactionsRequestValidator(IValidationService validationService)
        {
            ArgValidator.NotNull(validationService, nameof(validationService));

            RuleFor(request => request.AccountId)
                .NotEmpty()
                .WithMessage(ValidationMessages.MISSING_ACCOUNT_ID);

            RuleFor(request => request.From)
                .NotNull()
                .WithMessage(ValidationMessages.MISSING_DATE_FROM);

            RuleFor(request => request.To)
                .NotNull()
                .WithMessage(ValidationMessages.MISSING_DATE_TO);

            When(request => request.AccountId != null, () =>
            {
                RuleFor(request => request.AccountId)
                    .MustAsync((request, cancellation) => validationService.IsValidAccountId(request.Value))
                    .WithMessage(ValidationMessages.INVALID_ACCOUNT_ID);
            });

            When(request => request.From.HasValue && request.To.HasValue, () =>
              {
                  RuleFor(request => request)
                      .Must(request => request.From.Value < request.To.Value)
                      .WithMessage(ValidationMessages.INVALID_CUSTOMER_IBAN);
              });
        }
    }
}
