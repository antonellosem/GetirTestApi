using MediatR;

namespace GetirTestApi.Domain.Events
{
    public class TransactionCreatedDomainEvent : INotification
    {
        public Customer Customer { get;private set; }   
        public Account Account { get;private set; }   
        public Transaction Transaction { get;private set; }

        public TransactionCreatedDomainEvent(Customer customer, Account account, Transaction transaction)
        {
            ArgValidator.NotNull(customer, nameof(customer));
            ArgValidator.NotNull(account, nameof(account));
            ArgValidator.NotNull(transaction, nameof(transaction));

            Customer = customer;
            Account = account;
            Transaction = transaction;
        }
    }
}
