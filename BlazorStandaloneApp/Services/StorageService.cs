using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.JSInterop;

namespace BlazorStandaloneApp.Services;

/// <summary>
/// Provides functionality for saving and retrieving data from the browser's local storage
/// using JavaScript interop and JSON serialization.
/// </summary>

public class StorageService : IStorageService
{
    private readonly IJSRuntime _jsRuntime;

    // Configure JSON serialization options
    private JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter() }
    };

    public StorageService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    // Save an object to local storage as JSON
    public async Task SetItemAsync<T>(string key, T value)
    {
        var json = JsonSerializer.Serialize(value, _jsonSerializerOptions);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, json);
        Console.WriteLine($"StorageService INFO: SetItemAsync completed for key.");
    }

    // Retrieve an object from local storage and deserialize it
    public async Task<T> GetItemAsync<T>(string key)
    {
        var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
        if (string.IsNullOrEmpty(json))
        {
            Console.WriteLine($"StorageService WARNING: No data found for key '{key}'.");
            return default!;
        }
        Console.WriteLine($"StorageService INFO: GetItemAsync collected for key.");
        return JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions)!;
    }
}