using MediatR;

namespace GetirTestApi.Domain.Events
{
    public class CustomerCreatedDomainEvent : INotification
    {
        public Customer Customer { get;private set; }   

        public CustomerCreatedDomainEvent(Customer customer)
        {
            ArgValidator.NotNull(customer, nameof(customer));

            Customer = customer;
        }
    }
}
