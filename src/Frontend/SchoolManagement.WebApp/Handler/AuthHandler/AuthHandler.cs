using System.Net.Http.Headers;
using SchoolManagement.WebApp.Services.LocalStorageService;

namespace SchoolManagement.WebApp.Handler.AuthHandler
{
    public class AuthHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;

        public AuthHandler(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
