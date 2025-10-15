using BlazorStandaloneApp.Domain;

namespace BlazorStandaloneApp.Interfaces
{
    public interface ITransactionService
    {
        Task CreateTransaction(string fromAccountId, string toAccountId, decimal amount);
        Task<List<ITransaction>> GetTransactions();
    }
}