using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using System.Text.Json;

namespace LocalStorage.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        // GetItemAsync method invokes the bweInterop.getLocalStorage JS function with a key. If bweInterop.getLocalStorage returns a value,
        // that value is deserialized and returned
        public async Task<T> GetItemAsync<T>(string key)
        {
            var json = await js.InvokeAsync<string>(
                "bweInterop.getLocalStorage",
                key);
            return string.IsNullOrEmpty(json)
                ? default
                : JsonSerializer.Deserialize<T>(json);
        }
        // SetItemAsync method invokes the bweInterop.setLocalStorage JS function with a key and a serialized
        // version of the item to be storaged in localStorage
        public async Task SetItemAsync<T>(string key, T item)
        {
            await js.InvokeVoidAsync("bweInterop.setLocalStorage", key, JsonSerializer.Serialize(item));
        }

        private IJSRuntime js;
        public LocalStorageService(IJSRuntime JsRuntime)
        {
            js = JsRuntime;
        }
    }
}
