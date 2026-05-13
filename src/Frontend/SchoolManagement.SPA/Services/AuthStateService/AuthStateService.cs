using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using SchoolManagement.WebApp.Services.LocalStorageService;

namespace SchoolManagement.WebApp.Services.AuthStateService
{
	public class AuthStateService :
		AuthenticationStateProvider,
		IAuthStateService
	{
		private readonly ILocalStorageService _localStorage;
		private readonly ITokenProvider _tokenProvider;
		private bool _initialized;


		public ClaimsPrincipal CurrentUser { get; private set; }
			= new(new ClaimsIdentity());

		public AuthStateService(ILocalStorageService localStorage, ITokenProvider tokenProvider)
		{
			_localStorage = localStorage;
			_tokenProvider = tokenProvider;
		}

		public async Task InitializeAsync()
		{

			if (_initialized) return;

			try
			{
				_tokenProvider.Token = await _localStorage.GetItemAsync<string>("authToken");

				if (string.IsNullOrWhiteSpace(_tokenProvider.Token))
				{
					CurrentUser = new ClaimsPrincipal(
					  new ClaimsIdentity());

					NotifyAuthenticationStateChanged(
						Task.FromResult(
							new AuthenticationState(CurrentUser)));

					_initialized = true;
					return;
				}

				var identity = new ClaimsIdentity(
					ParseClaimsFromJwt(_tokenProvider.Token),
					"jwt");

				CurrentUser = new ClaimsPrincipal(identity);

				NotifyAuthenticationStateChanged(
					Task.FromResult(
						new AuthenticationState(CurrentUser)));

				_initialized = true;
			}
			catch
			{
				CurrentUser = new ClaimsPrincipal(
	new ClaimsIdentity());

				NotifyAuthenticationStateChanged(
					Task.FromResult(
						new AuthenticationState(CurrentUser)));

				_initialized = true;
			}
		}

		public async Task LoginAsync(string token)
		{
			_tokenProvider.Token = token;

			await _localStorage.SetItemAsync("authToken", token);

			var identity = new ClaimsIdentity(
				ParseClaimsFromJwt(token),
				"jwt");

			CurrentUser = new ClaimsPrincipal(identity);

			NotifyAuthenticationStateChanged(
				Task.FromResult(
					new AuthenticationState(CurrentUser)));
		}

		public async Task LogoutAsync()
		{

			_initialized = false;
			_tokenProvider.Token = null;

			await _localStorage.RemoveItemAsync("authToken");

			CurrentUser = new ClaimsPrincipal(
				new ClaimsIdentity());

			NotifyAuthenticationStateChanged(
				Task.FromResult(
					new AuthenticationState(CurrentUser)));
		}

		public Task<string?> GetTokenAsync()
		{
			return Task.FromResult(_tokenProvider.Token);
		}

		public override Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			return Task.FromResult(
				new AuthenticationState(CurrentUser));
		}

		private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
		{
			var claims = new List<Claim>();

			var payload = jwt.Split('.')[1];

			var jsonBytes = ParseBase64WithoutPadding(payload);

			var keyValuePairs =
				JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

			if (keyValuePairs is null)
				return claims;

			foreach (var kvp in keyValuePairs)
			{
				claims.Add(
					new Claim(kvp.Key, kvp.Value?.ToString() ?? ""));
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