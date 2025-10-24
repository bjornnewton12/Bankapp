namespace BlazorStandaloneApp.Interfaces;

public interface IAccountService
{
    Task<BankAccount> CreateAccount(string name, AccountType accountType, CurrencyType currencyType, decimal initialBalance);
    Task<List<BankAccount>> GetAccounts();
    Task DeleteAccount(IBankAccount account);
    Task Transfer(Guid fromAccountId, Guid toAccountId, decimal amount);

    Task Withdraw(Guid accountId, decimal amount);
    Task Deposit(Guid accountId, decimal amount);
}
