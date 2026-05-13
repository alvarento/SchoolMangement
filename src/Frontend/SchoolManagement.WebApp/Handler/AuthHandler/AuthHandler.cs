namespace SchoolManagement.WebApp.Handler.AuthHandler
{
    using System.Net.Http.Headers;
    using Microsoft.AspNetCore.Http;

    public class AuthHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // LEITURA DIRETA NO SERVIDOR:
            // O navegador envia os cookies em cada requisição para o servidor Blazor.
            // O IHttpContextAccessor nos permite pegar esse valor sem usar JavaScript.
            var context = _httpContextAccessor.HttpContext;
            var token = context?.Request.Cookies["authToken"];

            if (!string.IsNullOrEmpty(token))
            {
                // Injeta o token na requisição que vai para a sua API
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}