using System;
using NewPacg.Common.Enums;

namespace NewPacg.Common.Entities
{
    /// <summary>
    /// Represents a simple financial transaction. This model mirrors
    /// structures found in the legacy system and will be expanded during
    /// future refactoring stages.
    /// </summary>
    public class Transaction
    {
        /// <summary>Unique identifier of the transaction.</summary>
        public Guid Id { get; set; }

        /// <summary>Identifier of the user that performed the transaction.</summary>
        public int UserId { get; set; }

        /// <summary>Amount involved in the transaction.</summary>
        public decimal Amount { get; set; }

        /// <summary>Category of the transaction.</summary>
        public TransactionType Type { get; set; }

        /// <summary>Date and time when the transaction occurred.</summary>
        public DateTime Timestamp { get; set; }
    }
}
