using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Communication.ResponsesDto;
using System.Net.Http.Json;

namespace SchoolManagement.WebApp.Services.ProfessorService
{
	public class ProfessorService : IProfessorService
	{
		private readonly HttpClient _http;
		public ProfessorService(HttpClient http) => _http = http;

		public async Task<PagedResponse<ResponseReadProfessorDto>> GetAll()
		{
			var response = await _http.GetAsync("/professores");
			return response.IsSuccessStatusCode
				? await response.Content.ReadFromJsonAsync<PagedResponse<ResponseReadProfessorDto>>()
				: null;
		}

		public async Task<int?> GetTotal()
		{
			var response = await _http.GetAsync("/professores/count");
			return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<int?>() : null;
		}

		public async Task<bool> Delete(int id)
		{
			var response = await _http.DeleteAsync($"/professores/{id}");
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> Create(RequestCreateProfessorDto professor)
		{
			var response = await _http.PostAsJsonAsync("/professores", professor);
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> Update(int id, RequestUpdateProfessorDto professor)
		{
			var response = await _http.PutAsJsonAsync($"/professores/{id}", professor);
			return response.IsSuccessStatusCode;
		}
	}
}