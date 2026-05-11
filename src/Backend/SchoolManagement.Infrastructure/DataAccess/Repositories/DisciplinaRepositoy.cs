using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.Repositories;
using SchoolManagement.Infrastructure.DataAcess;


namespace SchoolManagement.Infrastructure.DataAccess.Repositories
{
	public class DisciplinaRepository(SchoolManagementDbContext dbContext) : IDisciplinaRepository
	{

		private readonly SchoolManagementDbContext _dbContext = dbContext;


		public async Task Add(Disciplina discip) 
			=> await _dbContext.Disciplinas.AddAsync(discip);


		public async Task<Disciplina?> GetDisciplinaById(int id)
			=> await _dbContext.Disciplinas.FirstOrDefaultAsync(discip => discip.Id == id);

		public void Update(Disciplina discip)
			=> _dbContext.Disciplinas.Update(discip);

		public async Task Delete(int id)
		{
			Disciplina? discip = await GetDisciplinaById(id);

			if (discip is null) return;

			_dbContext.Disciplinas.Remove(discip);
		}



	}
}
