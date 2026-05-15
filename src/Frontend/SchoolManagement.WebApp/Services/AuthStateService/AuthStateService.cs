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
                        await _jsRuntime.InvokeVoidAsync("setCookie", "authToken", token, 60); 
            SetAsLoggedIn(token);
        }

        public async Task LogoutAsync()
        {
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

		public Guid? GetUserId()
		{
												var claim = CurrentUser.FindFirst(ClaimTypes.Sid) ??
						CurrentUser.FindFirst("sid");

			if (claim != null && Guid.TryParse(claim.Value, out var guidId))
			{
				return guidId;
			}

			return null;
		}

		
		public override Task<AuthenticationState> GetAuthenticationStateAsync() => Task.FromResult(new AuthenticationState(CurrentUser));
        public Task<string?> GetTokenAsync() => Task.FromResult(_tokenProvider.Token);


		private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
		{
			var claims = new List<Claim>();
			var payload = jwt.Split('.')[1];
			var jsonBytes = ParseBase64WithoutPadding(payload);
			var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

			if (keyValuePairs is null) return claims;

			foreach (var kvp in keyValuePairs)
			{
				var key = kvp.Key;
				var value = kvp.Value?.ToString() ?? "";

												if (key == "sid")
				{
					claims.Add(new Claim(ClaimTypes.Sid, value));
				}
				else
				{
					claims.Add(new Claim(key, value));
				}
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