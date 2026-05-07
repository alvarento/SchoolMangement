using SchoolManagement.Communication.ResponsesDto;

namespace SchoolManagement.Application.UseCases.Professores.Read
{
	public interface IReadProfessorUseCase
	{
		public Task<ResponseReadProfessorDto> Execute(int professorId);
	}
}
