using GetirTestApi.Application.DTOs.V1;

using MediatR;

namespace GetirTestApi.Application
{
    public class GetAccountTransactionsCommand : IRequest<GetAccountTransactionsResponseDto>
    {
        public GetAccountTransactionsRequestDto Query { get; private set; }

        public GetAccountTransactionsCommand(GetAccountTransactionsRequestDto query)
        {
            ArgValidator.NotNull(query, nameof(query));

            Query = query;
        }
    }
}
