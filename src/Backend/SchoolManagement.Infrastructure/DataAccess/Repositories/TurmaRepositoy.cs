using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.Repositories;
using SchoolManagement.Infrastructure.DataAcess;


namespace SchoolManagement.Infrastructure.DataAccess.Repositories
{
	public class TurmaRepository(SchoolManagementDbContext dbContext) : ITurmaRepository
	{

		private readonly SchoolManagementDbContext _dbContext = dbContext;


		public async Task Add(Turma turma) 
			=> await _dbContext.Turmas.AddAsync(turma);

		public async Task<Turma?> GetTurmaById(int id)
			=> await _dbContext.Turmas.FirstOrDefaultAsync(turma => turma.Id == id);

		public void Update(Turma turma)
			=> _dbContext.Turmas.Update(turma);

		public async Task Delete(int id)
		{
			Turma? turma = await GetTurmaById(id);

			if (turma is null) return;

			_dbContext.Turmas.Remove(turma);
		}



	}
}
