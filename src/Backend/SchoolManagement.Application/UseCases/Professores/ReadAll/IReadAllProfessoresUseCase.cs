using SchoolManagement.Communication.ResponsesDto;

namespace SchoolManagement.Application.UseCases.Professores.ReadAll
{
	public interface IReadAllProfessoresUseCase
	{
		public Task<PagedResponse<ResponseReadProfessorDto>> Execute(int pageNumber, int pagesize);
	}
}
