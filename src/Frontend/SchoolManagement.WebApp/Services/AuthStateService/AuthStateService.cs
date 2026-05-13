using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace SchoolManagement.WebApp.Services.AuthStateService
{
    public class AuthStateService : AuthenticationStateProvider, IAuthStateService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenProvider _tokenProvider;
        private readonly IJSRuntime _jsRuntime;
        private bool _initialized;

        public ClaimsPrincipal CurrentUser { get; private set; } = new(new ClaimsIdentity());

        public AuthStateService(IHttpContextAccessor httpContextAccessor, ITokenProvider tokenProvider, IJSRuntime jsRuntime)
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenProvider = tokenProvider;
            _jsRuntime = jsRuntime;
        }

        public async Task InitializeAsync()
        {
            if (_initialized) return;

            // Diferente do LocalStorage, aqui lemos o Cookie que já veio na requisição HTTP
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["authToken"];

            if (string.IsNullOrWhiteSpace(token))
            {
                SetAsLoggedOut();
            }
            else
            {
                SetAsLoggedIn(token);
            }

            _initialized = true;
        }

        public async Task LoginAsync(string token)
        {
            // 1. Salva o cookie no navegador via JS (precisamos do JS aqui pois o login é uma ação do cliente)
            await _jsRuntime.InvokeVoidAsync("setCookie", "authToken", token, 60); // Função JS que criamos antes

            SetAsLoggedIn(token);
        }

        public async Task LogoutAsync()
        {
            // 1. Remove o cookie via JS
            await _jsRuntime.InvokeVoidAsync("setCookie", "authToken", "", -1);

            SetAsLoggedOut();
        }

        private void SetAsLoggedIn(string token)
        {
            _tokenProvider.Token = token;
            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            CurrentUser = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(CurrentUser)));
        }

        private void SetAsLoggedOut()
        {
            _tokenProvider.Token = null;
            CurrentUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(CurrentUser)));
        }

        // ... Mantenha os métodos ParseClaimsFromJwt e ParseBase64WithoutPadding como estão ...

        public override Task<AuthenticationState> GetAuthenticationStateAsync() => Task.FromResult(new AuthenticationState(CurrentUser));
        public Task<string?> GetTokenAsync() => Task.FromResult(_tokenProvider.Token);


        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();

            // Pega apenas a parte central do Token (o Payload)
            var payload = jwt.Split('.')[1];

            // Converte de Base64 para Bytes
            var jsonBytes = ParseBase64WithoutPadding(payload);

            // Transforma em um dicionário (Chave e Valor)
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            if (keyValuePairs is null)
                return claims;

            // Transforma cada item do dicionário em uma Claim do C#
            foreach (var kvp in keyValuePairs)
            {
                claims.Add(new Claim(kvp.Key, kvp.Value?.ToString() ?? ""));
            }

            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2:
                    base64 += "==";
                    break;

                case 3:
                    base64 += "=";
                    break;
            }

            return Convert.FromBase64String(base64);
        }
    }
}