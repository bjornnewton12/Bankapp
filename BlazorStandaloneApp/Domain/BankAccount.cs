using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BlazorStandaloneApp.Interfaces;

namespace BlazorStandaloneApp.Domain;

public class BankAccount : IBankAccount
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public AccountType AccountType { get; private set; }
    public CurrencyType CurrencyType { get; private set; }

    public string Name { get; private set; }


    public decimal Balance { get; private set; }

    public DateTime LastUpdated { get; private set; }

    private readonly List<Transaction> _transaction = new();

    public IReadOnlyList<Transaction> Transactions => _transaction.AsReadOnly();

    // Chat: Added this list

    public List<Transaction> GetTransactions()
    {
        return _transaction;
    }

    public BankAccount(string name, AccountType accountType, CurrencyType currencyType, decimal balance)
    {
        Name = name;
        AccountType = accountType;
        CurrencyType = currencyType;
        Balance = balance;
        LastUpdated = DateTime.Now;
    }

    [JsonConstructor]
    public BankAccount(Guid id, string name, AccountType accountType, CurrencyType currencyType, decimal balance, DateTime lastUpdated)
    {
        Id = id;
        Name = name;
        AccountType = accountType;
        CurrencyType = currencyType;
        Balance = balance;
        LastUpdated = lastUpdated;
    }

    public void Deposit(decimal amount)
    {
        throw new NotImplementedException();
    }

    public void Withdraw(decimal amount)
    {
        throw new NotImplementedException();
    }

    public void TransferTo(BankAccount toAccount, decimal amount)
    {
        // Från vilket konto
        Balance -= amount;
        LastUpdated = DateTime.Now;
        _transaction.Add(new Transaction
        {
            Type = TransactionType.TransferOut,
            Amount = amount,
            FromAccount = Id,
            ToAccount = toAccount.Id,
            DateTime = DateTime.UtcNow,
            BalanceAfterTransaction = Balance
        });
        // till vilket konto
        toAccount.Balance += amount;
        toAccount.LastUpdated = DateTime.UtcNow;
        toAccount._transaction.Add(new Transaction
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