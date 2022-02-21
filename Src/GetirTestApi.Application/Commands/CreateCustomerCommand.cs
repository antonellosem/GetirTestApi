using GetirTestApi.Application.DTOs.V1;

using MediatR;

namespace GetirTestApi.Application
{
    public class CreateCustomerCommand : IRequest<CreateCustomerResponseDto>
    {
        public CreateCustomerRequestDto Body { get; private set; }

        public CreateCustomerCommand(CreateCustomerRequestDto body)
        {
            ArgValidator.NotNull(body, nameof(body));

            Body = body;
        }
    }
}
