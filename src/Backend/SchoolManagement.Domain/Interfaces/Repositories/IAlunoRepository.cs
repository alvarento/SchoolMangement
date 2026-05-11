using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces.Repositories
{
	public interface IAlunoRepository
	{
		public Task Add(Aluno aluno);

		public Task<Aluno?> GetAlunoById(int id);

		public Task<Aluno?> GetAlunoByEmail(string email);

		public void Update(Aluno aluno);

		public Task Delete(int id);

		Task<int> CountAsync();
		Task<List<Aluno>> GetPagedAsync(int pageNumber, int pageSize);
	}
}
