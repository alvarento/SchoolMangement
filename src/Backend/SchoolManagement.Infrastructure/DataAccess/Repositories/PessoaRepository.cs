using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.Repositories;
using SchoolManagement.Infrastructure.DataAcess;


namespace SchoolManagement.Infrastructure.DataAccess.Repositories
{
	public class PessoaRepository(SchoolManagementDbContext dbContext) : IPessoaRepository
	{

		private readonly SchoolManagementDbContext _dbContext = dbContext;



		public async Task<bool> ExistsEmail(string email)
			=> await _dbContext.Pessoas.AnyAsync(pessoa => pessoa.Email.Valor == email);

		public async Task<bool> ExistsCpf(string cpf)
			=> await _dbContext.Pessoas.AnyAsync(pessoa => pessoa.Cpf.Valor == cpf);


		public async Task<Pessoa?> GetByEmail(string email)
			=> await _dbContext.Alunos.FirstOrDefaultAsync(pessoa => pessoa.Email.Valor.Equals(email));

		public async Task<Pessoa?> GetByCpf(string email)
			=> await _dbContext.Pessoas.FirstOrDefaultAsync(pessoa => pessoa.Email.Valor.Equals(email));


		public async Task<int> CountAsync()
			=> await _dbContext.Pessoas.CountAsync();

		public async Task<List<Pessoa>> GetPagedAsync(int pageNumber, int pageSize)
		{
			return await _dbContext.Pessoas
				.OrderBy(a => a.Nome)
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
		}



	}
}
