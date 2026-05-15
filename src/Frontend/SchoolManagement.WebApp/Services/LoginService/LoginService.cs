using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Communication.ResponsesDto;
using SchoolManagement.WebApp.Services.AuthStateService;

namespace SchoolManagement.WebApp.Services.LoginService
{
	public class LoginService : ILoginService
	{
		private readonly HttpClient _http;

		private readonly IAuthStateService _authState;

		public LoginService(
			HttpClient http,
			IAuthStateService authState)
		{
			_http = http;
			_authState = authState;
		}

		public async Task<ResponseLoginDto?> Login(
			RequestLoginDto loginDto)
		{
			try
			{
				var response = await _http.PostAsJsonAsync(
					"/login",
					loginDto);

				if (!response.IsSuccessStatusCode)
					return null;

				var result =
					await response.Content.ReadFromJsonAsync<ResponseLoginDto>();

				if (!string.IsNullOrWhiteSpace(
					result?.Tokens?.AcessToken))
				{
					await _authState.LoginAsync(
						result.Tokens.AcessToken);
				}

				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine(
					$"Erro ao tentar fazer login: {ex.Message}");

				return null;
			}
		}

		public async Task<bool> RegisterAsync(
			RequestCreateUsuarioDto usuarioDto)
		{
			try
			{
				usuarioDto.IsAdmin = true;
				var response = await _http.PostAsJsonAsync(
					"/usuarios",
					usuarioDto);

				return response.IsSuccessStatusCode;
			}
			catch (Exception ex)
			{
				Console.WriteLine(
					$"Erro ao registrar: {ex.Message}");

				return false;
			}
		}
	}
}