using System;
using System.Collections.Generic;

using GetirTestApi.Abstractions;

namespace GetirTestApi.Domain
{
    public class Transaction : ValueObject
    {
        public Transaction(string createdBy, bool isWithdrawal, decimal amount)
            : base(DateTime.UtcNow, createdBy)
        {
            ArgValidator.NotNull(isWithdrawal, nameof(isWithdrawal));
            ArgValidator.NotNull(amount, nameof(amount));

            IsWithdrawal = isWithdrawal;
            Amount = amount;
        }

        public bool IsWithdrawal { get; private set; }
        public decimal Amount { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return CreatedOn;
            yield return CreatedBy;
            yield return IsWithdrawal;
            yield return Amount;
        }
    }
}
