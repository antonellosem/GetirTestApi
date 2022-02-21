using System;
using System.Collections.Generic;
using System.Linq;

using GetirTestApi.Abstractions;
using GetirTestApi.Domain.Events;

namespace GetirTestApi.Domain
{
    public class Customer : RootAggregate
    {
        protected Customer() : base(DateTime.UtcNow, string.Empty)
        {
            _accounts = new List<Account>();
        }

        public Customer(string createdBy, string nationalId, DateTime birthdate, string firstName, string lastName)
            : base(DateTime.UtcNow, createdBy)
        {
            ArgValidator.NotNull(nationalId, nameof(nationalId));
            ArgValidator.NotNull(birthdate, nameof(birthdate));
            ArgValidator.NotNull(firstName, nameof(firstName));
            ArgValidator.NotNull(lastName, nameof(lastName));

            FirstName = firstName;
            LastName = lastName;
            Birthdate = birthdate;
            NationalId = nationalId;

            _accounts = new List<Account>();
            AddDomainEvent(new CustomerCreatedDomainEvent(this));
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime Birthdate { get; private set; }
        public string NationalId { get; private set; }

        public IReadOnlyCollection<Account> Accounts => (List<Account>)_accounts;
        private readonly ICollection<Account> _accounts;

        public Account AddAccount(string user, string iban)
        {
            var newAccount = new Account(user, iban);
            _accounts.Add(newAccount);

            AddDomainEvent(new AccountCreatedDomainEvent(this, newAccount));

            return newAccount;
        }

        public Transaction AddTransaction(Guid accountId, string user, bool isWithdrawal, decimal amount)
        {
            var account = _accounts.FirstOrDefault(x => x.Id.Equals(accountId));

            if (account == null)
                throw new ArgumentNullException("The accountId provided is not matching within the customer accounts list.");

            var newTransaction = account.AddTransaction(user, isWithdrawal, amount);

            AddDomainEvent(new TransactionCreatedDomainEvent(this, account, newTransaction));

            return newTransaction;
        }
    }
}
