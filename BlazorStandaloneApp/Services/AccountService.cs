namespace BlazorStandaloneApp.Services
{
        public class AccountService : IAccountService
    {
        private const string StorageKey = "BlazorStandaloneApp.accounts";
        private readonly List<BankAccount> _accounts = new();
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

        public async Task<BankAccount> CreateAccount(string name, AccountType accountType, CurrencyType currencyType, decimal initialBalance)
        {
            await IsInitialized();
            var account = new BankAccount(name, accountType, currencyType, initialBalance);
            _accounts.Add(account);
            await SaveAsync();
            return account;
        }

        public async Task<List<BankAccount>> GetAccounts()
        {
            await IsInitialized();
            return _accounts.Cast<BankAccount>().ToList();
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
            // Från Christopher
            var fromStorage = await _storageService.GetItemAsync<List<BankAccount>>(StorageKey);

            var fromAccount = _accounts.FirstOrDefault(x => x.Id == fromAccountId);
            var toAccount = _accounts.FirstOrDefault(y => y.Id == toAccountId);


            fromAccount.TransferTo(toAccount, amount); // Vad ska jag göra här?

            var allTransactions = _accounts
            .SelectMany(a => a.GetTransactions())
            .ToList();

            // Från Christopher
            await _storageService.SetItemAsync(StorageKey, _accounts);
        }

        public async Task Withdraw(Guid accountId, decimal amount)
        {
            await IsInitialized();
            var account = _accounts.FirstOrDefault(z => z.Id == accountId);

            account.Withdraw(amount); // Vad ska jag göra här?

            var allTransactions = _accounts.SelectMany(z => z.GetTransactions()).ToList();
            await _storageService.SetItemAsync("transactions", allTransactions);

            await SaveAsync();
        }
        
        public async Task Deposit(Guid accountId, decimal amount)
        {
            await IsInitialized();
            var account = _accounts.FirstOrDefault(z => z.Id == accountId);

            account.Deposit(amount); // Vad ska jag göra här?

            var allTransactions = _accounts.SelectMany(z => z.GetTransactions()).ToList();
            await _storageService.SetItemAsync("transactions", allTransactions);

            await SaveAsync();
        }
    }
}
