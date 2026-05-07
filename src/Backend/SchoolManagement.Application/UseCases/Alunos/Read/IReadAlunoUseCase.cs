using SchoolManagement.Communication.ResponsesDto;

namespace SchoolManagement.Application.UseCases.Alunos.Read
{
	public interface IReadAlunoUseCase
	{
		public Task<ResponseReadAlunoDto> Execute(int alunoId);
	}
}
