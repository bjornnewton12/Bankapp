namespace BlazorStandaloneApp.Interfaces;

public interface IAccountService
{
    Task<BankAccount> CreateAccount(string name, AccountType accountType, CurrencyType currencyType, decimal initialBalance);
    Task<List<BankAccount>> GetAccounts();
    Task DeleteAccount(IBankAccount account);
    void Transfer(Guid fromAccountId, Guid toAccountId, decimal amount);
    
}