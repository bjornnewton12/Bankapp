namespace BlazorStandaloneApp.Interfaces

{
    /// <summary>
    /// Interface containing Bankaccount methods
    /// </summary>
    public interface IBankAccount
    {
        Guid Id { get; }
        AccountType AccountType { get; }

        CurrencyType CurrencyType { get; }
        string Name { get; }

        decimal Balance { get; }
        DateTime LastUpdated { get; }

        // Lade till med Arbet 2025-10-23
        // IReadOnlyList<Transaction> Transactions { get; }
        List <Transaction> Transactions { get; }

        void Withdraw(decimal amount);
        void Deposit(decimal amount);

        void TransferTo(BankAccount toAccount , decimal amount);
    }
}
