namespace BlazorStandaloneApp.Interfaces;

/// <summary>
/// Defines methods for storing and retrieving data asynchronously, 
/// eg saving and loading objects from local or session storage.
/// </summary>

public interface IStorageService
{
    // Save
    Task SetItemAsync<T>(string key, T value);

    // Collect
    Task<T> GetItemAsync<T>(string key);
}