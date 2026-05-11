using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces.Repositories
{
	public interface IDisciplinaRepository
	{
		public Task Add(Disciplina discip);

		public Task<Disciplina?> GetDisciplinaById(int id);

		public void Update(Disciplina discip);

		public Task Delete(int id);
	}
}
