using BlazorStandaloneApp.Domain;
using BlazorStandaloneApp.Interfaces;

namespace BlazorStandaloneApp.Services
{
    public class TransactionService : ITransactionService
    {

        private readonly IAccountService _accountService;
        private readonly List<Transaction> _transactions = new();

        public TransactionService(IAccountService accountService)
        {
            _accountService = accountService;
        }


        

        public Task<List<ITransaction>> GetTransactions()
        {
            return Task.FromResult(_transactions.Cast<ITransaction>().ToList());
        }

        public async Task CreateTransaction(string fromAccountId, string toAccountId, decimal amount)
        {
            var accounts = await _accountService.GetAccounts();

            Guid fromId = Guid.Parse(fromAccountId);
            Guid toId = Guid.Parse(toAccountId);
    
            var fromAccount = accounts.FirstOrDefault(a => a.Id == fromId);
            var toAccount = accounts.FirstOrDefault(a => a.Id == toId);

            if (fromAccount == null || toAccount == null)
            {
                throw new Exception("One or both accounts could not be found.");
            }

            await _accountService.Transfer(fromId, toId, amount);
            
            accounts = await _accountService.GetAccounts();
            fromAccount = accounts.First(a => a.Id == fromId);
            toAccount   = accounts.First(a => a.Id == toId);
            
            var transaction = new Transaction
            {
                FromAccountId = fromAccountId,
                ToAccountId = toAccountId,
                FromAccountName = fromAccount?.Name ?? "Unknown",
                ToAccountName = toAccount?.Name ?? "Unknown",
                Amount = amount,
                Currency = fromAccount?.Currency ?? "SEK",
                LastUpdated = DateTime.UtcNow
            };

            _transactions.Add(transaction);

        }
    }
}