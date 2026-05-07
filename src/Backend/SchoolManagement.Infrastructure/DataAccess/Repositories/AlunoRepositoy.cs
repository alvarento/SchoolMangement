using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Infrastructure.DataAcess;


namespace SchoolManagement.Infrastructure.DataAccess.Repositories
{
	public class AlunoRepository(SchoolManagementDbContext dbContext) : IAlunoRepository
	{

		private readonly SchoolManagementDbContext _dbContext = dbContext;


		public async Task Add(Aluno aluno) 
			=> await _dbContext.Alunos.AddAsync(aluno);

		public async Task<Aluno?> GetAlunoById(int id)
			=> await _dbContext.Alunos.FirstOrDefaultAsync(aluno => aluno.Id == id);

		public async Task<Aluno?> GetAlunoByEmail(string email)
			=> await _dbContext.Alunos.FirstOrDefaultAsync(aluno => aluno.Email.Valor.Equals(email));

		public void Update(Aluno aluno)
			=> _dbContext.Alunos.Update(aluno);

		public async Task Delete(int id)
		{
			Aluno? aluno = await GetAlunoById(id);

			if (aluno is not null) _dbContext.Alunos.Remove(aluno);
		}

		public async Task<int> CountAsync()
			=> await _dbContext.Alunos.CountAsync();

		public async Task<List<Aluno>> GetPagedAsync(int pageNumber, int pageSize)
		{
			return await _dbContext.Alunos
				.OrderBy(a => a.Nome.Valor)
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
		}



	}
}
