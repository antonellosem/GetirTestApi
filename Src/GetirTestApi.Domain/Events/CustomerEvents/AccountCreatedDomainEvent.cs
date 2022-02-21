using MediatR;

namespace GetirTestApi.Domain.Events
{
    public class AccountCreatedDomainEvent : INotification
    {
        public Customer Customer { get;private set; }   
        public Account Account { get;private set; }   

        public AccountCreatedDomainEvent(Customer customer, Account account)
        {
            ArgValidator.NotNull(customer, nameof(customer));
            ArgValidator.NotNull(account, nameof(account));

            Customer = customer;
            Account = account;
        }
    }
}
