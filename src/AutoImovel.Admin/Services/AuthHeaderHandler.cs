using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace AutoImovel.Admin.Services;

public class AuthHeaderHandler : DelegatingHandler
{
    private readonly ILocalStorageService _storage;

    public AuthHeaderHandler(ILocalStorageService storage)
    {
        _storage = storage;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _storage.GetItemAsync<string>("autoimovel-admin-token");
        if (!string.IsNullOrEmpty(token))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return await base.SendAsync(request, cancellationToken);
    }
}
