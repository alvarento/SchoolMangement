using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Communication.ResponsesDto;

namespace SchoolManagement.WebApp.Services.AlunoService
{
    public class AlunoService : IAlunoService
    {
       
            private readonly HttpClient _http;
            public AlunoService(HttpClient http) => _http = http;
            public async Task<PagedResponse<ResponseReadAlunoDto>> GetAll() => await _http.GetFromJsonAsync<PagedResponse<ResponseReadAlunoDto>>("/alunos");

		public async Task<bool> Delete(int id)
		{
			var response = await _http.DeleteAsync($"/alunos/{id}");
			return response.IsSuccessStatusCode;
		}

        public async Task<bool> Create(RequestCreateAlunoDto aluno)
        {
            var response = await _http.PostAsJsonAsync("/alunos", aluno);
            return response.IsSuccessStatusCode;
        }

		public async Task<bool> Update(int id, RequestUpdateAlunoDto aluno)
		{
			var response = await _http.PutAsJsonAsync($"/alunos/{id}", aluno);
			return response.IsSuccessStatusCode;
		}

	}
}
