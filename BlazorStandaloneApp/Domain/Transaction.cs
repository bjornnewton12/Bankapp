using System;

namespace BlazorStandaloneApp.Domain
{

    public class Transaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; }
        public TransactionType Type { get; set; }
        public decimal BalanceAfterTransaction { get; set; }
        public Guid FromAccount { get; set; }
        public Guid ToAccount { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public string? RelatedAccountName { get; private set; }
        public string? Description { get; private set; }
    }
}