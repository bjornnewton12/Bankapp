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
            Console.WriteLine("AccountService: Initializing accounts from storage.");

            var fromStorage = await _storageService.GetItemAsync<List<BankAccount>>(StorageKey);
            _accounts.Clear();

            if (fromStorage is { Count: > 0 })
                _accounts.AddRange(fromStorage);
            isLoaded = true;
            Console.WriteLine("AccountService: Loaded accounts from storage.");
        }

        private Task SaveAsync() => _storageService.SetItemAsync(StorageKey, _accounts);

        public async Task<BankAccount> CreateAccount(string name, AccountType accountType, CurrencyType currencyType, decimal initialBalance)
        {
            await IsInitialized();
            var account = new BankAccount(name, accountType, currencyType, initialBalance);
            _accounts.Add(account);
            await SaveAsync();
            Console.WriteLine($"AccountService: Account {name} created with initial balance {initialBalance}.");
            return account;
        }

        public async Task<List<BankAccount>> GetAccounts()
        {
            await IsInitialized();
            Console.WriteLine("AccountService: Accounts collected.");
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
                Console.WriteLine($"AccountService: Account removed.");
            }
        }

        public async Task Transfer(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var fromStorage = await _storageService.GetItemAsync<List<BankAccount>>(StorageKey);
            var fromAccount = _accounts.FirstOrDefault(x => x.Id == fromAccountId);
            var toAccount = _accounts.FirstOrDefault(y => y.Id == toAccountId);

            fromAccount!.TransferTo(toAccount!, amount);

            var allTransactions = _accounts
            .SelectMany(a => a.GetTransactions())
            .ToList();

            await _storageService.SetItemAsync(StorageKey, _accounts);
            Console.WriteLine($"AccountService: Transfer completed {amount} kr from account {fromAccountId} to {toAccountId}.");
        }

        public async Task Withdraw(Guid accountId, decimal amount)
        {
            await IsInitialized();
            var account = _accounts.FirstOrDefault(z => z.Id == accountId);

            if (account == null)
            {
                Console.WriteLine("AccountService ERROR: ArgumentException because Account == null.");
                throw new ArgumentException("Select an account");
            }

            account.Withdraw(amount);

            var allTransactions = _accounts.SelectMany(z => z.GetTransactions()).ToList();
            await _storageService.SetItemAsync("transactions", allTransactions);

            await SaveAsync();
            Console.WriteLine("AccountService: Amount withdrawn.");
        }
        
        public async Task Deposit(Guid accountId, decimal amount)
        {
            await IsInitialized();
            var account = _accounts.FirstOrDefault(z => z.Id == accountId);

            if (account == null)
            {
                Console.WriteLine("AccountService ERROR: ArgumentException because Account == null.");
                throw new ArgumentException("Select an account");
            }

            account.Deposit(amount);

            var allTransactions = _accounts.SelectMany(z => z.GetTransactions()).ToList();
            await _storageService.SetItemAsync("transactions", allTransactions);

            await SaveAsync();
            Console.WriteLine("AccountService: Amount deposited.");
        }
    }
}
