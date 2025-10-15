using System.Threading.Tasks;

namespace BlazorStandaloneApp.Services
{

    public class AccountService : IAccountService
    {
        private const string StorageKey = "BlazorStandaloneApp.accounts";
        private readonly List<IBankAccount> _accounts = new();
        private readonly IStorageService _storageService;
        private bool isLoaded;

        public AccountService(IStorageService storageService) => _storageService = storageService;

        private async Task IsInitialized()
        {
            if (isLoaded)
            {
                return;
            }
            var fromStorage = await _storageService.GetItemAsync<List<BankAccount>>(StorageKey);
            _accounts.Clear();
            if (fromStorage is { Count: > 0 })
                _accounts.AddRange(fromStorage);
            isLoaded = true;
        }

        private Task SaveAsync() => _storageService.SetItemAsync(StorageKey, _accounts);

        public async Task<IBankAccount> CreateAccount(string name, AccountType accountType, string currency, decimal initialBalance)
        {
            await IsInitialized();
            var account = new BankAccount(name, accountType, currency, initialBalance);
            _accounts.Add(account);
            await SaveAsync();
            return account;
        }

        public async Task<List<IBankAccount>> GetAccounts()
        {
            await IsInitialized();
            return _accounts.Cast<IBankAccount>().ToList();
        }

        public async Task DeleteAccount(IBankAccount account)
        {
            await IsInitialized();

            var accountToRemove = _accounts.FirstOrDefault(a => a.Id == account.Id);
            if (accountToRemove != null)
            {
                _accounts.Remove(accountToRemove);
                await SaveAsync();
            }
        }
        public async Task Transfer(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            await IsInitialized();

            var fromAccount = _accounts.FirstOrDefault(a => a.Id == fromAccountId) as BankAccount;
            var toAccount = _accounts.FirstOrDefault(a => a.Id == toAccountId) as BankAccount;

            if (fromAccount == null || toAccount == null)
                throw new Exception("One or both accounts could not be found.");

            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");

            if (fromAccount.Balance < amount)
                throw new InvalidOperationException("Insufficient funds in the source account.");
            
            fromAccount.AdjustBalance(-amount);
            toAccount.AdjustBalance(amount);

            await SaveAsync();
        }
    }
}
