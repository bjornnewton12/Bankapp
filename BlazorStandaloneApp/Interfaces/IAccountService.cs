namespace BlazorStandaloneApp.Interfaces;

/// <summary>
/// Defines the contract for managing bank accounts, including creating accounts, retrieving accounts,
/// and performing financial operations such as transfers, withdrawals, and deposits.
/// </summary>

public interface IAccountService
{
    Task<BankAccount> CreateAccount(string name, AccountType accountType, decimal initialBalance);
    Task<List<BankAccount>> GetAccounts();
    Task DeleteAccount(IBankAccount account);
    Task Transfer(Guid fromAccountId, Guid toAccountId, decimal amount);
    Task Withdraw(Guid accountId, decimal amount);
    Task Deposit(Guid accountId, decimal amount);
}
