using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces
{
	public interface IMatriculaRepository
	{
		public Task Add(Matricula matri);

		public Task<Matricula?> GetMatriculaById(int id);

		public void Update(Matricula matri);

		public Task Delete(int id);
	}
}
