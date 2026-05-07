using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces;
using SchoolManagement.Domain.ValueObjects;
using SchoolManagement.Infrastructure.DataAcess;


namespace SchoolManagement.Infrastructure.DataAccess.Repositories
{
	public class ProfessorRepository(SchoolManagementDbContext dbContext) : IProfessorRepository
	{

		private readonly SchoolManagementDbContext _dbContext = dbContext;


		public async Task Add(Professor prof) 
			=> await _dbContext.Professores.AddAsync(prof);

		public async Task<bool> ExistActiveProfessorWithEmail(string email)
			=> await _dbContext.Professores.AnyAsync(prof => prof.Email == Email.Criar(email));

		public async Task<bool> ExistActiveProfessorWithCpf(string cpf)
			=> await _dbContext.Professores.AnyAsync(prof => prof.Cpf == Cpf.Criar(cpf));

		public async Task<Professor?> GetProfessorById(int id)
			=> await _dbContext.Professores.FirstOrDefaultAsync(prof => prof.Id == id);

		public async Task<Professor?> GetProfessorByEmail(string email)
			=> await _dbContext.Professores.FirstOrDefaultAsync(prof => prof.Email.Equals(Email.Criar(email)));

		public void Update(Professor prof)
			=> _dbContext.Professores.Update(prof);

		public async Task Delete(int id)
		{
			Professor? prof = await GetProfessorById(id);

			if (prof is null) return;

			_dbContext.Professores.Remove(prof);
		}

		public async Task<int> CountAsync()
			=> await _dbContext.Professores.CountAsync();

		public async Task<List<Professor>> GetPagedAsync(int pageNumber, int pageSize)
		{
			return await _dbContext.Professores
				.OrderBy(a => a.Nome)
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
		}



	}
}
