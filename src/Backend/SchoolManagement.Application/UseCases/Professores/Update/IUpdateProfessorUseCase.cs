using SchoolManagement.Communication.RequestsDto;

namespace SchoolManagement.Application.UseCases.Professores.Update
{
	public interface IUpdateProfessorUseCase
	{
		public Task Execute(RequestUpdateProfessorDto request, int alunoId);
	}
}
