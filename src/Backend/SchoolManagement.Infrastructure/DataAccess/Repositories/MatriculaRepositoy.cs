using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Interfaces.Repositories;
using SchoolManagement.Infrastructure.DataAcess;


namespace SchoolManagement.Infrastructure.DataAccess.Repositories
{
	public class MatriculaRepository(SchoolManagementDbContext dbContext) : IMatriculaRepository
	{

		private readonly SchoolManagementDbContext _dbContext = dbContext;


		public async Task Add(Matricula matri) 
			=> await _dbContext.Matriculas.AddAsync(matri);

		public async Task<Matricula?> GetMatriculaById(int id)
			=> await _dbContext.Matriculas.FirstOrDefaultAsync(matri => matri.Id == id);


		public void Update(Matricula matri)
			=> _dbContext.Matriculas.Update(matri);

		public async Task Delete(int id)
		{
			Matricula? matri = await GetMatriculaById(id);

			if (matri is null) return;

			_dbContext.Matriculas.Remove(matri);
		}



	}
}
