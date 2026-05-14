using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces.Repositories
{
	public interface IProfessorRepository
	{
		public Task Add(Professor prof);

		public Task<Professor?> GetProfessorById(int id);

		public Task<Professor?> GetProfessorByEmail(string email);

		public void Update(Professor prof);

		public Task Delete(int id);

		Task<int> CountAsync();
		Task<List<Professor>> GetPagedAsync(int pageNumber, int pageSize);
	}
}
