using BlazorStandaloneApp.Domain;
using BlazorStandaloneApp.Interfaces;

namespace BlazorStandaloneApp.Services;

/// <summary>
/// Provides methods for handling user authentication, including login, logout, and retrieving the current user.
/// Manages authentication state and user data using the IStorageService.
/// </summary>

public class AuthenticationService : IAuthenticationService
{
    private readonly IStorageService _storageService;
    private string UserKey = "CurrentUser";
    private string LoggedInKey = "isLoggedIn";
    private readonly User _defaultUser = new("arber", "27237");
    public event Action? OnAuthStateChanged;

    // Initialize the authentication service and log that it has started
    public AuthenticationService(IStorageService storageService)
    {
        _storageService = storageService;
        Console.WriteLine("AuthenticationService INFO: AuthenticationService initialized.");
    }

    // Attempt to log in using provided credentials and store user data if successful
    public async Task<bool> LoginAsync(string username, string pin)
    {
        Console.WriteLine($"AuthenticationService INFO: Attempting login user '{username}'.");
        if (username == _defaultUser.Username && pin == _defaultUser.Pin)
        {
            await _storageService.SetItemAsync(UserKey, _defaultUser);
            await _storageService.SetItemAsync(LoggedInKey, true);
            Console.WriteLine($"AuthenticationService INFO: '{username}' logged in successfully.");
            OnAuthStateChanged?.Invoke();
            return true;
        }
        Console.WriteLine("AuthenticationService INFO: Login failed. Invalid username or PIN.");
        return false;
    }

    // Log out the current user and clear stored authentication data
    public async Task LogoutAsync()
    {
        Console.WriteLine("AuthenticationService INFO: Logging out user.");
        await _storageService.SetItemAsync<User?>(UserKey, null);
        await _storageService.SetItemAsync(LoggedInKey, false);
        OnAuthStateChanged?.Invoke();
    }

    // Check if a user is currently logged in based on stored authentication state
    public async Task<bool> IsLoggedInAsync()
    {
        return await _storageService.GetItemAsync<bool>(LoggedInKey);
    }
    
    // Retrieve the currently logged-in user's data from storage
    public async Task<User?> GetUserAsync()
    {
        return await _storageService.GetItemAsync<User>(UserKey);
    }
}