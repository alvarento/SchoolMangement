using SchoolManagement.Communication.ResponsesDto;

namespace SchoolManagement.Application.UseCases.Alunos.ReadAll
{
	public interface IReadAllAlunosUseCase
	{
		public Task<PagedResponse<ResponseReadAlunoDto>> Execute(int pageNumber, int pagesize);
	}
}
