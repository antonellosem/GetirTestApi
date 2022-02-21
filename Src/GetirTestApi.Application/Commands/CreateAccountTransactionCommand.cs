using GetirTestApi.Application.DTOs.V1;

using MediatR;

namespace GetirTestApi.Application
{
    public class CreateAccountTransactionCommand : IRequest<CreateAccountTransactionResponseDto>
    {
        public CreateAccountTransactionRequestDto Body { get; private set; }

        public CreateAccountTransactionCommand(CreateAccountTransactionRequestDto body)
        {
            ArgValidator.NotNull(body, nameof(body));

            Body = body;
        }
    }
}
