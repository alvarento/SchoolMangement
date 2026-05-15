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

						return null;
		}
	}
}
