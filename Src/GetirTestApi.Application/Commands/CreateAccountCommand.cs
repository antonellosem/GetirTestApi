using GetirTestApi.Application.DTOs.V1;

using MediatR;

namespace GetirTestApi.Application
{
    public class CreateAccountCommand : IRequest<CreateAccountResponseDto>
    {
        public CreateAccountRequestDto Body { get; private set; }

        public CreateAccountCommand(CreateAccountRequestDto body)
        {
            ArgValidator.NotNull(body, nameof(body));

            Body = body;
        }
    }
}
