using System.Text.Json.Serialization;

namespace BlazorStandaloneApp.Domain;

/// <summary>
/// Sammanfattning av klassen
/// </summary>

public class BankAccount : IBankAccount
{
    // Contents
    public Guid Id { get; private set; } = Guid.NewGuid();
    public AccountType AccountType { get; private set; }
    public CurrencyType CurrencyType { get; private set; }
    public string Name { get; private set; }
    public decimal Balance { get; private set; }
    public DateTime LastUpdated { get; private set; }
    public List<Transaction> Transactions { get; private set; } = new();

    public List<Transaction> GetTransactions()
    {
        return Transactions;
    }

    // Constructor
    public BankAccount(string name, AccountType accountType, CurrencyType currencyType, decimal balance)
    {
        Name = name;
        AccountType = accountType;
        CurrencyType = currencyType;
        Balance = balance;
        LastUpdated = DateTime.Now;
    }

    [JsonConstructor]
    public BankAccount(Guid id, string name, AccountType accountType, CurrencyType currencyType, decimal balance, DateTime lastUpdated, List<Transaction> transactions)
    {
        Id = id;
        Name = name;
        AccountType = accountType;
        CurrencyType = currencyType;
        Balance = balance;
        LastUpdated = lastUpdated;
        Transactions = transactions ?? new List<Transaction>();
    }

    /// <summary>
    /// Deposit specific amount to account balance
    /// </summary>
    /// <param name="amount"></param>
    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Deposit must be higher than 0");
        }

        Balance += amount;
        LastUpdated = DateTime.UtcNow;
        Transactions.Add(new Transaction
        {
            Type = TransactionType.Deposit,
            Amount = amount,
            FromAccount = Id,
            ToAccount = Id,
            DateTime = DateTime.UtcNow,
            BalanceAfterTransaction = Balance
        });
    }

    /// <summary>
    /// Withdraw specific amount from account balance
    /// </summary>
    /// <param name="amount"></param>
    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Withdraw must be higher than 0");
        }

        Balance -= amount;
        LastUpdated = DateTime.UtcNow;
        Transactions.Add(new Transaction
        {
            Type = TransactionType.Withdraw,
            Amount = amount,
            FromAccount = Id,
            ToAccount = Id,
            DateTime = DateTime.UtcNow,
            BalanceAfterTransaction = Balance
        });
    }

    /// <summary>
    /// Transfers from specific account to which account
    /// </summary>
    /// <param name="toAccount"></param>
    /// <param name="amount"></param>
    public void TransferTo(BankAccount toAccount, decimal amount)
    {
        // From which account
        Balance -= amount;
        LastUpdated = DateTime.Now;
        Transactions.Add(new Transaction
        {
            Type = TransactionType.TransferOut,
            Amount = amount,
            FromAccount = Id,
            ToAccount = toAccount.Id,
            DateTime = DateTime.UtcNow,
            BalanceAfterTransaction = Balance
        });
        
        // To which account
        toAccount.Balance += amount;
        toAccount.LastUpdated = DateTime.UtcNow;
        toAccount.Transactions.Add(new Transaction
        {
            Type = TransactionType.TransferIn,
            Amount = amount,
            FromAccount = Id,
            ToAccount = toAccount.Id,
            DateTime = DateTime.UtcNow,
            BalanceAfterTransaction = toAccount.Balance
        });
    }
}