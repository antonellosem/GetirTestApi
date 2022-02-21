using GetirTestApi.Application.DTOs.V1;

using MediatR;

namespace GetirTestApi.Application
{
    public class GetCustomerAccountsCommand : IRequest<GetCustomerAccountsResponseDto>
    {
        public GetCustomerAccountsRequestDto Query { get; private set; }

        public GetCustomerAccountsCommand(GetCustomerAccountsRequestDto query)
        {
            ArgValidator.NotNull(query, nameof(query));

            Query = query;
        }
    }
}
