using System.Security.Claims;
using System.Text.Json; // Necessário para o JWT
using Microsoft.AspNetCore.Components.Authorization;
using SchoolManagement.WebApp.Services.LocalStorageService;

namespace SchoolManagement.WebApp.Services.AuthStateService
{
	public class AuthStateService : AuthenticationStateProvider
	{
		private readonly ILocalStorageService _localStorage;

		// Remova o HttpClient daqui. O AuthHandler cuidará do Header de forma mais segura.
		public AuthStateService(ILocalStorageService localStorage)
		{
			_localStorage = localStorage;
		}

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			try
			{
				var token = await _localStorage.GetItemAsync<string>("authToken");

				if (string.IsNullOrWhiteSpace(token))
				{
					return CreateAnonymous();
				}

				var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
				var user = new ClaimsPrincipal(identity);

				return new AuthenticationState(user);
			}
			catch
			{
				return CreateAnonymous();
			}
		}

		public void NotifyLogin(string token)
		{
			var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
			var user = new ClaimsPrincipal(identity);
			var authState = Task.FromResult(new AuthenticationState(user));
			NotifyAuthenticationStateChanged(authState);
		}

		public void NotifyLogout()
		{
			var anonymous = CreateAnonymous();
			NotifyAuthenticationStateChanged(Task.FromResult(anonymous));
		}

		private AuthenticationState CreateAnonymous() =>
			new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

		private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
		{
			var claims = new List<Claim>();
			var payload = jwt.Split('.')[1];
			var jsonBytes = ParseBase64WithoutPadding(payload);
			var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

			if (keyValuePairs != null)
			{
				foreach (var kvp in keyValuePairs)
				{
					claims.Add(new Claim(kvp.Key, kvp.Value.ToString() ?? ""));
				}
			}
			return claims;
		}

		private byte[] ParseBase64WithoutPadding(string base64)
		{
			switch (base64.Length % 4)
			{
				case 2: base64 += "=="; break;
				case 3: base64 += "="; break;
			}
			return Convert.FromBase64String(base64);
		}
	}
}