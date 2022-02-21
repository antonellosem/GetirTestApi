using GetirTestApi.Application.Resources;

using FluentValidation;

namespace GetirTestApi.Application.DTOs.V1.Validators
{
    public class CreateAccountTransactionRequestValidator : AbstractValidator<CreateAccountTransactionRequestDto>
    {
        public CreateAccountTransactionRequestValidator(IValidationService validationService)
        {
            ArgValidator.NotNull(validationService, nameof(validationService));

            RuleFor(request => request.User)
                .NotEmpty()
                .WithMessage(ValidationMessages.MISSING_USER);

            RuleFor(request => request.IsWithdrawal)
                .NotEmpty()
                .WithMessage(ValidationMessages.MISSING_TRANSACTION_ISFLAG);

            RuleFor(request => request.Amount)
                .NotEmpty()
                .WithMessage(ValidationMessages.MISSING_TRANSACTION_AMOUNT);

            RuleFor(request => request.AccountId)
                .NotEmpty()
                .WithMessage(ValidationMessages.MISSING_ACCOUNT_ID);

            When(request => request.AccountId.HasValue, () =>
            {
                RuleFor(request => request.AccountId)
                    .MustAsync((request, cancellation) => validationService.IsValidAccountId(request.Value))
                    .WithMessage(ValidationMessages.INVALID_ACCOUNT_ID);
            });

            When(request => request.Amount.HasValue, () =>
            {
                RuleFor(request => request.Amount)
                    .Must(request => validationService.IsValidAmountRange(request.Value))
                    .WithMessage(ValidationMessages.TRANSACTION_AMOUNT_OUTOFRANGE);

                RuleFor(request => request.Amount)
                    .Must(request => validationService.IsValidAmountScale(request.Value))
                    .WithMessage(ValidationMessages.TRANSACTION_AMOUNT_OUTOFRANGE);
            });
        }
    }
}
