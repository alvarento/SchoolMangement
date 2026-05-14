using SchoolManagement.Communication.ResponsesDto;

namespace SchoolManagement.WebApp.Services.UsuarioService
{
	public class UsuarioService : IUsuarioService
	{

		private readonly HttpClient _http;
		public UsuarioService(HttpClient http) => _http = http;
		public async Task<ResponseReadUsuarioDto> GetById(Guid id)
		{
			var response = await _http.GetAsync($"/usuarios/{id}");

			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadFromJsonAsync<ResponseReadUsuarioDto>();
			}

			// Se for 401 (Unauthorized) ou 403 (Forbidden), o token expirou ou é inválido
			return null;
		}
	}
}
