using SchoolManagement.Communication.ResponsesDto;

namespace SchoolManagement.WebApp.Services.AlunoService
{
    public class AlunoService : IAlunoService
    {
       
            private readonly HttpClient _http;
            public AlunoService(HttpClient http) => _http = http;
            public async Task<PagedResponse<ResponseReadAlunoDto>> GetAlunos() => await _http.GetFromJsonAsync<PagedResponse<ResponseReadAlunoDto>>("/alunos");

		public async Task<bool> Delete(int id)
		{
			var response = await _http.DeleteAsync($"/alunos/{id}");
			return response.IsSuccessStatusCode;
		}

	}
}
