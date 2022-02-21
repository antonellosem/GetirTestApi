using System;
using System.Collections.Generic;

using GetirTestApi.Abstractions;

namespace GetirTestApi.Domain
{
    public class Account : DomainEntity<Guid>
    {
        protected Account() : base(DateTime.UtcNow, string.Empty)
        {
            _transactions = new List<Transaction>();
        }

        public Account(string createdBy, string iban) 
            : base(DateTime.UtcNow, createdBy)
        {
            ArgValidator.NotNull(createdBy, nameof(createdBy));
            ArgValidator.NotNull(iban, nameof(iban));
          
            IBAN = iban;
            Balance = 0m;

            _transactions = new List<Transaction>();
        }

        public string IBAN { get; private set; }
        public decimal Balance { get; private set; }

        public IReadOnlyCollection<Transaction> Transactions => (List<Transaction>)_transactions;
        private readonly ICollection<Transaction> _transactions;

        internal Transaction AddTransaction(string user, bool isWithdrawal, decimal amount)
        {
            var newTransaction = new Transaction(user, isWithdrawal, amount);

            _transactions.Add(newTransaction);

            return newTransaction;
        }

        public Account AddMoney(decimal value)
        {
            Balance += value;

            return this;
        }

        public Account Withdraw(decimal value)
        {
            Balance -= value;

            return this;
        }
    }
}
