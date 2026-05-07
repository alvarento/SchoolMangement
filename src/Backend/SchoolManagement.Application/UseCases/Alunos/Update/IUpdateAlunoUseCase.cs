using SchoolManagement.Communication.RequestsDto;

namespace SchoolManagement.Application.UseCases.Alunos.Update
{
	public interface IUpdateAlunoUseCase
	{
		public Task Execute(RequestUpdateAlunoDto request, int alunoId);
	}
}
