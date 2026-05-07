using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Domain.Interfaces
{
	public interface IPessoaRepository
	{

		public Task<bool> ExistsEmail(string email);

		public Task<bool> ExistsCpf(string cpf);


		public Task<Pessoa?> GetByEmail(string email);

		public Task<Pessoa?> GetByCpf(string email);


		public Task<int> CountAsync();

		public Task<List<Pessoa>> GetPagedAsync(int pageNumber, int pageSize);

	}
}
