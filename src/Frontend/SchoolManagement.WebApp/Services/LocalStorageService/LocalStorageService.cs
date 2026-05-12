using Microsoft.JSInterop;
using System.Text.Json;


namespace SchoolManagement.WebApp.Services.LocalStorageService
{
    public class LocalStorageService : ILocalStorageService
    {
        private readonly IJSRuntime _jsRuntime;

        public LocalStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task SetItemAsync<T>(string key, T value)
        {
            object valueToSave = value is string str ? str : JsonSerializer.Serialize(value);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, valueToSave);
        }

        public async Task<T?> GetItemAsync<T>(string key)
        {
            try
            {
                var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);


                if (string.IsNullOrEmpty(json))
                    return default;

                if (typeof(T) == typeof(string))
                    return (T)(object)json;

                return JsonSerializer.Deserialize<T>(json);
            }
            catch (InvalidOperationException)
            {
                return default;
            }
        }

        public async Task RemoveItemAsync(string key)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }
    }
}
