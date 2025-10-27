namespace BlazorStandaloneApp.Interfaces;

public interface IStorageService
{
    // Save
    Task SetItemAsync<T>(string key, T value);

    // Collect
    Task<T> GetItemAsync<T>(string key);
}