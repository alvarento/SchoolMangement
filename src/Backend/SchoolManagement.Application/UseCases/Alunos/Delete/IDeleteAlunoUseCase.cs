namespace SchoolManagement.Application.UseCases.Alunos.Delete
{
	public interface IDeleteAlunoUseCase
	{
		public Task Execute(int alunoId);
	}
}
