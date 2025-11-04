namespace BlazorStandaloneApp.Services

/// <summary>
/// Provides methods for managing bank accounts, including creation, deletion, deposits, withdrawals, and transfers.
/// Handles loading and saving account data using the IStorageService.
/// </summary>

{
        public class AccountService : IAccountService
    {
        private const string StorageKey = "BlazorStandaloneApp.accounts";
        private readonly List<BankAccount> _accounts = new();
        private readonly IStorageService _storageService;
        private bool isLoaded;
        public AccountService(IStorageService storageService) => _storageService = storageService;

        // Ensure account data is loaded from storage before performing operations
        private async Task IsInitialized()
        {
            if (isLoaded)
            {
                return;
            }
            Console.WriteLine("AccountService INFO: Initializing accounts from storage.");

            var fromStorage = await _storageService.GetItemAsync<List<BankAccount>>(StorageKey);
            _accounts.Clear();

            if (fromStorage is { Count: > 0 })
                _accounts.AddRange(fromStorage);
            isLoaded = true;
            Console.WriteLine("AccountService INFO: Loaded accounts from storage.");
        }

        private Task SaveAsync() => _storageService.SetItemAsync(StorageKey, _accounts);

        // Create a new bank account and save it to storage
        public async Task<BankAccount> CreateAccount(string name, AccountType accountType, decimal initialBalance)
        {
            await IsInitialized();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("AccountService ERROR: Attempted to create account with empty or null name.");
                throw new ArgumentException("Account name cannot be empty.");
            }

            var account = new BankAccount(name, accountType, initialBalance);
            _accounts.Add(account);
            await SaveAsync();
            Console.WriteLine($"AccountService INFO: Account created with initial balance {initialBalance}.");
            return account;
        }

        // Retrieve all bank accounts
        public async Task<List<BankAccount>> GetAccounts()
        {
            await IsInitialized();
            Console.WriteLine("AccountService INFO: Accounts collected.");
            return _accounts.Cast<BankAccount>().ToList();
        }

        // Delete a specific bank account
        public async Task DeleteAccount(IBankAccount account)
        {
            await IsInitialized();

            var accountToRemove = _accounts.FirstOrDefault(a => a.Id == account.Id);
            if (accountToRemove != null)
            {
                _accounts.Remove(accountToRemove);
                await SaveAsync();
                Console.WriteLine($"AccountService INFO: Account removed.");
            }
        }

        // Transfer between two accounts
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
            Console.WriteLine($"AccountService INFO: Transfer completed {amount} kr from account {fromAccountId} to {toAccountId}.");
        }

        // Withdraw from specific account
        public async Task Withdraw(Guid accountId, decimal amount, ExpenseCategory category)
        {
            await IsInitialized();
            var account = _accounts.FirstOrDefault(z => z.Id == accountId);

            if (account == null)
            {
                Console.WriteLine("AccountService ERROR: ArgumentException because Account == null.");
                throw new ArgumentException("Select an account");
            }

            account.Withdraw(amount, category);

            var allTransactions = _accounts.SelectMany(z => z.GetTransactions()).ToList();
            await _storageService.SetItemAsync("transactions", allTransactions);

            await SaveAsync();
            Console.WriteLine("AccountService INFO: Amount withdrawn.");
        }
        
        // Deposit from specific account
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
            Console.WriteLine("AccountService INFO: Amount deposited.");
        }
    }
}
