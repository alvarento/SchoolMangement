using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces
{
	public interface ITurmaRepository
	{
		public Task Add(Turma turma);

		public Task<Turma?> GetTurmaById(int id);

		public void Update(Turma turma);

		public Task Delete(int id);
	}
}
