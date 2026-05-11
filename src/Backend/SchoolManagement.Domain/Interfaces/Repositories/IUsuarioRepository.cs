using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces.Repositories
{
	public interface IUsuarioRepository
	{
		public Task Add(Usuario usuario);

		public Task<Usuario?> GetById(Guid id);

		public Task<bool> ExistisUsuarioWithId(Guid id);

		public Task<Usuario?> GetByEmail(string email);

		public Task<Usuario?> GetByEmailAndPassword(string email, string password);

		public Task<bool> ExistsEmail(string email);

		public void Update(Usuario usuario);

		public Task Delete(Guid id);

		Task<int> CountAsync();
		Task<List<Usuario>> GetPagedAsync(int pageNumber, int pageSize);
	}
}
