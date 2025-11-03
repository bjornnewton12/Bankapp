namespace BlazorStandaloneApp.Interfaces
{
    /// <summary>
    /// Defines the structure and core operations of a bank account, 
    /// including balance management, transaction history, and 
    /// methods for withdrawing, depositing, and transferring funds.
    /// </summary>
    public interface IBankAccount
    {
        Guid Id { get; }
        AccountType AccountType { get; }
        CurrencyType CurrencyType { get; }
        string Name { get; }
        decimal Balance { get; }
        DateTime LastUpdated { get; }
        List <Transaction> Transactions { get; }
        void Withdraw(decimal amount);
        void Deposit(decimal amount);
        void TransferTo(BankAccount toAccount , decimal amount);
    }
}
