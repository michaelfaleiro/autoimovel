using System;
using Microsoft.JSInterop;
using Blazored.LocalStorage;

namespace AutoImovel.Dashboard.Services
{
    public class ThemeService
    {
        private readonly ILocalStorageService _storage;
        private readonly IJSRuntime _js;
        private bool _isDark;

        public event Action OnThemeChanged;
        public bool IsDark 
        { 
            get => _isDark; 
            private set 
            { 
                if (_isDark != value)
                {
                    _isDark = value;
                    OnThemeChanged?.Invoke();
                }
            } 
        }

        public ThemeService(ILocalStorageService storage, IJSRuntime js)
        {
            _storage = storage;
            _js = js;
        }

        public async Task InitAsync()
        {
            _isDark = await _storage.GetItemAsync<bool>("autoimovel-dashboard-theme");
            // Default to dark if not set, as it's the primary brand feel
            if (_isDark == false && (await _storage.GetItemAsync<bool?>("autoimovel-dashboard-theme") == null))
            {
                _isDark = true;
            }
            await ApplyThemeAsync();
        }

        public async Task ToggleThemeAsync()
        {
            IsDark = !IsDark;
            await _storage.SetItemAsync("autoimovel-dashboard-theme", IsDark);
            await ApplyThemeAsync();
        }

        private async Task ApplyThemeAsync()
        {
            if (_isDark)
                await _js.InvokeVoidAsync("document.documentElement.classList.add", "dark");
            else
                await _js.InvokeVoidAsync("document.documentElement.classList.remove", "dark");
        }
    }
}
