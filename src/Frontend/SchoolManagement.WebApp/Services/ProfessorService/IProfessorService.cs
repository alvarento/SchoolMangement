using SchoolManagement.Communication.RequestsDto;
using SchoolManagement.Communication.ResponsesDto;

namespace SchoolManagement.WebApp.Services.ProfessorService
{
	public interface IProfessorService
	{
		Task<PagedResponse<ResponseReadProfessorDto>> GetAll();
		Task<int?> GetTotal();
		Task<bool> Create(RequestCreateProfessorDto professor);
		Task<bool> Update(int id, RequestUpdateProfessorDto professor);
		Task<bool> Delete(int id);
	}
}