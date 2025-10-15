namespace BlazorStandaloneApp.Interfaces;

public interface IAccountService
{
    Task<IBankAccount> CreateAccount(string name, AccountType accountType, string currency, decimal initialBalance);
    Task<List<IBankAccount>> GetAccounts();
    Task DeleteAccount(IBankAccount account);
    Task Transfer(Guid fromAccountId, Guid toAccountId, decimal amount);
}
